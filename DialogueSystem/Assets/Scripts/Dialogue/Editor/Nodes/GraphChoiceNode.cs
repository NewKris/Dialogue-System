using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class GraphChoiceNode : GraphNode
    {

        private const string DeleteButtonStyle = "ds-node_choice-delete_button";
        private const string AddChoiceStyle = "ds-node_choice-add";
        private const string ChoiceTextStyle = "ds-node_choice-text";

        private readonly List<TextField> _choices;

        public string[] Choices => Array.ConvertAll(_choices.ToArray(), choice => choice.value);

        public GraphChoiceNode(string[] choices = null, string nodeName = "Choice Node") 
            : base(nodeName)
        {
            choices ??= new[] {"Choice 1", "Choice 2"};
            _choices = Array.ConvertAll(choices, choice => ElementUtility.CreateTextField(choice)).ToList();
        }

        public override void Draw()
        {
            base.Draw();

            AddInputPort();

            Button addChoice = ElementUtility.CreateButton("+", AddChoice);
            addChoice.AddClasses(AddChoiceStyle);
            mainContainer.Add(addChoice);

            foreach (TextField choice in _choices)
            {
                Port outputPort = CreateChoicePort(choice);
                outputContainer.Add(outputPort);
            }

            RefreshExpandedState();
        }

        private void AddChoice()
        {
            string newChoiceName = "Choice " + (_choices.Count + 1);
            TextField choiceText = ElementUtility.CreateTextField(newChoiceName);
            _choices.Add(choiceText);

            outputContainer.Add(CreateChoicePort(choiceText));
        }

        private Port CreateChoicePort(TextField choiceName)
        {
            choiceName.AddClasses(ChoiceTextStyle);

            Port outputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Output, Port.Capacity.Single));

            Button deleteChoice = ElementUtility.CreateButton("X", () => DeleteChoice(choiceName, outputPort));
            deleteChoice.AddClasses(DeleteButtonStyle);

            outputPort.Add(choiceName);
            outputPort.Add(deleteChoice);

            return outputPort;
        }

        private void DeleteChoice(TextField choiceName, Port targetPort)
        {
            if (_choices.Count <= 1) return;

            _choices.Remove(choiceName);
            
            if(targetPort.connected)
                targetPort.connections.First().RemoveFromHierarchy();
            
            outputContainer.Remove(targetPort);
        }

    }
}

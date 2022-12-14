using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;
using VirtualDeviants.Dialogue.SharedNodeData;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class ChoiceNode : GraphNode
    {

        private const string AddChoiceStyle = "ds-node_choice-add";
        private const string ChoiceTextStyle = "ds-node_choice-text";

        private readonly List<TextField> _choices;

        public string[] Choices => Array.ConvertAll(_choices.ToArray(), choice => choice.value);

        public ChoiceNode(string[] choices = null, string nodeName = "Choice Node") 
            : base(nodeName)
        {
            choices ??= new[] {"Choice 1", "Choice 2"};
            _choices = Array.ConvertAll(choices, ElementUtility.CreateTextField).ToList();
        }

        public override void Draw(Vector2 position)
        {
            base.Draw(position);

            AddInputPort();

            Button addChoice = ElementUtility.CreateButton("+", AddChoice);
            addChoice.AddStyleClasses(AddChoiceStyle);
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
            choiceName.AddStyleClasses(ChoiceTextStyle);

            Port outputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Output, Port.Capacity.Single));

            Button deleteChoice = ElementUtility.CreateButton("X", () => DeleteChoice(choiceName, outputPort));
            deleteChoice.AddStyleClasses(DeleteButtonStyle);

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

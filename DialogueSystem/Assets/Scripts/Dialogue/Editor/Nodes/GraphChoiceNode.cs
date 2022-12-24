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

        public List<TextField> Choices { get; set; }

        public override void Initialize(Vector2 position, string nodeName = "Node")
        {
            base.Initialize(position, nodeName);

            Choices = new List<TextField>() 
            { 
                ElementUtility.CreateTextField("Choice 1"),
                ElementUtility.CreateTextField("Choice 2")
            };
        }

        public override void Draw()
        {
            base.Draw();

            AddInputPort();

            Button addChoice = ElementUtility.CreateButton("+", AddChoice);
            addChoice.AddClasses(AddChoiceStyle);
            mainContainer.Add(addChoice);

            foreach (TextField choice in Choices)
            {
                Port outputPort = CreateChoicePort(choice);
                outputContainer.Add(outputPort);
            }

            RefreshExpandedState();
        }

        private void AddChoice()
        {
            string newChoiceName = "Choice " + (Choices.Count + 1);
            TextField choiceText = ElementUtility.CreateTextField(newChoiceName);
            Choices.Add(choiceText);

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
            if (Choices.Count <= 1) return;

            Choices.Remove(choiceName);
            
            if(targetPort.connected)
                targetPort.connections.First().RemoveFromHierarchy();
            
            outputContainer.Remove(targetPort);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class DSChoiceNode : DSNode
    {

        private const string deleteButtonStyle = "ds-node_choice-delete_button";
        private const string addChoiceStyle = "ds-node_choice-add";
        private const string choiceTextStyle = "ds-node_choice-text";

        public List<TextField> Choices { get; set; }

        public override void Initialize(Vector2 position)
        {

            base.Initialize(position);

            NodeName = "Choices";
            Choices = new List<TextField>() 
            { 
                DSElementUtility.CreateTextField("Choice 1"),
                DSElementUtility.CreateTextField("Choice 2")
            };
        }

        public override void Draw()
        {
            base.Draw();

            AddInputPort();

            Button addChoice = DSElementUtility.CreateButton("+", AddChoice);
            addChoice.AddClasses(addChoiceStyle);
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
            TextField choiceText = DSElementUtility.CreateTextField(newChoiceName);
            Choices.Add(choiceText);

            outputContainer.Add(CreateChoicePort(choiceText));
        }

        private Port CreateChoicePort(TextField choiceName)
        {
            choiceName.AddClasses(choiceTextStyle);

            Port outputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Output, Port.Capacity.Single));

            Button deleteChoice = DSElementUtility.CreateButton("X", () => DeleteChoice(choiceName, outputPort));
            deleteChoice.AddClasses(deleteButtonStyle);

            outputPort.Add(choiceName);
            outputPort.Add(deleteChoice);

            return outputPort;
        }

        private void DeleteChoice(TextField choiceName, Port targetPort)
        {
            if (Choices.Count <= 1) return;

            Choices.Remove(choiceName);
            outputContainer.Remove(targetPort);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
    public class DSChoiceNode : DSNode
    {
        
        public List<string> Choices { get; set; }

        public override void Initialize(Vector2 position)
        {

            base.Initialize(position);

            NodeName = "Choices";
            Choices = new List<string>() { "Choice 1", "Choice 2"};
        }

        public override void Draw()
        {
            base.Draw();

            Button addChoice = DSElementUtility.CreateButton("+", AddChoice);
            mainContainer.Insert(1, addChoice);

            foreach (string choice in Choices)
            {
                Port outputPort = CreateChoicePort(choice);
                outputContainer.Add(outputPort);
            }

            RefreshExpandedState();
        }

        private void AddChoice()
        {
            string newChoice = "Choice " + (Choices.Count + 1);
            Choices.Add(newChoice);

            outputContainer.Add(CreateChoicePort(newChoice));
        }

        private Port CreateChoicePort(string portName)
        {
            Port outputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Output, Port.Capacity.Single));

            Button deleteChoice = DSElementUtility.CreateButton("X");

            TextField choiceText = DSElementUtility.CreateTextField(portName);

            outputPort.Add(choiceText);
            outputPort.Add(deleteChoice);

            return outputPort;
        }

    }
}

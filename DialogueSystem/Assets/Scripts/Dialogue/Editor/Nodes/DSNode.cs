using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Enumerations;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{

    /*
    Text
    Choice
    Effector
        - Display Actor
        - Change Expression
        - Play SFX
        - Play VFX
        - Show CG
    Variable
    Branch
    Wait

    StartCutscene(GameObject prefab)
    EndCutscene
    PlayRegion(string regionGUI)
    */

    public abstract class DSNode : Node
    {

        private const string titleStyle = "ds-node_title";
        private const string titleContainerStyle = "ds-node_title_container";
        private const string dragAreaStyle = "ds-node_drag";

        public string NodeName { get; set; }

        public virtual void Initialize(Vector2 position)
        {
            SetPosition(new Rect(position, Vector2.one));
        }

        public virtual void Draw()
        {
            // Title
            TextField title = DSElementUtility.CreateTextField(NodeName);
            title.AddClasses(titleStyle);

            titleContainer.Insert(0, title);
            titleContainer.AddToClassList(titleContainerStyle);

            // Drag Area
            VisualElement dragArea = new VisualElement();
            dragArea.AddClasses(dragAreaStyle);
            mainContainer.Insert(0, dragArea);
        }

        protected void AddInputPort()
        {
            // Input Port
            Port inputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi));
            inputContainer.Add(inputPort);
        }

        protected void AddOutputPort()
        {
            // Output Port
            Port outputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Output, Port.Capacity.Single));
            outputContainer.Add(outputPort);
        }

    }
}

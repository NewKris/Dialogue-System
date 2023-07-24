using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;
using VirtualDeviants.Dialogue.SharedNodeData;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{

    /*
    Text
    Choice
    Effector
        - Display Actor
        - Play Actor Animation
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

    public abstract class GraphNode : Node
    {

        protected const string DeleteButtonStyle = "ds-node_delete-button";
        
        private const string TitleTextStyle = "ds-node_title";
        private const string TitleContainerStyle = "ds-node_title_container";
        private const string ExternalContainerStyle = "ds-node_external_container";
        private const string DragAreaStyle = "ds-node_drag";

        public string nodeName;

        private TextField _titleField;

        public string NodeName => _titleField.value;

        public Vector2 Position
        {
            get => GetPosition().position;
            set => SetPosition(new Rect(value, Vector2.one));
        }

        protected GraphNode(string nodeName = "Node") : base()
        {
            this.nodeName = nodeName;
        }

        public virtual void Draw(Vector2 position)
        {
            Position = position;
            
            AddTitle(nodeName);
            titleContainer.AddToClassList(TitleContainerStyle);

            VisualElement dragArea = new VisualElement();
            dragArea.AddStyleClasses(DragAreaStyle);
            mainContainer.Insert(0, dragArea);
            
            extensionContainer.AddStyleClasses(ExternalContainerStyle);
        }
        
        protected void AddInputPort()
        {
            Port inputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi));
            inputContainer.Add(inputPort);
        }

        protected void AddOutputPort()
        {
            Port outputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Output, Port.Capacity.Single));
            outputContainer.Add(outputPort);
        }
        
        private void AddTitle(string titleText = "Node")
        {
            _titleField = ElementUtility.CreateTextField(titleText);
            _titleField.AddStyleClasses(TitleTextStyle);
            titleContainer.Insert(0, _titleField);
            titleContainer.AddStyleClasses(TitleContainerStyle);
        }
    }
}

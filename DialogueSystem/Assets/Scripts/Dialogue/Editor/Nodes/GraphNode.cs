using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;
using VirtualDeviants.Dialogue.Enumerations;

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
        private const string TitleContainerStyle = "ds-node_title_container";
        private const string DragAreaStyle = "ds-node_drag";

        public virtual void Initialize(Vector2 position)
        {
            SetPosition(new Rect(position, Vector2.one));
        }

        public virtual void Draw()
        {
            titleContainer.AddToClassList(TitleContainerStyle);

            VisualElement dragArea = new VisualElement();
            dragArea.AddClasses(DragAreaStyle);
            mainContainer.Insert(0, dragArea);
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
    }
}

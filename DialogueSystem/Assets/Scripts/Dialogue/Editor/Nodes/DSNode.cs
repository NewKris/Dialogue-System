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

        private const string titleClass = "ds-node_title";
        private const string titleContainerClass = "ds-node_title-container";

        public string NodeName { get; set; }

        public virtual void Initialize(Vector2 position)
        {
            SetPosition(new Rect(position, Vector2.one));
        }

        public virtual void Draw()
        {
            // Title

            TextField title = DSElementUtility.CreateTextField(NodeName);
            title.AddClasses(titleClass);

            titleContainer.AddToClassList(titleContainerClass);
            titleContainer.Insert(0, title);

            // Input Port
            Port inputPort = this.CreatePort("", new PortSettings(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi));
            inputContainer.Add(inputPort);
        }

    }
}

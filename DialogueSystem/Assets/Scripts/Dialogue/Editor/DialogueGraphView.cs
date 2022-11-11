using System;
using System.Collections;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueGraphView : GraphView
    {
        
        public DialogueGraphView()
        {
            AddManipulators();
            AddGridBackground();

            // Temp
            CreateNode();

            AddStyles();
        }

        private void CreateNode()
        {
            DialogueNode node = new DialogueNode();
            node.Initialize();
            node.Draw();
            AddElement(node);
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
        }

        private void AddStyles()
        {
            StyleSheet styleSheet = (StyleSheet) EditorGUIUtility.Load("Dialogue/DialogueGraphViewStyle.uss");
            styleSheets.Add(styleSheet);
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }
    }
}

using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VirtualDeviants.Editor.DialogueAuthor.Graph;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.Blocks {
    public class ChoiceRow : VisualElement {
        private Port _port;
        private GraphNode _node;
        
        public ChoiceRow(
            string value,
            GraphNode node,
            Action<ChoiceRow, string> onValueChanged,
            Action<ChoiceRow> onDelete
        ) {
            _node = node;
            
            Add(VisualElementFactory.CreateTextField(
                value,
                "",
                "Choice",
                (newValue) => onValueChanged(this, newValue)
            ));

            _port = _node.CreateOutputPort(); 
            
            Add(VisualElementFactory.CreateButton(
                EditorGUIUtility.FindTexture("AS Badge Delete"),
                () => onDelete(this)
            ));
            
            Add(_port);
        }

        public void DisconnectPort() {
            _node.DeletePort(_port);
        }
    }
}
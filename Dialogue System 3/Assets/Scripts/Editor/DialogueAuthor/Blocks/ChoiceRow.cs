using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VirtualDeviants.Editor.DialogueAuthor.Graph;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.Blocks {
    public class ChoiceRow : VisualElement {
        private const string CHOICE_ROW_STYLE = "node__choice-container__row";
        
        private readonly Port _port;
        private readonly GraphNode _node;
        
        public ChoiceRow(
            string value,
            GraphNode node,
            Action<ChoiceRow, string> onValueChanged,
            Action<ChoiceRow> onDelete
        ) {
            _node = node;
            
            this.AddStyleClass(CHOICE_ROW_STYLE);
            
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
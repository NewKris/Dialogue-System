using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor.Nodes;
using VirtualDeviants.Editor.DialogueAuthor.Graph;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.Blocks {
    public class ChoiceRow : VisualElement {
        public ChoiceRow(
            string value,
            int index, 
            GraphNode node,
            Action<string> onValueChanged
        ) {
            Add(VisualElementFactory.CreateTextField(
                value,
                index.ToString(),
                "Choice",
                onValueChanged
            ));

            Port port = node.CreateOutputPort(); 
            
            Add(VisualElementFactory.CreateButton(
                EditorGUIUtility.FindTexture("AS Badge Delete"),
                () => {
                    node.DeletePort(port);
                    parent.Remove(this);
                    // TODO: Remove choice text
                }
            ));
            
            Add(port);
        }
    }
}
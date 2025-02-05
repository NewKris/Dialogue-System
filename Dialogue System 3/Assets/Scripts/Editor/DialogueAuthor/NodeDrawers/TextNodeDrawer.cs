using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor;
using VirtualDeviants.DialogueAuthor.Nodes;
using VirtualDeviants.Editor.DialogueAuthor.Attributes;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.NodeDrawers {
    [CustomNodeDrawer(typeof(TextNode))]
    public class TextNodeDrawer : NodeDrawer {
        public override VisualElement Draw(NodeTemplate nodeTemplate) {
            VisualElement dataContainer = VisualElementFactory.CreateEmpty();
            
            if (nodeTemplate is not TextNode textNode) {
                return dataContainer;
            }
            
            dataContainer.Add(VisualElementFactory.CreateTextField(textNode.speaker, val => {
                textNode.speaker = val;
            }));
            
            dataContainer.Add(VisualElementFactory.CreateTextArea(textNode.text, val => {
                textNode.text = val;
            }));

            return dataContainer;
        }
    }
}
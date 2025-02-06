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
            TextNode textNode = nodeTemplate as TextNode;
            
            dataContainer.Add(VisualElementFactory.CreateTextField(
                textNode.speaker, 
                "Speaker: ",
                "Bob",
                val => {
                textNode.speaker = val;
            }));
            
            dataContainer.Add(VisualElementFactory.CreateTextArea(
                textNode.text,
                "Text: ",
                "Lorem ipsum...",
                val => {
                textNode.text = val;
            }));

            return dataContainer;
        }
    }
}
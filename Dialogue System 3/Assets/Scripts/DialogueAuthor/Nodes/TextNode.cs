using VirtualDeviants.DialogueAuthor.Attributes;

namespace VirtualDeviants.DialogueAuthor.Nodes {
    [NodeTitle("Text Node")]
    public class TextNode : NodeTemplate {
        [TextField] public string speaker;
        [TextField] public string text;
    }
}
using VirtualDeviants.Dialogue.Attributes;

namespace VirtualDeviants.Dialogue.Nodes {
    [NodeTitle("Text Node")]
    public class TextNode : NodeTemplate {
        [TextField] public string speaker;
        [TextField] public string text;
    }
}
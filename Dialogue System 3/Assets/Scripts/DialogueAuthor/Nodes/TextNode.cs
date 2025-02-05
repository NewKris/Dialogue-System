using System;
using VirtualDeviants.DialogueAuthor.Attributes;

namespace VirtualDeviants.DialogueAuthor.Nodes {
    [NodeTitle("Text Node")]
    [Serializable]
    public class TextNode : NodeTemplate {
        public string speaker;
        public string text;
    }
}

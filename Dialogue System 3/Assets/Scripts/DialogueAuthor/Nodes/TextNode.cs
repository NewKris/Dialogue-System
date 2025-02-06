using System;
using VirtualDeviants.DialogueAuthor.MemberAttributes;
using VirtualDeviants.DialogueAuthor.NodeAttributes;

namespace VirtualDeviants.DialogueAuthor.Nodes {
    [NodeTitle("Text Node")]
    [Serializable]
    public class TextNode : NodeTemplate {
        [TextInput("Speaker", "Bob")] 
        public string speaker;
        
        [TextInput("Text", "Lorem ipsum...")]
        public string text;
    }
}

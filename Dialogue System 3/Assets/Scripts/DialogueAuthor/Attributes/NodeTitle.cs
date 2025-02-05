using System;

namespace VirtualDeviants.DialogueAuthor.Attributes {
    public class NodeTitle : Attribute {
        public readonly string title;

        public NodeTitle(string title) {
            this.title = title;
        }
    }
}
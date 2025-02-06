using System;

namespace VirtualDeviants.DialogueAuthor.NodeAttributes {
    public class NodeTitle : Attribute {
        public readonly string title;

        public NodeTitle(string title) {
            this.title = title;
        }
    }
}
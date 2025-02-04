using System;

namespace VirtualDeviants.Dialogue.Attributes {
    public class NodeTitle : Attribute {
        public readonly string title;

        public NodeTitle(string title) {
            this.title = title;
        }
    }
}
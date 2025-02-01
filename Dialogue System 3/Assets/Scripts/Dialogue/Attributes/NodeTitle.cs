using System;

namespace VirtualDeviants.Dialogue.Attributes {
    public class NodeTitle : Attribute {
        public string title;

        public NodeTitle(string title) {
            this.title = title;
        }
    }
}
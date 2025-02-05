using System;

namespace VirtualDeviants.Editor.DialogueAuthor.Attributes {
    public class CustomNodeDrawer : Attribute {
        public readonly Type type;
        
        public CustomNodeDrawer(Type type) {
            this.type = type;
        }
    }
}
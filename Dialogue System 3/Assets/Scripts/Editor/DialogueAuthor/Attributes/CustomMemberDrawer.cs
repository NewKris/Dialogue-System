using System;

namespace VirtualDeviants.Editor.DialogueAuthor.Attributes {
    public class CustomMemberDrawer : Attribute {
        public readonly Type type;

        public CustomMemberDrawer(Type type) {
            this.type = type;
        }
    }
}
namespace VirtualDeviants.DialogueAuthor.MemberAttributes {
    public class TextInput : NodeMember {
        public readonly string label;
        public readonly string placeholder;

        public TextInput(string label, string placeholder) {
            this.label = label;
            this.placeholder = placeholder;
        }
    }
}
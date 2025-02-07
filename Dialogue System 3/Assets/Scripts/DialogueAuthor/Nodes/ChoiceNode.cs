using System;
using System.Collections.Generic;
using VirtualDeviants.DialogueAuthor.MemberAttributes;
using VirtualDeviants.DialogueAuthor.NodeAttributes;

namespace VirtualDeviants.DialogueAuthor.Nodes {
    [NodeTitle("Choice")]
    [RemoveDefaultOutputPort]
    [Serializable]
    public class ChoiceNode : NodeTemplate {
        [ChoiceInput]
        public List<string> choiceText;
    }
}
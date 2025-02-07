using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using VirtualDeviants.DialogueAuthor.MemberAttributes;
using VirtualDeviants.DialogueAuthor.NodeAttributes;

namespace VirtualDeviants.DialogueAuthor.Nodes {
    [NodeTitle("Choice")]
    [RemoveDefaultOutputPort]
    [Serializable]
    public class ChoiceNode : NodeTemplate {
        [FormerlySerializedAs("choiceText")] [ChoiceInput]
        public List<string> choices;
    }
}
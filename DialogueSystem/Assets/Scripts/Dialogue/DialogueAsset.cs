using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VirtualDeviants.Dialogue.GraphSaving;

namespace VirtualDeviants.Dialogue
{
    public class DialogueAsset : ScriptableObject
    {
        [SerializeReference]
        public DialogueNode[] nodes;
    }
}

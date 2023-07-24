using UnityEngine;

namespace VirtualDeviants.Dialogue.RuntimeAsset
{
    public class DialogueAsset : ScriptableObject
    {
        [SerializeReference, NonReorderable]
        public DialogueNode[] nodes;
    }
}

using UnityEngine;

namespace VirtualDeviants.Dialogue.SerializedAsset
{
    public class DialogueAsset : ScriptableObject
    {
        [SerializeReference, NonReorderable]
        public Node[] nodes;
    }
}

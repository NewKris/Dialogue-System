using UnityEngine;

namespace VirtualDeviants.Dialogue.RuntimeAsset
{
    public class DialogueAsset : ScriptableObject
    {
        [SerializeReference, NonReorderable]
        public Node[] nodes;
    }
}

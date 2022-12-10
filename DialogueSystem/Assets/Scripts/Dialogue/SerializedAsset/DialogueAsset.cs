using UnityEngine;

namespace VirtualDeviants.Dialogue.SerializedAsset
{
    public class DialogueAsset : ScriptableObject
    {
        [SerializeReference]
        public Node[] nodes;
    }
}

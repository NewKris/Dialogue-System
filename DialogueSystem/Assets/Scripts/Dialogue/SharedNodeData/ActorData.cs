using UnityEngine;

namespace VirtualDeviants.Dialogue.SharedNodeData
{
    public class ActorData : NodeData
    {
        public GameObject actorPrefab;

        public ActorData(GameObject actorPrefab)
        {
            this.actorPrefab = actorPrefab;
        }
        
    }
}

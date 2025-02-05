using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor;

namespace VirtualDeviants.Editor.DialogueAuthor.NodeDrawers {
    public abstract class NodeDrawer {
        public abstract VisualElement Draw(NodeTemplate nodeTemplate);
    }
}
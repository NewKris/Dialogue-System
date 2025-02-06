using System.Reflection;
using UnityEngine.UIElements;
using VirtualDeviants.Editor.DialogueAuthor.Graph;

namespace VirtualDeviants.Editor.DialogueAuthor.NodeDrawers {
    public abstract class NodeMemberDrawer {
        public abstract VisualElement Draw(
            FieldInfo fieldInfo, 
            GraphNode node,
            object objectInstance
        );
    }
}
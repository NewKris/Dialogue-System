using System.Reflection;
using UnityEngine.UIElements;

namespace VirtualDeviants.Editor.DialogueAuthor.NodeDrawers {
    public abstract class NodeMemberDrawer {
        public abstract VisualElement Draw(FieldInfo fieldInfo, object objectInstance);
    }
}
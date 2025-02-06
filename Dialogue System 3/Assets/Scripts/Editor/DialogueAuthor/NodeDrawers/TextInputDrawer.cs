using System.Reflection;
using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor.MemberAttributes;
using VirtualDeviants.Editor.DialogueAuthor.Attributes;
using VirtualDeviants.Editor.DialogueAuthor.Graph;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.NodeDrawers {
    [CustomMemberDrawer(typeof(TextInput))]
    public class TextInputDrawer : NodeMemberDrawer {
        public override VisualElement Draw(
            FieldInfo fieldInfo,
            GraphNode _,
            object objectInstance
        ) {
            if (fieldInfo.FieldType != typeof(string)) {
                return VisualElementFactory.CreateEmpty();
            }

            TextInput attribute = fieldInfo.GetCustomAttribute<TextInput>();
            return VisualElementFactory.CreateTextField(
                (string)fieldInfo.GetValue(objectInstance),
                attribute.label + ": ",
                attribute.placeholder,
                (newValue) => {
                    fieldInfo.SetValue(objectInstance, newValue);
                }
            );
        }
    }
}
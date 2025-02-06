using System.Reflection;
using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor.Attributes;
using VirtualDeviants.Editor.DialogueAuthor.Attributes;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.NodeDrawers {
    [CustomMemberDrawer(typeof(TextInput))]
    public class TextInputDrawer : NodeMemberDrawer {
        public override VisualElement Draw(FieldInfo fieldInfo, object objectInstance) {
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
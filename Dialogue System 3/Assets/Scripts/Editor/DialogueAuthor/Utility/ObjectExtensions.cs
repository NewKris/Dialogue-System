using System;
using System.Linq;
using System.Reflection;

namespace VirtualDeviants.Editor.DialogueAuthor.Utility {
    public static class ObjectExtensions {
        public static T GetAttribute<T>(this object obj) where T: Attribute {
            return obj.GetType().GetCustomAttribute(typeof(T)) as T;
        }

        public static FieldInfo[] GetFieldsWithAttribute(this object obj, Type attributeType) {
            return obj
                .GetType()
                .GetFields()
                .Where(field => Attribute.IsDefined(field, attributeType))
                .ToArray();
        }
    }
}
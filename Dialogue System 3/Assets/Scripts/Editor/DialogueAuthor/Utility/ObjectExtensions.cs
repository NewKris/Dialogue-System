using System;
using System.Reflection;

namespace VirtualDeviants.Editor.DialogueAuthor.Utility {
    public static class ObjectExtensions {
        public static T GetAttribute<T>(this object obj) where T: Attribute {
            return obj.GetType().GetCustomAttribute(typeof(T)) as T;
        }
    }
}
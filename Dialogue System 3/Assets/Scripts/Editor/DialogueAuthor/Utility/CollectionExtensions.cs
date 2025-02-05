using System.Collections.Generic;
using UnityEngine;

namespace VirtualDeviants.Editor.DialogueAuthor.Utility {
    public static class CollectionExtensions {
        public static IEnumerable<T> LogCollection<T>(this IEnumerable<T> collection) {
            foreach (T entry in collection) {
                Debug.Log(entry);
            }

            return collection;
        }
    }
}
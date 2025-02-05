using System;

namespace VirtualDeviants.DialogueAuthor.Editor.Utility {
    public static class WarnUnchangedChanges {
        public static event Action OnWarn;
        
        public static void Invoke() {
            OnWarn?.Invoke();
        }
    }
}
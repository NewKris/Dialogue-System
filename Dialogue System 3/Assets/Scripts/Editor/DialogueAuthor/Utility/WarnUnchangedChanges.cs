using System;

namespace VirtualDeviants.Editor.DialogueAuthor.Utility {
    public static class WarnUnchangedChanges {
        public static event Action OnWarn;
        
        public static void Invoke() {
            OnWarn?.Invoke();
        }
    }
}
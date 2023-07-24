using System;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Core.DialogueVariables
{
    [CreateAssetMenu(menuName = "Variable Database")]
    public class VariableDatabase : ScriptableObject
    {
        public static event Action OnVariableListChanged;
        
        public Variable[] variables;

        private void OnValidate()
        {
            OnVariableListChanged?.Invoke();
        }
    }
}

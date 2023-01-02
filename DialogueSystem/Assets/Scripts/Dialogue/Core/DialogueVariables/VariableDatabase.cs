using UnityEngine;

namespace VirtualDeviants.Dialogue.Core.DialogueVariables
{
    [CreateAssetMenu(menuName = "Variable Database")]
    public class VariableDatabase : ScriptableObject
    {
        public Variable[] variables;
    }
}

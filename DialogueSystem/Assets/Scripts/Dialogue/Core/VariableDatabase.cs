using UnityEngine;

namespace VirtualDeviants.Dialogue.Core
{
    [CreateAssetMenu(menuName = "Variable Database")]
    public class VariableDatabase : ScriptableObject
    {
        public Variable[] variables;
    }
}

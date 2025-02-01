using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Variables
{
	[CreateAssetMenu]
	public class VariableDatabase : ScriptableObject
	{
#if UNITY_EDITOR
		public static VariableDatabase editorInstance;
#endif
		
		public Variable[] variables;

		public List<string> GetVariableKeys(VariableType type)
		{
			return variables.Where(variable => variable.type == type).Select(variable => variable.key).ToList();	
		} 
	}
}
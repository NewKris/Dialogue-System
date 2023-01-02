using System;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Core.DialogueVariables
{
	[Serializable]
	public class Variable
	{
		public string name;
		public int defaultValue;
		
		[HideInInspector]
		public int value;
	}
}
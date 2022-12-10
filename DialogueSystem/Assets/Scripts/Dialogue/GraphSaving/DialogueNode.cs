using System;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualDeviants.Dialogue.GraphSaving
{
	[Serializable]
	public class DialogueNode
	{
		public string nodeName;
		public string guid;
		public string[] outputGuids;
	}
}
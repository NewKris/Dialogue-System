using System;

namespace VirtualDeviants.Dialogue.SharedNodeData
{
	public class ChoiceData : NodeData
	{
		public string[] choices;

		public ChoiceData(string[] choices)
		{
			this.choices = choices;
		}
	}
}
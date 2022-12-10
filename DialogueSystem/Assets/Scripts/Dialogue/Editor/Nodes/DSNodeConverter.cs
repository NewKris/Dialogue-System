using System;
using System.Linq;
using VirtualDeviants.Dialogue.GraphSaving;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
	public static class DSNodeConverter
	{

		public static DialogueNode MapData(DSNode node)
		{
			return node switch
			{
				DSTextNode textNode => MapToTextNode(textNode),
				DSChoiceNode choiceNode => MapToChoiceNode(choiceNode),
				DSEntryNode entryNode => MapToEntryNode(entryNode),
				_ => MapToExitNode(node as DSExitNode)
			};
		}
		
		private static TextDialogueNode MapToTextNode(DSTextNode textNode)
		{
			return new TextDialogueNode()
			{
				nodeName = textNode.NodeName,
				speaker = textNode.Speaker,
				text = textNode.Text
			};
		}
		
		private static ChoiceDialogueNode MapToChoiceNode(DSChoiceNode choiceNode)
		{
			return new ChoiceDialogueNode()
			{
				nodeName = choiceNode.NodeName,
				choices = choiceNode.Choices.Select(x => x.value).ToArray()
			};
		}
		
		private static DialogueNode MapToEntryNode(DSEntryNode entryNode)
		{
			return new DialogueNode()
			{
				nodeName = entryNode.NodeName
			};
		}
		
		private static DialogueNode MapToExitNode(DSExitNode exitNode)
		{
			return new DialogueNode()
			{
				nodeName = exitNode.NodeName
			};
		}
		
	}
}
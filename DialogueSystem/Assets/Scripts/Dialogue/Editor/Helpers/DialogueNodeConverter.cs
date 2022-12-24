using System.Linq;
using VirtualDeviants.Dialogue.Editor.GraphSaving;
using VirtualDeviants.Dialogue.Editor.Nodes;
using VirtualDeviants.Dialogue.SerializedAsset;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class DialogueNodeConverter
	{

		public static Node MapData(GraphNode node)
		{
			return node switch
			{
				GraphTextNode textNode => MapToTextNode(textNode),
				GraphChoiceNode choiceNode => MapToChoiceNode(choiceNode),
				GraphEntryNode entryNode => MapToEntryNode(),
				_ => MapToExitNode()
			};
		}
		
		private static TextNode MapToTextNode(GraphTextNode textNode)
		{
			return new TextNode()
			{
				speaker = textNode.Speaker,
				text = textNode.Text
			};
		}
		
		private static ChoiceNode MapToChoiceNode(GraphChoiceNode choiceNode)
		{
			return new ChoiceNode()
			{
				choices = choiceNode.Choices.Select(x => x.value).ToArray()
			};
		}
		
		private static EntryNode MapToEntryNode()
		{
			return new EntryNode();
		}

		private static ExitNode MapToExitNode()
		{
			return new ExitNode();
		}

	}
}
using System.Linq;
using VirtualDeviants.Dialogue.Editor.GraphSaving;
using VirtualDeviants.Dialogue.Editor.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class GraphNodeConverter
	{
		public static SerializedNode MapData(GraphNode node)
		{
			return node switch
			{
				GraphTextNode textNode => MapToTextNode(textNode),
				GraphChoiceNode choiceNode => MapToChoiceNode(choiceNode),
				GraphEntryNode entryNode => MapToEntryNode(),
				_ => MapToExitNode()
			};
		}

		private static SerializedChoiceNode MapToChoiceNode(GraphChoiceNode choiceNode)
		{
			return new SerializedChoiceNode()
			{
				choices = choiceNode.Choices.Select(x => x.value).ToArray()
			};
		}

		private static SerializedTextNode MapToTextNode(GraphTextNode textNode)
		{
			return new SerializedTextNode()
			{
				speaker = textNode.Speaker,
				text = textNode.Text
			};
		}
		
		private static SerializedEntryNode MapToEntryNode()
		{
			return new SerializedEntryNode();
		}

		private static SerializedExitNode MapToExitNode()
		{
			return new SerializedExitNode();
		}
	}
}
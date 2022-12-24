using System.Linq;
using VirtualDeviants.Dialogue.Editor.GraphSaving;
using VirtualDeviants.Dialogue.Editor.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class GraphNodeToSerializedNodeConverter
	{
		public static SerializedNode MapData(GraphNode node)
		{
			return node switch
			{
				GraphTextNode textNode => MapToTextNode(textNode),
				GraphChoiceNode choiceNode => MapToChoiceNode(choiceNode),
				GraphEntryNode entryNode => MapToEntryNode(entryNode),
				_ => MapToExitNode(node)
			};
		}

		private static SerializedChoiceNode MapToChoiceNode(GraphChoiceNode choiceNode)
		{
			return new SerializedChoiceNode()
			{
				nodeName = choiceNode.NodeName,
				choices = choiceNode.Choices.Select(x => x.value).ToArray()
			};
		}

		private static SerializedTextNode MapToTextNode(GraphTextNode textNode)
		{
			return new SerializedTextNode()
			{
				nodeName = textNode.NodeName,
				speaker = textNode.Speaker,
				text = textNode.Text
			};
		}
		
		private static SerializedEntryNode MapToEntryNode(GraphEntryNode entryNode)
		{
			return new SerializedEntryNode() { nodeName = entryNode.NodeName };
		}

		private static SerializedExitNode MapToExitNode(GraphNode graphNode)
		{
			return new SerializedExitNode() { nodeName = graphNode.NodeName };
		}
	}
}
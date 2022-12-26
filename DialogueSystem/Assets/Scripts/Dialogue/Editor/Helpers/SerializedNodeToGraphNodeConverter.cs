using VirtualDeviants.Dialogue.Editor.GraphSaving;
using VirtualDeviants.Dialogue.Editor.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class SerializedNodeToGraphNodeConverter
	{

		public static GraphNode MapData(SerializedNode node)
		{
			return node switch
			{
				SerializedTextNode textNode => MapToTextNode(textNode),
				SerializedChoiceNode choiceNode => MapToChoiceNode(choiceNode),
				SerializedEntryNode entryNode => MapToEntryNode(entryNode),
				_ => MapToExitNode(node)
			};
		}

		private static GraphTextNode MapToTextNode(SerializedTextNode textNode)
		{
			return new GraphTextNode(textNode.speaker, textNode.text, textNode.nodeName);
		}

		private static GraphChoiceNode MapToChoiceNode(SerializedChoiceNode choiceNode)
		{
			return new GraphChoiceNode(choiceNode.choices);
		}

		private static GraphEntryNode MapToEntryNode(SerializedEntryNode entryNode)
		{
			return new GraphEntryNode(entryNode.nodeName);
		}

		private static GraphExitNode MapToExitNode(SerializedNode node)
		{
			return new GraphExitNode(node.nodeName);
		}
		
	}
}
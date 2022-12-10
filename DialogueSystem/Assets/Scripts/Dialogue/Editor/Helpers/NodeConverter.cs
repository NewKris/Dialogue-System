using System.Linq;
using VirtualDeviants.Dialogue.Editor.Nodes;
using VirtualDeviants.Dialogue.SerializedAsset;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class NodeConverter
	{

		public static Node MapData(GraphNode node)
		{
			return node switch
			{
				GraphTextNode textNode => MapToTextNode(textNode),
				GraphChoiceNode choiceNode => MapToChoiceNode(choiceNode),
				GraphEntryNode entryNode => MapToEntryNode(entryNode),
				_ => MapToExitNode(node as GraphExitNode)
			};
		}
		
		private static TextNode MapToTextNode(GraphTextNode textNode)
		{
			return new TextNode()
			{
				nodeName = textNode.NodeName,
				speaker = textNode.Speaker,
				text = textNode.Text
			};
		}
		
		private static ChoiceNode MapToChoiceNode(GraphChoiceNode choiceNode)
		{
			return new ChoiceNode()
			{
				nodeName = choiceNode.NodeName,
				choices = choiceNode.Choices.Select(x => x.value).ToArray()
			};
		}
		
		private static Node MapToEntryNode(GraphEntryNode entryNode)
		{
			return new Node()
			{
				nodeName = entryNode.NodeName
			};
		}
		
		private static Node MapToExitNode(GraphExitNode exitNode)
		{
			return new Node()
			{
				nodeName = exitNode.NodeName
			};
		}
		
	}
}
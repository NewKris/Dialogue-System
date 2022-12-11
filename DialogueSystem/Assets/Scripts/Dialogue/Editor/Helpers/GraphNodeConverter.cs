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
				_ => MapToNode(node)
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
		
		private static SerializedNode MapToNode(GraphNode node)
		{
			return new SerializedNode()
			{
				nodeName = node.NodeName
			};
		}
	}
}
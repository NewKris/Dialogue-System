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
				_ => MapToNode(node)
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
		
		private static Node MapToNode(GraphNode entryNode)
		{
			return new Node()
			{
				nodeName = entryNode.NodeName
			};
		}

	}
}
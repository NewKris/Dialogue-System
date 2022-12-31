using VirtualDeviants.Dialogue.Editor.Nodes;
using VirtualDeviants.Dialogue.SharedNodeData;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class DataParser
	{

		public static NodeData CreateNodeData(GraphNode node)
		{
			return node switch
			{
				GraphTextNode textNode => CreateTextData(textNode),
				GraphChoiceNode choiceNode => CreateChoiceData(choiceNode),
				GraphEntryNode => CreateStartData(),
				_ => CreateEndData()
			};
		}

		public static GraphNode CreateGraphNode(NodeData data)
		{
			return data switch
			{
				TextData textData => CreateTextNode(textData),
				ChoiceData choiceData => CreateChoiceNode(choiceData),
				EntryData entryData => CreateEntryNode(),
				_ => CreateExitNode()
			};
		}

		private static GraphTextNode CreateTextNode(TextData textData) => new(textData.speaker, textData.text);
		private static GraphChoiceNode CreateChoiceNode(ChoiceData choiceData) => new(choiceData.choices);
		private static GraphEntryNode CreateEntryNode() => new();
		private static GraphExitNode CreateExitNode() => new();
		
		private static TextData CreateTextData(GraphTextNode textNode) => new(textNode.Speaker, textNode.Text);
		private static ChoiceData CreateChoiceData(GraphChoiceNode choiceNode) => new(choiceNode.Choices);
		private static EntryData CreateStartData() => new();
		private static ExitData CreateEndData() => new();

	}
}
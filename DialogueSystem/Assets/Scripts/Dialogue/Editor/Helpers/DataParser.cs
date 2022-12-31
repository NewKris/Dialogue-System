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
				ActorNode actorNode => CreateActorData(actorNode),
				TextNode textNode => CreateTextData(textNode),
				ChoiceNode choiceNode => CreateChoiceData(choiceNode),
				EntryNode => CreateStartData(),
				_ => CreateEndData()
			};
		}

		public static GraphNode CreateGraphNode(NodeData data)
		{
			return data switch
			{
				ActorData actorData => CreateActorNode(actorData),
				TextData textData => CreateTextNode(textData),
				ChoiceData choiceData => CreateChoiceNode(choiceData),
				EntryData entryData => CreateEntryNode(),
				_ => CreateExitNode()
			};
		}

		private static ActorNode CreateActorNode(ActorData actorData) => new(actorData.actorPrefab);
		private static TextNode CreateTextNode(TextData textData) => new(textData.speaker, textData.text);
		private static ChoiceNode CreateChoiceNode(ChoiceData choiceData) => new(choiceData.choices);
		private static EntryNode CreateEntryNode() => new();
		private static ExitNode CreateExitNode() => new();

		private static ActorData CreateActorData(ActorNode actorNode) => new(actorNode.Actor);
		private static TextData CreateTextData(TextNode textNode) => new(textNode.Speaker, textNode.Text);
		private static ChoiceData CreateChoiceData(ChoiceNode choiceNode) => new(choiceNode.Choices);
		private static EntryData CreateStartData() => new();
		private static ExitData CreateEndData() => new();

	}
}
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
				IfNode ifNode => CreateIfData(ifNode),
				VariableNode variableNode => CreateVariableData(variableNode),
				ActorNode actorNode => CreateActorData(actorNode),
				TextNode textNode => CreateTextData(textNode),
				ChoiceNode choiceNode => CreateChoiceData(choiceNode),
				StartNode => CreateStartData(),
				_ => CreateExitData()
			};
		}

		public static GraphNode CreateGraphNode(NodeData data)
		{
			return data switch
			{
				IfData ifData => CreateIfNode(ifData),
				VariableData variableData => CreateVariableNode(variableData),
				ActorData actorData => CreateActorNode(actorData),
				TextData textData => CreateTextNode(textData),
				ChoiceData choiceData => CreateChoiceNode(choiceData),
				StartData startData => CreateStartNode(),
				_ => CreateExitNode()
			};
		}

		private static IfNode CreateIfNode(IfData ifData) => new(ifData.key, ifData.value, ifData.comparison);
		private static IfData CreateIfData(IfNode ifNode) => new(ifNode.VariableKey, ifNode.ComparisonValue, ifNode.ValueComparison);
		
		private static VariableNode CreateVariableNode(VariableData variableData) 
			=> new(variableData.key, variableData.value, variableData.operation);
		private static VariableData CreateVariableData(VariableNode variableNode) 
			=> new(variableNode.VariableKey, variableNode.OperationValue, variableNode.Operation);
		
		private static ActorNode CreateActorNode(ActorData actorData) 
			=> new(actorData.actorPrefab);
		private static ActorData CreateActorData(ActorNode actorNode) 
			=> new(actorNode.Actor);
		
		private static TextNode CreateTextNode(TextData textData) 
			=> new(textData.speaker, textData.text);
		private static TextData CreateTextData(TextNode textNode) 
			=> new(textNode.Speaker, textNode.Text);
		
		private static ChoiceNode CreateChoiceNode(ChoiceData choiceData) 
			=> new(choiceData.choices);
		private static ChoiceData CreateChoiceData(ChoiceNode choiceNode) 
			=> new(choiceNode.Choices);
		
		private static StartNode CreateStartNode() 
			=> new();
		private static StartData CreateStartData() 
			=> new();
		
		private static ExitNode CreateExitNode() 
			=> new();
		private static ExitData CreateExitData() 
			=> new();

	}
}
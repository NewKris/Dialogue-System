using System;
using System.Collections.Generic;
using VirtualDeviants.Dialogue.Editor.Graph.Nodes;
using VirtualDeviants.Dialogue.Editor.Graph.Nodes.SetVars;
using VirtualDeviants.Dialogue.Editor.Graph.Nodes.VarChecks;
using VirtualDeviants.Dialogue.Nodes;

namespace VirtualDeviants.Dialogue.Editor
{
	public static class NodeTypeMap
	{
		public static readonly Dictionary<Type, Type> NodeTypes = new Dictionary<Type, Type>()
		{
			{ typeof(TextNode), typeof(TextGraphNode) },
			{ typeof(ChoiceNode), typeof(ChoiceGraphNode) },
			{ typeof(BoolVarCheckNode), typeof(BoolVarCheckGraphNode) },
			{ typeof(IntVarCheckNode), typeof(IntVarCheckGraphNode) },
			{ typeof(StringVarCheckNode), typeof(StringVarCheckGraphNode) },
			{ typeof(SetBoolVarNode), typeof(SetBoolVarGraphNode) },
			{ typeof(SetIntVarNode), typeof(SetIntVarGraphNode) },
			{ typeof(SetStringVarNode), typeof(SetStringVarGraphNode) },
			{ typeof(StartNode), typeof(StartGraphNode) },
			{ typeof(EndNode), typeof(EndGraphNode) },
		};
	}
}
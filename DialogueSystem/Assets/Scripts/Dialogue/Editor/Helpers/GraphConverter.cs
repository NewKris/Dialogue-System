using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using VirtualDeviants.Dialogue.Editor.Nodes;
using Node = VirtualDeviants.Dialogue.SerializedAsset.Node;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class GraphConverter
	{
		public static Node[] ConvertGraph(DialogueGraphView graph)
		{
			var graphNodes = graph.nodes;
			int count = graphNodes.Count();
            
			List<Node> nodes = new List<Node>();
			Dictionary<GraphNode, Node> closedList = new Dictionary<GraphNode, Node>();

			foreach (UnityEditor.Experimental.GraphView.Node node in graphNodes)
			{
				if(node is not GraphNode dsNode) continue;

				MapToDialogueNode(dsNode, nodes, closedList);
			}

			return nodes.ToArray();
		}
        
		private static Node MapToDialogueNode(GraphNode graphNode, List<Node> mappedList, Dictionary<GraphNode, Node> closedList) 
		{
			if(closedList.ContainsKey(graphNode)) return closedList[graphNode];

			Node node = NodeConverter.MapData(graphNode);
			node.guid = Guid.NewGuid().ToString();
            
			closedList.Add(graphNode, node);

			List<Node> connected = new List<Node>();
			foreach (Port output in graphNode.outputContainer.Children().Where(x => x is Port))
			{
				if(!output.connected) continue;

				GraphNode connection = (GraphNode) output.connections.First().input.parent.parent.parent.parent.parent;
				connected.Add(MapToDialogueNode(connection, mappedList, closedList));
			}

			node.outputGuids = connected.Select(x => x.guid).ToArray();
            
			mappedList.Add(node);
			return node;
		}
	}
}
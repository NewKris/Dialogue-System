using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VirtualDeviants.Dialogue.Editor.GraphSaving;
using VirtualDeviants.Dialogue.Editor.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class GraphAssetConverter
	{
		

		public static GraphAsset ConvertToAsset(DialogueGraphView graph)
		{
			GraphAsset asset = ScriptableObject.CreateInstance<GraphAsset>();
			var graphNodes = graph.nodes;
            
			List<SerializedNode> nodes = new List<SerializedNode>();

			Dictionary<GraphNode, SerializedNode> closedList = new Dictionary<GraphNode, SerializedNode>();

			int universalIndex = 0;
			
			foreach (Node node in graphNodes)
			{
				if(node is not GraphNode graphNode) continue;

				MapNode(graphNode, nodes, closedList, ref universalIndex);
			}
			asset.nodes = nodes;

			List<SerializedGroup> groups = new List<SerializedGroup>();
			foreach (Group group in graph.graphElements.Where(graphElement => graphElement is Group))
			{
				GraphNode[] groupedNodes = Array.ConvertAll(
					group.containedElements.Where(graphElement => graphElement is GraphNode).ToArray(),
					arrayElement => (GraphNode)arrayElement);
				
				SerializedGroup serializedGroup = new SerializedGroup()
				{
					groupTitle = group.title,
					groupedNodeGuids = groupedNodes.Select(groupNode => closedList[groupNode].guid).ToArray()
				};
				
				groups.Add(serializedGroup);
			}
			asset.groups = groups;

			return asset;
		}

		private static SerializedNode MapNode(
			GraphNode graphNode, 
			List<SerializedNode> mappedList, 
			Dictionary<GraphNode, SerializedNode> closedList,
			ref int index)
		{
			if(closedList.ContainsKey(graphNode)) return closedList[graphNode];

			SerializedNode node = GraphNodeConverter.MapData(graphNode);
			node.guid = index;
			node.nodeRect = graphNode.GetPosition();

			closedList.Add(graphNode, node);
			index++;

			List<SerializedNode> connected = new List<SerializedNode>();
			foreach (Port output in graphNode.outputContainer.Children().Where(x => x is Port))
			{
				if(!output.connected) continue;

				GraphNode connection = (GraphNode) output.connections.First().input.parent.parent.parent.parent.parent;
				connected.Add(MapNode(connection, mappedList, closedList, ref index));
			}

			node.outputGuids = connected.Select(x => x.guid).ToArray();
            
			mappedList.Add(node);
			return node;
		}

		public static DialogueGraphView ConvertToGraphView(GraphAsset graphAsset)
		{
			throw new NotImplementedException();
		}
		
	}
}
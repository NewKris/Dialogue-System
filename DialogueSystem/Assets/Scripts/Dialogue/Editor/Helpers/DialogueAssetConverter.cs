using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VirtualDeviants.Dialogue.Editor.Nodes;
using VirtualDeviants.Dialogue.SerializedAsset;
using Node = VirtualDeviants.Dialogue.SerializedAsset.Node;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class DialogueAssetConverter
	{
		public static DialogueAsset ConvertToAsset(DialogueGraphView graph)
		{
			DialogueAsset dialogueAsset = ScriptableObject.CreateInstance<DialogueAsset>();
			var graphNodes = graph.nodes;
            
			List<Node> nodes = new List<Node>();
			
			Dictionary<GraphNode, Node> closedList = new Dictionary<GraphNode, Node>();

			foreach (UnityEditor.Experimental.GraphView.Node node in graphNodes)
			{
				if(node is not GraphNode graphNode) continue;

				MapNode(graphNode, nodes, closedList);
			}

			dialogueAsset.nodes = nodes.ToArray();
			
			return dialogueAsset;
		}
        
		private static Node MapNode(
			GraphNode graphNode, 
			List<Node> mappedList, 
			Dictionary<GraphNode, Node> closedList) 
		{
			if(closedList.ContainsKey(graphNode)) return closedList[graphNode];

			Node node = DialogueNodeConverter.MapData(graphNode);
			node.guid = Guid.NewGuid().ToString();
            
			closedList.Add(graphNode, node);

			List<Node> connected = new List<Node>();
			foreach (Port output in graphNode.outputContainer.Children().Where(x => x is Port))
			{
				if(!output.connected) continue;

				GraphNode connection = (GraphNode) output.connections.First().input.parent.parent.parent.parent.parent;
				connected.Add(MapNode(connection, mappedList, closedList));
			}

			node.outputGuids = connected.Select(x => x.guid).ToArray();
            
			mappedList.Add(node);
			return node;
		}
	}
}
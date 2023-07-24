using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VirtualDeviants.Dialogue.Editor.Nodes;
using VirtualDeviants.Dialogue.RuntimeAsset;

namespace VirtualDeviants.Dialogue.Editor.Helpers
{
	public static class DialogueAssetConverter
	{
		public static DialogueAsset ConvertToAsset(DialogueGraphView graph)
		{
			DialogueAsset dialogueAsset = ScriptableObject.CreateInstance<DialogueAsset>();
			var graphNodes = graph.nodes;
			List<DialogueNode> nodes = new List<DialogueNode>();
			Dictionary<GraphNode, DialogueNode> closedList = new Dictionary<GraphNode, DialogueNode>();
			int universalIndex = 0;
			
			foreach (UnityEditor.Experimental.GraphView.Node node in graphNodes)
			{
				if(node is not GraphNode graphNode) continue;

				MapNode(graphNode, nodes, closedList, ref universalIndex);
			}

			dialogueAsset.nodes = nodes.ToArray();
			
			return dialogueAsset;
		}
        
		private static DialogueNode MapNode(
			GraphNode graphNode, 
			List<DialogueNode> mappedList, 
			Dictionary<GraphNode, DialogueNode> closedList,
			ref int index) 
		{
			if(closedList.ContainsKey(graphNode)) return closedList[graphNode];

			DialogueNode dialogueNode = new DialogueNode()
			{
				data = DataParser.CreateNodeData(graphNode),
			};
            
			closedList.Add(graphNode, dialogueNode);

			List<DialogueNode> connected = new List<DialogueNode>();
			foreach (Port output in graphNode.outputContainer.Children().Where(x => x is Port))
			{
				if(!output.connected) continue;

				GraphNode connection = (GraphNode) output.connections.First().input.parent.parent.parent.parent.parent;
				connected.Add(MapNode(connection, mappedList, closedList, ref index));
			}

			dialogueNode.outputGuids = connected.Select(x => x.guid).ToArray();

			dialogueNode.guid = index;
			mappedList.Add(dialogueNode);
			index++;
			
			return dialogueNode;
		}
	}
}
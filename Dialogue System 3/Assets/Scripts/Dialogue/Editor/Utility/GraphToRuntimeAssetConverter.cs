using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using VirtualDeviants.Dialogue.Editor.Graph;
using VirtualDeviants.Dialogue.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Utility {
	public static class GraphToRuntimeAssetConverter {
		public static NodeTemplate[] ToRuntimeAssetNodes(this DialogueGraphView graphView) {
			List<GraphNode> nodes = graphView.nodes.Where(node => node is GraphNode).Cast<GraphNode>().ToList();
			
			GraphNode startNode = nodes.First(node => node.Template is EntryNode);
			nodes.Remove(startNode);
			nodes.Insert(0, startNode);
			
			NodeTemplate[] templates = nodes.Select(node => node.Template).ToArray();
			for (int i = 0; i < templates.Length; i++) {
				GraphNode[] connections = nodes[i].GetConnections();
				templates[i].connections = connections.Select(connection => nodes.IndexOf(connection)).ToArray();
			}
			
			return templates;
		}
	}
}
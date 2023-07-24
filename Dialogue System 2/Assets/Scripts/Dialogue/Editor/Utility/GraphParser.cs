using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VirtualDeviants.Dialogue.Editor.Graph;
using VirtualDeviants.Dialogue.Editor.Graph.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Utility
{
	public static class GraphParser
	{
		public static DialogueGraphView ToGraphView(this GraphAsset graphAsset, DialogueAuthorWindow parentWindow)
		{
			DialogueGraphView graphView = new DialogueGraphView(parentWindow);

			List<GraphNode> nodes = new List<GraphNode>();
			foreach (SerializedGraphNode serializedNode in graphAsset.nodes)
			{
				GraphNode graphNode = (GraphNode) Activator.CreateInstance(
					NodeTypeMap.NodeTypes[serializedNode.template.GetType()], 
					serializedNode.nodeTitle, 
					serializedNode.template
				);
				
				nodes.Add(graphNode);
				graphView.AddNodeToGraph(graphNode, serializedNode.position);
			}
			
			for (int i = 0; i < graphAsset.nodes.Length; i++)
			{
				GraphNode[] connections = graphAsset.nodes[i].connections
					.Select(connection => connection == -1 ? null : nodes[connection])
					.ToArray();
				
				nodes[i].CreateConnections(graphView, connections);
			}
			
			foreach (SerializedGraphGroup serializedGroup in graphAsset.groups)
			{
				GraphNode[] containedNodes = serializedGroup.containedNodes.Select(node => nodes[node]).ToArray();
				graphView.AddGroupToGraph(serializedGroup.groupTitle, containedNodes);
			}
			
			return graphView;
		}

		public static void UpdateGraphAssetData(this GraphAsset graphAsset, DialogueGraphView graph)
		{
			List<GraphNode> nodes = graph.nodes.Where(node => node is GraphNode).Cast<GraphNode>().ToList();
			SerializedGraphNode[] serializedNodes = new SerializedGraphNode[nodes.Count];

			for (int i = 0; i < nodes.Count; i++)
			{
				nodes[i].UpdateTemplateData();
				int[] connections = nodes[i].GetConnections().Select(connection =>
				{
					if (connection == null) return -1;
					
					return nodes.IndexOf(connection);
				}).ToArray();

				serializedNodes[i] = new SerializedGraphNode()
				{
					nodeTitle = nodes[i].NodeName,
					template = nodes[i].Template,
					connections = connections,
					position = nodes[i].Position
				};
			}

			List<Group> groups = graph.graphElements.Where(group => group is Group).Cast<Group>().ToList();
			SerializedGraphGroup[] serializedGroups = new SerializedGraphGroup[groups.Count];

			for (int i = 0; i < groups.Count; i++)
			{
				GraphNode[] containedNodes = groups[i].containedElements.Where(node => node is GraphNode).Cast<GraphNode>().ToArray();

				serializedGroups[i] = new SerializedGraphGroup()
				{
					groupTitle = groups[i].title,
					containedNodes = containedNodes.Select(node => nodes.IndexOf(node)).ToArray()
				};
			}

			graphAsset.nodes = serializedNodes;
			graphAsset.groups = serializedGroups;
		}

		public static NodeTemplate[] ToDialogueAssetNodes(this DialogueGraphView graphView)
		{
			List<GraphNode> nodes = graphView.nodes.Where(node => node is GraphNode).Cast<GraphNode>().ToList();
			GraphNode startNode = nodes.First(node => node is StartGraphNode);
			
			nodes.Remove(startNode);
			nodes.Insert(0, startNode);
			
			NodeTemplate[] templates = nodes.Select(node => node.Template).ToArray();
			for (int i = 0; i < templates.Length; i++)
			{
				GraphNode[] connections = nodes[i].GetConnections();
				templates[i].connections = connections.Select(connection => nodes.IndexOf(connection)).ToArray();
			}
			
			return templates;
		}
	}
}
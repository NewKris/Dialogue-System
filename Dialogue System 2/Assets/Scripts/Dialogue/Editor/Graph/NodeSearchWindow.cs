using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VirtualDeviants.Dialogue.Editor.Graph.Nodes;
using VirtualDeviants.Dialogue.Editor.Utility;

namespace VirtualDeviants.Dialogue.Editor.Graph
{
	public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
	{
		private DialogueGraphView _dialogueGraphView;
		
		public void Initialize(DialogueGraphView dialogueGraphView)
		{
			_dialogueGraphView = dialogueGraphView;
		}

		public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
		{
			return NodeSearchConfig.SearchEntries;
		}

		public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
		{
			Type nodeTemplateType = (Type) searchTreeEntry.userData;
			Type graphNodeType = NodeTypeMap.NodeTypes[nodeTemplateType];

			NodeTemplate template = (NodeTemplate) Activator.CreateInstance(nodeTemplateType);
 			GraphNode node = (GraphNode) Activator.CreateInstance(graphNodeType, CreateDisplayName(template), template);
			
			_dialogueGraphView.AddNodeToGraph(node, context.screenMousePosition, true);
			return true;
		}

		private static string CreateDisplayName(NodeTemplate template)
		{
			return Regex.Replace(template.GetType().Name, "(\\B[A-Z])", " $1").Replace(" Node", "");
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VirtualDeviants.DialogueAuthor;
using VirtualDeviants.DialogueAuthor.Attributes;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.Graph {
	public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider {
		private DialogueGraphView _dialogueGraphView;
		
		public void Initialize(DialogueGraphView dialogueGraphView) {
			_dialogueGraphView = dialogueGraphView;
		}

		public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) {
			Type[] nodeTypes = Assembly.GetAssembly(typeof(NodeTemplate))
				.GetTypes()
				.Where(type => type.IsSubclassOf(typeof(NodeTemplate)))
				.ToArray();
			
			List<SearchTreeEntry> entries = nodeTypes.Select(CreateEntry).ToList();

			entries.Insert(0, CreateGroup("Create Node"));
            
			return entries;
		}

		public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context) {
			Type nodeTemplateType = (Type)searchTreeEntry.userData;

			NodeTemplate template = (NodeTemplate)Activator.CreateInstance(nodeTemplateType);
 			GraphNode node = new GraphNode(template);
			
			_dialogueGraphView.AddNodeToGraph(node, context.screenMousePosition, true);
			return true;
		}

		private SearchTreeEntry CreateEntry(Type nodeType) {
			return new SearchTreeEntry(new GUIContent(
				GetNodeTitle(nodeType),
				TextureUtility.GetIndentTexture()
			)) {
				level = 1,
				userData = nodeType
			};
		}
		
		private static SearchTreeGroupEntry CreateGroup(string groupName, int level = 0) {
			return new SearchTreeGroupEntry(new GUIContent(groupName), level);
		}

		private string GetNodeTitle(Type nodeType) {
			NodeTitle title = nodeType.GetCustomAttribute<NodeTitle>();
			return title?.title ?? nodeType.ToString();
		}
	}
}
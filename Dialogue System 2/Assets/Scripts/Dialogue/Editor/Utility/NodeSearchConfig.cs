using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VirtualDeviants.Dialogue.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Utility
{
	public static class NodeSearchConfig
	{
		public static readonly List<SearchTreeEntry> SearchEntries = new List<SearchTreeEntry>()
		{
			CreateGroup("Create Node"),
			CreateEntry("Text", 1, typeof(TextNode)),
			CreateEntry("Choice", 1, typeof(ChoiceNode)),
			CreateGroup("Check Variable", 1),
			CreateEntry("Check Bool Variable", 2, typeof(BoolVarCheckNode)),
			CreateEntry("Check Int Variable", 2, typeof(IntVarCheckNode)),
			CreateEntry("Check String Variable", 2, typeof(StringVarCheckNode)),
			CreateGroup("Set Variable", 1),
			CreateEntry("Set Bool Variable", 2, typeof(SetBoolVarNode)),
			CreateEntry("Set Int Variable", 2, typeof(SetIntVarNode)),
			CreateEntry("Set String Variable", 2, typeof(SetStringVarNode)),
			CreateGroup("Other", 1),
			CreateEntry("Start", 2, typeof(StartNode)),
			CreateEntry("End", 2, typeof(EndNode)),
		};

		private static SearchTreeEntry CreateEntry(string entryName, int level, Type nodeTemplateType)
		{
			return new SearchTreeEntry(new GUIContent(entryName, TextureUtility.indentTexture)) {level = level, userData = nodeTemplateType};
		}

		private static SearchTreeGroupEntry CreateGroup(string groupName, int level = 0)
		{
			return new SearchTreeGroupEntry(new GUIContent(groupName), level);
		}
	}
}
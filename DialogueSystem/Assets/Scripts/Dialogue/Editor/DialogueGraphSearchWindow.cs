using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueGraphSearchWindow : ScriptableObject, ISearchWindowProvider
    {

        private static Texture2D Indent;
        
        private DialogueGraphView _graphView;

        private List<SearchTreeEntry> _searchEntries;

        public void Initialize(DialogueGraphView graphView)
        {
            _graphView = graphView;

            Indent = new Texture2D(1, 1);
            Indent.SetPixel(0, 0, Color.clear);
            Indent.Apply();

            _searchEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Node")),
                CreateEntry("Text", 1, NodeType.TEXT),
                CreateEntry("Choice", 1, NodeType.CHOICE),
                CreateEntry("Variable", 1, NodeType.VARIABLE),

                new SearchTreeGroupEntry(new GUIContent("Flow"), 1),
                CreateEntry("If", 2, NodeType.IF),
                CreateEntry("Start", 2, NodeType.ENTRY),
                CreateEntry("Exit", 2, NodeType.EXIT),

                new SearchTreeGroupEntry(new GUIContent("Graphics"), 1),
                CreateEntry("Show Actor", 2, NodeType.ACTOR),
            };
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            NodeType nodeType = (NodeType) searchTreeEntry.userData;
            _graphView.AddElement(_graphView.CreateNode(nodeType, context.screenMousePosition, true));
            return true;
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) { return _searchEntries; }

        private static SearchTreeEntry CreateEntry(string label, int level, Enum nodeType)
        {
            return new SearchTreeEntry(new GUIContent(label, Indent)) {level = level, userData = nodeType};
        }
    }
}

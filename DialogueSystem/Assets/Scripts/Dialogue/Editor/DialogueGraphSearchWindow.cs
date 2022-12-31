using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueGraphSearchWindow : ScriptableObject, ISearchWindowProvider
    {

        private DialogueGraphView _graphView;
        private Texture2D _indent;

        private List<SearchTreeEntry> _searchEntries;

        public void Initialize(DialogueGraphView graphView)
        {
            _graphView = graphView;

            _indent = new Texture2D(1, 1);
            _indent.SetPixel(0, 0, Color.clear);
            _indent.Apply();

            _searchEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Node")),
                new SearchTreeEntry(new GUIContent("Text", _indent)) {level = 1, userData = NodeType.TEXT},
                new SearchTreeEntry(new GUIContent("Choice", _indent)) {level = 1, userData = NodeType.CHOICE},

                new SearchTreeGroupEntry(new GUIContent("Flow"), 1),
                new SearchTreeEntry(new GUIContent("Entry", _indent)) {level = 2, userData = NodeType.ENTRY},
                new SearchTreeEntry(new GUIContent("Exit", _indent)) {level = 2, userData = NodeType.EXIT},

                new SearchTreeGroupEntry(new GUIContent("Graphics"), 1),
                new SearchTreeEntry(new GUIContent("Show Actor", _indent)) {level = 2, userData = NodeType.ACTOR},
            };
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            NodeType nodeType = (NodeType) searchTreeEntry.userData;
            _graphView.AddElement(_graphView.CreateNode(nodeType, context.screenMousePosition, true));
            return true;
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) { return _searchEntries; }
    }
}

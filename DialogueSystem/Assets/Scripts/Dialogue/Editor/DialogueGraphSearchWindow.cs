using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VirtualDeviants.Dialogue.Enumerations;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueGraphSearchWindow : ScriptableObject, ISearchWindowProvider
    {

        private DialogueGraphView _graphView;
        private Texture2D _indent;

        List<SearchTreeEntry> searchEntries;

        public void Initialize(DialogueGraphView graphView)
        {
            _graphView = graphView;

            _indent = new Texture2D(1, 1);
            _indent.SetPixel(0, 0, Color.clear);
            _indent.Apply();

            searchEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Node")),
                new SearchTreeEntry(new GUIContent("Text", _indent)) {level = 1, userData = NodeType.Text},
                new SearchTreeEntry(new GUIContent("Choice", _indent)) {level = 1, userData = NodeType.Choice},

                new SearchTreeGroupEntry(new GUIContent("Flow"), 1),
                new SearchTreeEntry(new GUIContent("Entry", _indent)) {level = 2, userData = NodeType.Entry},
                new SearchTreeEntry(new GUIContent("Exit", _indent)) {level = 2, userData = NodeType.Exit},

                new SearchTreeGroupEntry(new GUIContent("Effects"), 1),
            };
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            return searchEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            NodeType nodeType = (NodeType) SearchTreeEntry.userData;
            _graphView.AddElement(_graphView.CreateNode(nodeType, context.screenMousePosition, true));
            return true;
        }
    }
}

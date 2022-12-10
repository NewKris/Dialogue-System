using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using VirtualDeviants.Dialogue.GraphSaving;
using VirtualDeviants.Dialogue.Editor.Nodes;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueAuthorWindow : EditorWindow
    {

        private const string SavePath = "Assets/Data/Dialogue/";
        private const string DefaultFileName = "New Dialogue";
        private const string ContainerClass = "ds-toolbar_container";
        private const string ContainerElement = "ds-toolbar_element";
        private const string ContainerTextField = "ds-toolbar_textfield";
        private const string ContainerButton = "ds-toolbar_button";

        private static DSGraphView Graph;
        private static GraphData CurrentlyLoadedGraph;
        private static TextField GraphName;
        
        [MenuItem("Window/Dialogue Author")]
        public static void OpenWindow()
        {
            GetWindow<DialogueAuthorWindow>("Dialogue Author");
        }

        private void CreateGUI()
        {
            AddGraphView();
            AddToolbar();
            AddStyles();
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();
            toolbar.AddClasses(ContainerClass);

            GraphName = DSElementUtility.CreateTextField(DefaultFileName);
            GraphName.AddClasses(ContainerTextField, ContainerElement);
            toolbar.Add(GraphName);

            Button saveButton = DSElementUtility.CreateButton("Save", SaveActiveGraph);
            saveButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(saveButton);

            Button loadButton = DSElementUtility.CreateButton("Load", LoadGraph);
            loadButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(loadButton);

            Button exportButton = DSElementUtility.CreateButton("Export", ExportActiveGraph);
            exportButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(exportButton);

            Button importButton = DSElementUtility.CreateButton("Import", ImportGraph);
            importButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(importButton);

            rootVisualElement.Add(toolbar);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("Dialogue/DSVariables.uss");
            rootVisualElement.AddStyleSheets("Dialogue/DSToolbarStyle.uss");
        }

        private void AddGraphView()
        {
            DSGraphView graphView = new DSGraphView(this);

            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);

            Graph = graphView;
        }

        private void SaveActiveGraph()
        {

            DialogueNode[] nodes = CompileCurrentGraph();

            if (CurrentlyLoadedGraph == null)
            {
                // TODO
                // Open the FileDialog to select a save location

                DialogueAsset newAsset = ScriptableObject.CreateInstance<DialogueAsset>();
                newAsset.nodes = nodes;
                
                AssetDatabase.CreateAsset(newAsset, SavePath + GraphName.value + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
            }
            else
            {
                CurrentlyLoadedGraph.nodes = nodes;
                EditorUtility.SetDirty(CurrentlyLoadedGraph);
            }
        }

        private DialogueNode[] CompileCurrentGraph()
        {
            var graphNodes = Graph.nodes;
            int count = graphNodes.Count();
            
            List<DialogueNode> nodes = new List<DialogueNode>();
            Dictionary<DSNode, DialogueNode> closedList = new Dictionary<DSNode, DialogueNode>();

            foreach (Node node in graphNodes)
            {
                if(node is not DSNode dsNode) continue;

                MapToDialogueNode(dsNode, nodes, closedList);
            }

            return nodes.ToArray();
        }
        
        private DialogueNode MapToDialogueNode(DSNode node, List<DialogueNode> mappedList, Dictionary<DSNode, DialogueNode> closedList) 
        {
            if(closedList.ContainsKey(node)) return closedList[node];

            DialogueNode dialogueNode = DSNodeConverter.MapData(node);
            dialogueNode.guid = Guid.NewGuid().ToString();
            
            closedList.Add(node, dialogueNode);

            List<DialogueNode> connected = new List<DialogueNode>();
            foreach (Port output in node.outputContainer.Children().Where(x => x is Port))
            {
                if(!output.connected) continue;

                DSNode connection = (DSNode) output.connections.First().input.parent.parent.parent.parent.parent;
                connected.Add(MapToDialogueNode(connection, mappedList, closedList));
            }

            dialogueNode.outputGuids = connected.Select(x => x.guid).ToArray();
            
            mappedList.Add(dialogueNode);
            return dialogueNode;
        }

        private void LoadGraph()
        {

        }

        private void ExportActiveGraph()
        {

        }

        private void ImportGraph()
        {

        }
    }
}

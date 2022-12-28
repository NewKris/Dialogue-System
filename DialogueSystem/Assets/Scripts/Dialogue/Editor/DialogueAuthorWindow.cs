using System;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;
using VirtualDeviants.Dialogue.Editor.GraphSaving;
using VirtualDeviants.Dialogue.Editor.Helpers;
using VirtualDeviants.Dialogue.RuntimeAsset;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueAuthorWindow : EditorWindow
    {

        // TODO
        // Support multiple Entry nodes
        // Use a common NodeData struct for each node class
        // Variables
        // Confirm before discarding unsaved changes

        private const string DefaultFileName = "New Dialogue";
        private const string ContainerClass = "ds-toolbar_container";
        private const string ContainerElement = "ds-toolbar_element";
        private const string ContainerText = "ds-toolbar_text";
        private const string ContainerButton = "ds-toolbar_button";

        private static string LoadedPath;
        private static DialogueGraphView Graph;
        private static Label GraphName;

        [MenuItem("Window/Dialogue Author")]
        public static void OpenWindow()
        {
            GetWindow<DialogueAuthorWindow>("Dialogue Author");
        }

        private void CreateGUI()
        {
            AddToolbar();
            AddStyles();
            DrawGraphView();
        }

        private void OnEnable()
        {
            rootVisualElement.RegisterCallback<KeyDownEvent>(OnSave, TrickleDown.TrickleDown);
            rootVisualElement.RegisterCallback<KeyDownEvent>(OnAlignHorizontal, TrickleDown.TrickleDown);
            rootVisualElement.RegisterCallback<KeyDownEvent>(OnAlignVertical, TrickleDown.TrickleDown);
        }

        private void OnDisable()
        {
            rootVisualElement.UnregisterCallback<KeyDownEvent>(OnSave);
            rootVisualElement.UnregisterCallback<KeyDownEvent>(OnAlignHorizontal);
            rootVisualElement.UnregisterCallback<KeyDownEvent>(OnAlignVertical);
        }

        private void OnSave(KeyDownEvent keyDownEvent)
        {
            if(keyDownEvent.keyCode == KeyCode.S && keyDownEvent.modifiers == EventModifiers.Control)
                SaveActiveGraph();
        }

        private void OnAlignVertical(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode == KeyCode.V && keyDownEvent.modifiers == EventModifiers.Control)
                Graph?.AlignVertical();
        }

        private void OnAlignHorizontal(KeyDownEvent keyDownEvent)
        {
            if(keyDownEvent.keyCode == KeyCode.H && keyDownEvent.modifiers == EventModifiers.Control)
                Graph?.AlignHorizontal();
        }
        
        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();
            toolbar.AddClasses(ContainerClass);

            GraphName = ElementUtility.CreateLabel();
            GraphName.AddClasses(ContainerElement, ContainerText);
            toolbar.Add(GraphName);
            
            Button[] buttons = {
                ElementUtility.CreateButton("Save Graph", SaveActiveGraph),
                ElementUtility.CreateButton("Load Graph", LoadGraphAsset),
                ElementUtility.CreateButton("Export to Asset", ExportActiveGraph),
            };

            foreach (Button button in buttons)
            {
                button.AddClasses(ContainerElement, ContainerButton);
                toolbar.Add(button);
            }

            rootVisualElement.Add(toolbar);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("Dialogue/DSVariables.uss");
            rootVisualElement.AddStyleSheets("Dialogue/DSToolbarStyle.uss");
        }

        private void DrawGraphView()
        {
            if(rootVisualElement.Contains(Graph))
                rootVisualElement.Remove(Graph);

            if(string.IsNullOrEmpty(LoadedPath)) return;
            
            Graph = LoadGraph(LoadedPath);
            
            if (Graph == null) return;

            Graph.AuthorWindow = this;
            Graph.StretchToParentSize();

            rootVisualElement.Insert(0, Graph);
        }

        private void SaveActiveGraph()
        {
            if(string.IsNullOrEmpty(LoadedPath) || Graph == null) return;
            
            GraphAsset graphAsset = GraphAssetConverter.ConvertToAsset(Graph);
            AssetCreator.CreateAsset(LoadedPath, graphAsset);
        }

        private void LoadGraphAsset()
        {
            string selectedFile = EditorUtility.OpenFilePanel("Load Graph Asset", Application.dataPath, "asset");

            if (string.IsNullOrEmpty(selectedFile)) return;

            selectedFile = ToLocalPath(selectedFile);
            LoadedPath = selectedFile;
            DrawGraphView();
        }

        private static DialogueGraphView LoadGraph(string path)
        {
            GraphAsset graphAsset = AssetDatabase.LoadAssetAtPath<GraphAsset>(path);

            if (graphAsset == null)
            {
                EditorUtility.DisplayDialog("Load Graph", "The selected file is not of type GraphAsset!", "Close");
                return null;
            }
            
            GraphName.text = graphAsset.name;
            return GraphAssetConverter.ConvertToGraphView(graphAsset);
        }
        
        private void ExportActiveGraph()
        {
            if(string.IsNullOrEmpty(LoadedPath) || Graph == null) return;
            
            string exportPath = EditorUtility.OpenFolderPanel("Save Graph Asset", Application.dataPath, "Name");
            
            if(string.IsNullOrEmpty(exportPath)) return;
            
            exportPath = ToLocalPath(exportPath) + "/" + GraphName.text + ".asset";

            if (File.Exists(exportPath))
            {
                if(!EditorUtility.DisplayDialog("Export Graph", "The file already exists in this folder. Do you want to overwrite it?", "Yes", "No"))
                    return;
            }
            
            DialogueAsset dialogueAsset = DialogueAssetConverter.ConvertToAsset(Graph);
            
            AssetCreator.CreateAsset(exportPath, dialogueAsset);
        }

        private static string ToLocalPath(string absolutePath)
        {
            return absolutePath.Substring(absolutePath.LastIndexOf("Assets/", StringComparison.Ordinal));
        }
    }
}

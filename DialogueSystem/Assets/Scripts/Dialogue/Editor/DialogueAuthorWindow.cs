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
        // Align selected Nodes with shortcuts like in PureRef
        // Support multiple Entry nodes

        private const string DefaultFileName = "New Dialogue";
        private const string ContainerClass = "ds-toolbar_container";
        private const string ContainerElement = "ds-toolbar_element";
        private const string ContainerTextField = "ds-toolbar_textfield";
        private const string ContainerButton = "ds-toolbar_button";

        private static DialogueGraphView Graph;
        private static TextField GraphName;
        
        [MenuItem("Window/Dialogue Author")]
        public static void OpenWindow()
        {
            GetWindow<DialogueAuthorWindow>("Dialogue Author");
        }

        private void CreateGUI()
        {
            AddGraphView(new DialogueGraphView());
            AddToolbar();
            AddStyles();
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();
            toolbar.AddClasses(ContainerClass);

            GraphName = ElementUtility.CreateTextField(DefaultFileName);
            GraphName.AddClasses(ContainerTextField, ContainerElement);
            toolbar.Add(GraphName);

            Button saveButton = ElementUtility.CreateButton("Save Graph", SaveActiveGraph);
            saveButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(saveButton);

            Button loadButton = ElementUtility.CreateButton("Load Graph", LoadGraph);
            loadButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(loadButton);

            Button exportButton = ElementUtility.CreateButton("Export to Asset", ExportActiveGraph);
            exportButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(exportButton);

            rootVisualElement.Add(toolbar);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("Dialogue/DSVariables.uss");
            rootVisualElement.AddStyleSheets("Dialogue/DSToolbarStyle.uss");
        }

        private void AddGraphView(DialogueGraphView graphView)
        {
            graphView.AuthorWindow = this;

            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);

            Graph = graphView;
        }

        private void SaveActiveGraph()
        {
            string savePath = EditorUtility.OpenFolderPanel("Save Graph Asset", Application.dataPath, "Name");
            
            if(string.IsNullOrEmpty(savePath)) return;

            savePath = ToLocalPath(savePath) + "/" + GraphName.value + ".asset";

            if (File.Exists(savePath))
            {
                if(!EditorUtility.DisplayDialog("Save Graph", "The file already exists in this folder. Do you want to overwrite it?", "Yes", "No"))
                    return;
            }
            
            GraphAsset graphAsset = GraphAssetConverter.ConvertToAsset(Graph);
            AssetCreator.CreateAsset(savePath, graphAsset);
        }

        private void LoadGraph()
        {
            string selectedFile = EditorUtility.OpenFilePanel("Load Graph Asset", Application.dataPath, "asset");

            if (string.IsNullOrEmpty(selectedFile)) return;

            selectedFile = ToLocalPath(selectedFile);
            GraphAsset graphAsset = AssetDatabase.LoadAssetAtPath<GraphAsset>(selectedFile);

            if (graphAsset == null)
            {
                EditorUtility.DisplayDialog("Load Graph", "Invalid file selected! File must be of type GraphAsset.", "Close");
                return;
            }

            rootVisualElement.Clear();
            AddGraphView(GraphAssetConverter.ConvertToGraphView(graphAsset));
            AddToolbar();
        }

        private void ExportActiveGraph()
        {
            string exportPath = EditorUtility.OpenFolderPanel("Save Graph Asset", Application.dataPath, "Name");
            
            if(string.IsNullOrEmpty(exportPath)) return;
            
            exportPath = ToLocalPath(exportPath) + "/" + GraphName.value + ".asset";

            if (File.Exists(exportPath))
            {
                if(!EditorUtility.DisplayDialog("Export Graph", "The file already exists in this folder. Do you want to overwrite it?", "Yes", "No"))
                    return;
            }
            
            DialogueAsset dialogueAsset = DialogueAssetConverter.ConvertToAsset(Graph);
            
            AssetCreator.CreateAsset(exportPath, dialogueAsset);
        }

        private string ToLocalPath(string absolutePath)
        {
            return absolutePath.Substring(absolutePath.LastIndexOf("Assets/", StringComparison.Ordinal));
        }
    }
}

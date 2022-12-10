using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;
using VirtualDeviants.Dialogue.SerializedAsset;
using Node = VirtualDeviants.Dialogue.SerializedAsset.Node;

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

        private static DialogueGraphView Graph;
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

            GraphName = ElementUtility.CreateTextField(DefaultFileName);
            GraphName.AddClasses(ContainerTextField, ContainerElement);
            toolbar.Add(GraphName);

            Button saveButton = ElementUtility.CreateButton("Save", SaveActiveGraph);
            saveButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(saveButton);

            Button loadButton = ElementUtility.CreateButton("Load", LoadGraph);
            loadButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(loadButton);

            Button exportButton = ElementUtility.CreateButton("Export", ExportActiveGraph);
            exportButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(exportButton);

            Button importButton = ElementUtility.CreateButton("Import", ImportGraph);
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
            DialogueGraphView graphView = new DialogueGraphView(this);

            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);

            Graph = graphView;
        }

        private void SaveActiveGraph()
        {
            
        }

        private void LoadGraph()
        {

        }

        private void ExportActiveGraph()
        {
            Node[] nodes = GraphConverter.ConvertGraph(Graph);
            
            // TODO
            // Open the FileDialog to select a save location

            string path = SavePath + GraphName.value + ".asset";

            if (File.Exists(path))
            {
                AssetCreator.UpdateDialogueAsset(path, nodes);
            }
            else
            {
                DialogueAsset newAsset = CreateInstance<DialogueAsset>();
                newAsset.nodes = nodes;

                AssetCreator.CreateDialogueAsset(path, newAsset);    
            }
        }

        private void ImportGraph()
        {

        }
    }
}

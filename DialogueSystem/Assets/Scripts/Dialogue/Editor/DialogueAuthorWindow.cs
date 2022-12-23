using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using VirtualDeviants.Dialogue.Editor.GraphSaving;
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

            /*Button importButton = ElementUtility.CreateButton("Import", ImportGraph);
            importButton.AddClasses(ContainerElement, ContainerButton);
            toolbar.Add(importButton);*/

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
            GraphAsset graphAsset = GraphAssetConverter.ConvertToAsset(Graph);
            string path = SavePath + GraphName.value + " Graph.asset";
            
            AssetCreator.CreateAsset(path, graphAsset);
        }

        private void LoadGraph()
        {
            rootVisualElement.Clear();
            
            GraphAsset graphAsset = AssetDatabase.LoadAssetAtPath<GraphAsset>(SavePath + GraphName.value + " Graph.asset");
            
            // TODO
            // Open FileDialog to select GraphAsset
            
            AddGraphView(GraphAssetConverter.ConvertToGraphView(graphAsset));
            AddToolbar();
        }

        private void ExportActiveGraph()
        {
            DialogueAsset dialogueAsset = DialogueAssetConverter.ConvertToAsset(Graph);
            
            // TODO
            // Open the FileDialog to select a save location

            string path = SavePath + GraphName.value + ".asset";
            AssetCreator.CreateAsset(path, dialogueAsset);
        }
    }
}

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Graph;
using VirtualDeviants.Dialogue.Editor.ShortcutCommands;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Variables;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueAuthorWindow : EditorWindow
    {
        private static event Action<GraphAsset> OnGraphAssetOpened;
        
        public GraphAsset openedGraphAsset;
        public DialogueGraphView activeGraphView;
        
        [MenuItem("Window/Dialogue Author")]
        public static void OpenWindow()
        { 
            GetWindow<DialogueAuthorWindow>("Dialogue Author");
        }

        public static void OpenWindow(GraphAsset graphAsset)
        {
            GetWindow<DialogueAuthorWindow>("Dialogue Author");
            OnGraphAssetOpened?.Invoke(graphAsset);
        }
        
        public void DrawGraphAsset(GraphAsset graphAsset)
        {
            openedGraphAsset = graphAsset;
            
            if(rootVisualElement.Contains(activeGraphView))
                rootVisualElement.Remove(activeGraphView);

            activeGraphView = graphAsset.ToGraphView(this);
            if(activeGraphView == null) return;

            activeGraphView.StretchToParentSize();
            rootVisualElement.Insert(0, activeGraphView);
        }

        public void WarnForUnsavedChanges()
        {
            hasUnsavedChanges = true;
        }
        
        private void CreateGUI()
        {
            rootVisualElement.LoadStyleSheet(StyleBlackBoard.WindowStyleSheets);
            TextureUtility.indentTexture = TextureUtility.CreateIndentTexture();
        }

        private void OnEnable()
        {
            OnGraphAssetOpened += DrawGraphAsset;
            
            LoadVariableDatabase();
            
            rootVisualElement.Add(BlockFactory.CreateNavigationBar(new []
            {
                new ButtonConfig("Save", new SaveCommand(this)),
                // new ButtonConfig("Save As", new SaveAsCommand()),
                // new ButtonConfig("Load", new LoadCommand(this)),
                new ButtonConfig("Export", new ExportCommand(this)),
            }));
        }

        private void OnDisable()
        {
            OnGraphAssetOpened -= DrawGraphAsset;
        }

        private void LoadVariableDatabase()
        {
            string[] guids = AssetDatabase.FindAssets("t:VariableDatabase");

            if (guids.Length == 0)
            {
                CreateVariableDatabase();
                guids = AssetDatabase.FindAssets("t:VariableDatabase");
            }
            
            if(guids.Length > 1)
                Debug.LogError("Multiple Variable Databases found in the project! Please make sure there is only one!");

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            VariableDatabase.editorInstance = AssetDatabase.LoadAssetAtPath<VariableDatabase>(path);
        }

        private void CreateVariableDatabase()
        {
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance(typeof(VariableDatabase)), "Assets/Dialogue/VariableDatabase.asset");
            AssetDatabase.Refresh();
        }
    }
}

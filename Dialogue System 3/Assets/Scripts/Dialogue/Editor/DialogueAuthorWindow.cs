using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Blocks.Toolbar;
using VirtualDeviants.Dialogue.Editor.Graph;
using VirtualDeviants.Dialogue.Editor.ShortcutCommands;
using VirtualDeviants.Dialogue.Editor.Utility;

namespace VirtualDeviants.Dialogue.Editor {
    public class DialogueAuthorWindow : EditorWindow {
        public GraphAsset openedGraphAsset;
        public DialogueGraphView activeGraphView;

        private DialogueToolbar _toolbar;
        
        public static void OpenWindow(GraphAsset graphAsset) {
            DialogueAuthorWindow window = GetWindow<DialogueAuthorWindow>("Dialogue Author");
            window.DrawGraphAsset(graphAsset);
        }
        
        private void CreateGUI() {
            rootVisualElement.LoadStyleSheet(StyleBlackBoard.WindowStyleSheets);
        }

        private void OnEnable() {
            WarnUnchangedChanges.OnWarn += WarnForUnsavedChanges;
            
            rootVisualElement.Clear();
            
            rootVisualElement.Add(DialogueToolbar.Create(new [] {
                new ButtonConfig("Save", new SaveCommand(this)),
                new ButtonConfig("Export", new ExportCommand(this)),
            }));
        }

        private void OnDisable() {
            WarnUnchangedChanges.OnWarn -= WarnForUnsavedChanges;
        }
        
        private void WarnForUnsavedChanges() {
            hasUnsavedChanges = true;
        }
        
        private void DrawGraphAsset(GraphAsset graphAsset) {
            openedGraphAsset = graphAsset;

            if (rootVisualElement.Contains(activeGraphView)) {
                rootVisualElement.Remove(activeGraphView);
            }

            activeGraphView = AssetToGraphViewConverter.Convert(graphAsset, this);
            activeGraphView.StretchToParentSize();
            
            rootVisualElement.Insert(0, activeGraphView);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using UnityEditor.UIElements;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueAuthorWindow : EditorWindow
    {

        private const string defaultFileName = "New Dialogue";
        private const string containerClass = "ds-toolbar_container";
        private const string containerElement = "ds-toolbar_element";
        private const string containerTextField = "ds-toolbar_textfield";
        private const string containerButton = "ds-toolbar_button";

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
            toolbar.AddClasses(containerClass);

            TextField dialogueName = DSElementUtility.CreateTextField(defaultFileName);
            dialogueName.AddClasses(containerTextField, containerElement);
            toolbar.Add(dialogueName);

            Button saveButton = DSElementUtility.CreateButton("Save", SaveActiveGraph);
            saveButton.AddClasses(containerElement, containerButton);
            toolbar.Add(saveButton);

            Button loadButton = DSElementUtility.CreateButton("Load", LoadGraph);
            loadButton.AddClasses(containerElement, containerButton);
            toolbar.Add(loadButton);

            Button exportButton = DSElementUtility.CreateButton("Export", ExportActiveGraph);
            exportButton.AddClasses(containerElement, containerButton);
            toolbar.Add(exportButton);

            Button importButton = DSElementUtility.CreateButton("Import", ImportGraph);
            importButton.AddClasses(containerElement, containerButton);
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
        }

        private void SaveActiveGraph()
        {

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

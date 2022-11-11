using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;

namespace VirtualDeviants.Dialogue.Editor
{
    public class DialogueAuthorWindow : EditorWindow
    {

        [MenuItem("Window/Dialogue Author")]
        public static void OpenWindow()
        {
            GetWindow<DialogueAuthorWindow>("Dialogue Author");
        }

        private void CreateGUI()
        {
            AddGraphView();
            AddStyles();
        }

        private void AddStyles()
        {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("Dialogue/DialogueVariables.uss");
            rootVisualElement.styleSheets.Add(styleSheet);
        }

        private void AddGraphView()
        {
            DialogueGraphView graphView = new DialogueGraphView();

            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);
        }

    }
}

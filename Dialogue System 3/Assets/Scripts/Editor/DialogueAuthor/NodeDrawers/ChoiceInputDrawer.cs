using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor.MemberAttributes;
using VirtualDeviants.Editor.DialogueAuthor.Attributes;
using VirtualDeviants.Editor.DialogueAuthor.Graph;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.NodeDrawers {
    [CustomMemberDrawer(typeof(ChoiceInput))]
    public class ChoiceInputDrawer : NodeMemberDrawer {
        public override VisualElement Draw(
            FieldInfo fieldInfo,
            GraphNode node,
            object objectInstance
        ) {
            if (fieldInfo.FieldType != typeof(List<string>) ) {
                return VisualElementFactory.CreateEmpty();
            }

            VisualElement container = VisualElementFactory.CreateEmpty();
            
            List<string> choices = fieldInfo.GetValue(objectInstance) as List<string>;
            choices ??= new List<string>();
            
            Button addChoiceButton = VisualElementFactory.CreateButton(
                EditorGUIUtility.FindTexture("Toolbar Plus"),
                () => {
                    choices.Add("");
                    int newIndex = choices.Count - 1;
                    fieldInfo.SetValue(objectInstance, choices);
                    
                    container.Add(CreateChoiceRow(
                        "",
                        newIndex,
                        node,
                        (newValue) => {
                            WarnUnchangedChanges.Invoke();
                            choices[newIndex] = newValue;
                        }
                    ));
                }
            );
            
            container.Add(addChoiceButton);
            
            for (int i = 0; i < choices.Count; i++) {
                int temp = i;
                
                container.Add(CreateChoiceRow(
                    choices[i],
                    i,
                    node,
                    (newValue) => {
                        WarnUnchangedChanges.Invoke();
                        choices[temp] = newValue;
                    }
                ));
            }
            
            return container;
        }

        private VisualElement CreateChoiceRow(
            string value, 
            int index, 
            GraphNode parentNode, 
            Action<string> updateTextCallback
        ) {
            VisualElement row = VisualElementFactory.CreateEmpty();
            
            TextField inputField = VisualElementFactory.CreateTextField(
                value,
                index.ToString(),
                "Choice",
                updateTextCallback
            );

            Port outputPort = parentNode.CreateOutputPort();
            
            row.Add(inputField);
            row.Add(outputPort);
            
            return row;
        }
    }
}
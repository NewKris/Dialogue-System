using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor.Nodes;
using VirtualDeviants.Editor.DialogueAuthor.Graph;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.Blocks {
    public class ChoiceContainer : VisualElement {
        private readonly GraphNode _graphNode;
        private readonly List<ChoiceRow> _rows;
        private readonly List<string> _choices;
        
        public ChoiceContainer(GraphNode graphNode, List<string> choices) {
            _graphNode = graphNode;
            _choices = choices;
            _rows = new List<ChoiceRow>();
            
            Add(CreateAddButton());
            AddChoiceRows();
        }

        private void AddChoiceRows() {
            foreach (string choice in _choices) {
                ChoiceRow row = new ChoiceRow(
                    choice,
                    _graphNode,
                    UpdateChoiceValue,
                    DeleteChoiceRow
                ); 
                
                Add(row);
                _rows.Add(row);
            }
        }

        private Button CreateAddButton() {
            return VisualElementFactory.CreateButton(
                EditorGUIUtility.FindTexture("Toolbar Plus"),
                () => {
                    _choices.Add("");

                    ChoiceRow newRow = new ChoiceRow(
                        "",
                        _graphNode,
                        UpdateChoiceValue,
                        DeleteChoiceRow
                    ); 
                    
                    Add(newRow);
                    _rows.Add(newRow);
                    
                    WarnUnchangedChanges.Invoke();
                }
            );
        }

        private void UpdateChoiceValue(ChoiceRow row, string newValue) {
            int index = _rows.IndexOf(row);
                            
            _choices[index] = newValue;
            WarnUnchangedChanges.Invoke();
        }

        private void DeleteChoiceRow(ChoiceRow row) {
            int index = _rows.IndexOf(row);
                            
            row.DisconnectPort();

            Remove(row);
            _rows.Remove(row);
            _choices.RemoveAt(index);
                            
            WarnUnchangedChanges.Invoke();
        }
    }
}
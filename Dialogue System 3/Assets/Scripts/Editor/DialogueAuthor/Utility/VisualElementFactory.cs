using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Editor.DialogueAuthor.Graph;

namespace VirtualDeviants.Editor.DialogueAuthor.Utility {
	public static class VisualElementFactory {
		public static VisualElement CreateEmpty() {
			return new VisualElement();
		}
        
		public static Button CreateButton(string text, Action onClick = null) {
			return new Button(onClick) {
				text = text,
			};
		}
		
		public static Button CreateButton(Texture2D icon, Action onClick = null) {
			return new Button(onClick) {
				iconImage = Background.FromTexture2D(icon)
			};
		}

		public static ToolbarButton CreateToolbarButton(string text, Action onClick) {
			return new ToolbarButton(onClick) {
				text = text
			};
		}

		public static GridBackground CreateGridBackground() {
			GridBackground gridBackground = new GridBackground();
			gridBackground.StretchToParentSize();
			return gridBackground;
		}

		public static Group CreateGroup(string groupName, List<ISelectable> fromSelection) {
			Group group = new Group() {
				title = groupName,
			};
			
			group.SetPosition(new Rect(Vector2.zero, Vector2.one));
			
			foreach (ISelectable graphElement in fromSelection) {
				if(graphElement is GraphNode node) 
					group.AddElement(node);
			}

			return group;
		}

		public static TextField CreateTextField(
			string value, 
			string label,
			string placeholder,
			Action<string> callback
		) {
			TextField textField = new TextField {
				value = value,
				label = label,
				textEdition = {
					placeholder = placeholder
				},
				textSelection = {
					selectAllOnFocus = false,
					selectAllOnMouseUp = false,
					selectionColor = Color.yellow
				}
			};

			textField.AddStyleClass("node__text-field");
			textField.RegisterValueChangedCallback(x => {
				WarnUnchangedChanges.Invoke();
				callback(x.newValue);
			});

			return textField;
		}

		public static TextField CreateTextArea(
			string value, 
			string label, 
			string placeholder,
			Action<string> callback
		) {
			TextField textArea = CreateTextField(value, label, placeholder, callback);
			textArea.multiline = true;
			textArea.AddStyleClass("node__text-field--area");
			
			return textArea;
		}

		public static IntegerField CreateIntegerField(int value = 0) {
			return new IntegerField() { value = value };
		}

		public static PopupField<T> CreatePopupField<T>(List<T> values, int selected = 0) {
			return new PopupField<T>(values, selected);
		}

		public static EnumField CreateEnumField(Enum value) {
			return new EnumField(value);
		}

		public static Toggle CreateCheckBox(bool value = false, string label = "") {
			return new Toggle(label) {
				value = value
			};
		}

		public static Label CreateLabel(string value) {
			return new Label(value);
		}
	}
}

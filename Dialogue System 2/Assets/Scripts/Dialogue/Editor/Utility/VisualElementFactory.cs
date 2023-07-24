using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Graph;
using VirtualDeviants.Dialogue.Editor.Graph.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Utility
{
	public static class VisualElementFactory
	{
		public static Button CreateButton(string text, Action onClick = null)
		{
			Button button = new Button(onClick)
			{
				text = text
			};

			return button;
		}

		public static ToolbarButton CreateToolbarButton(string text, Action onClick = null)
		{
			ToolbarButton toolbarButton = new ToolbarButton(onClick)
			{
				text = text
			};

			return toolbarButton;
		}

		public static GridBackground CreateGridBackground()
		{
			GridBackground gridBackground = new GridBackground();
			gridBackground.StretchToParentSize();
			return gridBackground;
		}

		public static Group CreateGroup(string groupName, List<ISelectable> fromSelection)
		{
			Group group = new Group()
			{
				title = groupName,
			};
			
			group.SetPosition(new Rect(Vector2.zero, Vector2.one));
			
			foreach (ISelectable graphElement in fromSelection)
			{
				if(graphElement is GraphNode node) 
					group.AddElement(node);
			}

			return group;
		}

		public static TextField CreateTextField(string value = "")
		{
			TextField textField = new TextField()
			{
				value = value,
			};

			return textField;
		}

		public static TextField CreateTextArea(string value = "")
		{
			TextField textArea = CreateTextField(value);
			textArea.multiline = true;
			return textArea;
		}

		public static IntegerField CreateIntegerField(int value = 0)
		{
			return new IntegerField() { value = value };
		}

		public static PopupField<T> CreatePopupField<T>(List<T> values, int selected = 0)
		{
			return new PopupField<T>(values, selected);
		}

		public static EnumField CreateEnumField(Enum value)
		{
			return new EnumField(value);
		}

		public static Toggle CreateCheckBox(bool value = false, string label = "")
		{
			return new Toggle(label)
			{
				value = value
			};
		}

		public static Label CreateLabel(string value)
		{
			return new Label(value);
		}

		public static Port CreateInputPort(this GraphNode node)
		{
			Port newPort = node.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
			newPort.portName = "";
			return newPort;
		}

		public static Port CreateOutputPort(this GraphNode node, string label = "")
		{
			Port newPort = node.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
			newPort.portName = label;
			return newPort;
		}
	}
}
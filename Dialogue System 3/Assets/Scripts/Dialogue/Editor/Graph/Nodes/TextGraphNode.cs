using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;
using VirtualDeviants.Dialogue.Nodes;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes
{
	public class TextGraphNode : GraphNode
	{
		private const string TextNodeDataContainer = "text-node__data-container";
		private const string TextNodeInputField = "text-node__input-field";
		private const string TextNodeName = "text-node__name";
		private const string TextNodeText = "text-node__text";

		private TextField _speakerField;
		private TextField _textField;
		
		private TextNode Data => template as TextNode;

		public TextGraphNode(string nodeName, NodeTemplate template) : base(nodeName, template)
		{
		}

		public override void UpdateTemplateData()
		{
			Data.speaker = _speakerField.value;
			Data.text = _textField.value;
		}

		public override void Draw(Vector2 initialPosition)
		{
			AddInputPort();
			AddOutputPort();

			_speakerField = VisualElementFactory.CreateTextField(Data.speaker);
			_speakerField.AddStyleClass(TextNodeInputField, TextNodeName);
			
			_textField = VisualElementFactory.CreateTextArea(Data.text);
			_textField.AddStyleClass(TextNodeInputField, TextNodeText);

			customDataContainer.AddRange(_speakerField, _textField);
			customDataContainer.AddStyleClass(TextNodeDataContainer);

			base.Draw(initialPosition);
		}
	}
}
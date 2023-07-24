using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes
{
	public class EndGraphNode : GraphNode
	{
		private const string EndNode = "end-node";
		
		public EndGraphNode(string nodeName, NodeTemplate template) : base(nodeName, template)
		{
		}

		public override void UpdateTemplateData() { }

		public override void Draw(Vector2 initialPosition)
		{
			AddInputPort();

			VisualElement red = new VisualElement();
			red.AddStyleClass(EndNode);
			customDataContainer.Add(red);

			base.Draw(initialPosition);
		}
	}
}
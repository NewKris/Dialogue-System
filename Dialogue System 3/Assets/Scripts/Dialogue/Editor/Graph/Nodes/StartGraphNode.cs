using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Utility;

namespace VirtualDeviants.Dialogue.Editor.Graph.Nodes
{
	public class StartGraphNode : GraphNode
	{
		private const string StartNode = "start-node";
		
		public StartGraphNode(string nodeName, NodeTemplate template) : base(nodeName, template) { }

		public override void UpdateTemplateData() { }

		public override void Draw(Vector2 initialPosition)
		{
			AddOutputPort();

			VisualElement green = new VisualElement();
			green.AddStyleClass(StartNode);
			customDataContainer.Add(green);
			
			base.Draw(initialPosition);
		}
	}
}
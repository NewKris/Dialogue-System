using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.Dialogue.Editor.Helpers;

namespace VirtualDeviants.Dialogue.Editor.Nodes
{
	public class ActorNode : GraphNode
	{

		private const string ObjectContainerStyle = "ds-node_actor-container";
		private const string ObjectFieldStyle = "ds-node_actor-object";

		private GameObject _actorPrefab;

		public GameObject Actor => _actorPrefab;

		public ActorNode(GameObject actorPrefab = null, string nodeName = "Show Actor") 
			: base(nodeName)
		{
			_actorPrefab = actorPrefab;
		}

		public override void Draw(Vector2 position)
		{
			base.Draw(position);
			
			AddInputPort();
			AddOutputPort();

			VisualElement customDataContainer = new VisualElement();
			customDataContainer.AddStyleClasses(ObjectContainerStyle);

			ObjectField actorField = ElementUtility.CreateObjectField(_actorPrefab);
			actorField.AddStyleClasses(ObjectFieldStyle);
			actorField.OnValueChanged(UpdateValue);
			
			customDataContainer.Add(actorField);
			
			extensionContainer.Add(customDataContainer);
			RefreshExpandedState();
		}

		private void UpdateValue(GameObject value)
		{
			_actorPrefab = value;
			
			if(value)
				ShowPreview(value);
			else
				HidePreview();
		}

		private void ShowPreview(GameObject value)
		{
		}

		private void HidePreview()
		{
		}
		
	}
}
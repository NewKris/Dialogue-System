using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace VirtualDeviants.Dialogue.Editor.Utility {
	public static class VisualElementExtensions {
		public static void AddStyleClass(this VisualElement element, params string[] styles) {
			foreach (string style in styles) {
				element.AddToClassList(style);
			}
		}

		public static void LoadStyleSheet(this VisualElement element, params string[] styleSheets) {
			foreach (string styleSheetName in styleSheets) {
				StyleSheet styleSheet = EditorGUIUtility.Load(styleSheetName) as StyleSheet;
				element.styleSheets.Add(styleSheet);
			}
		}

		public static void AddRange(this VisualElement visualElement, params VisualElement[] children) {
			foreach (VisualElement child in children) {
				visualElement.Add(child);
			}
		}

		public static void PrintHierarchy(this VisualElement root) {
			Debug.Log("Hierarchy\n" + root.hierarchy.GetHierarchy());
		}

		private static string GetHierarchy(this VisualElement.Hierarchy hierarchy, int level = 0) {
			string name = string.IsNullOrEmpty(hierarchy.parent.name) 
				? hierarchy.parent.GetType().ToString() 
				: hierarchy.parent.name;
			
			string line = new string('_', level) + name + "\n";

			foreach (VisualElement child in hierarchy.Children()) {
				line += child.hierarchy.GetHierarchy(level + 1);
			}

			return line;
		}
	}
}
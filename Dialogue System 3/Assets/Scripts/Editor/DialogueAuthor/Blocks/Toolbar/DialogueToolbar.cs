using UnityEditor.UIElements;
using UnityEngine.UIElements;
using VirtualDeviants.Editor.DialogueAuthor.Utility;

namespace VirtualDeviants.Editor.DialogueAuthor.Blocks.Toolbar {
    public class DialogueToolbar : VisualElement {
        public static VisualElement Create(ButtonConfig[] buttons) {
            UnityEditor.UIElements.Toolbar toolbar = new UnityEditor.UIElements.Toolbar();
            toolbar.AddStyleClass(StyleBlackBoard.NavigationBar);

            foreach (ButtonConfig config in buttons) {
                ToolbarButton button = VisualElementFactory.CreateToolbarButton(config.text, () => config.callback.Execute());
                button.AddStyleClass(StyleBlackBoard.NavigationBarTabButton);
                toolbar.Add(button);
            }

            return toolbar;
        }
    }
}
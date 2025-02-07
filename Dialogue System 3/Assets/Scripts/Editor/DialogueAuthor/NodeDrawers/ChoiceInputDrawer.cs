using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VirtualDeviants.DialogueAuthor.MemberAttributes;
using VirtualDeviants.Editor.DialogueAuthor.Attributes;
using VirtualDeviants.Editor.DialogueAuthor.Blocks;
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

            List<string> choices = fieldInfo.GetValue(objectInstance) as List<string>;
            choices ??= new List<string>();

            return new ChoiceContainer(node, choices);
        }
    }
}
using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yonii.Unity.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidDrawer : PropertyDrawer 
    {
        private static readonly string[] GuidParts = 
        {
            nameof(SerializableGuid.Part1),
            nameof(SerializableGuid.Part2),
            nameof(SerializableGuid.Part3), 
            nameof(SerializableGuid.Part4)
        };

        private static SerializedProperty[] GetGuidParts(SerializedProperty property) 
        {
            var values = new SerializedProperty[GuidParts.Length];
            for (var i = 0; i < GuidParts.Length; i++)
            {
                values[i] = property.FindPropertyRelative(GuidParts[i]);
            }

            return values;
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            root.style.flexDirection = FlexDirection.Column;
            root.style.alignItems = Align.Center;
            root.style.marginBottom = 8;
            root.style.marginTop = 8;
            root.style.paddingBottom = 8;
            root.style.paddingTop = 8;

            var guidParts = GetGuidParts(property);

            var guidField = new Label();
            guidField.style.unityTextAlign = TextAnchor.MiddleCenter;

            guidField.text = guidParts.All(x => x != null) 
                ? BuildGuidString(guidParts) 
                : "GUID Not Initialized";

            root.Add(guidField);

            var buttonRow = new VisualElement();
            buttonRow.style.flexDirection = FlexDirection.Row;
            buttonRow.style.justifyContent = Justify.Center;
            buttonRow.style.alignItems = Align.Center;
            buttonRow.style.marginTop = 8;

            var copyButton = new Button(() => CopyGuid(property, guidParts, guidField)) { text = "Copy GUID" };
            var resetButton = new Button(() => ResetGuid(property, guidParts, guidField)) { text = "Reset GUID" };
            var regenButton = new Button(() => RegenerateGuid(property, guidParts, guidField)) { text = "Regenerate GUID" };

            // Optionally add some spacing
            copyButton.style.marginLeft = 10;
            copyButton.style.marginRight = 10;
            resetButton.style.marginLeft = 10;
            resetButton.style.marginRight = 10;
            regenButton.style.marginLeft = 10;
            regenButton.style.marginRight = 10;

            buttonRow.Add(copyButton);
            buttonRow.Add(resetButton);
            buttonRow.Add(regenButton);

            root.Add(buttonRow);

            return root;
        }

        private static void CopyGuid(SerializedProperty property, SerializedProperty[] guidParts, Label output)
        {
            if (guidParts.Any(x => x == null))
            {
                return;
            }

            var guid = BuildGuidString(guidParts);
            EditorGUIUtility.systemCopyBuffer = guid;
            Debug.Log($"GUID copied to clipboard: {guid}");
        }

        private static void ResetGuid(SerializedProperty property, SerializedProperty[] guidParts, Label output)
        {
            if (!EditorUtility.DisplayDialog("Reset GUID", "Are you sure you want to reset the GUID?", "Yes", "No"))
            {
                return;
            }

            foreach (var part in guidParts)
            {
                part.uintValue = 0;
            }
            property.serializedObject.ApplyModifiedProperties();
            output.text = "GUID Not Initialized";
            Debug.Log("GUID has been reset.");
        }

        private static void RegenerateGuid(SerializedProperty property, SerializedProperty[] guidParts, Label output)
        {
            if (!EditorUtility.DisplayDialog("Regenerate GUID", "Are you sure you want to regenerate the GUID?", "Yes", "No"))
            {
                return;
            }

            var bytes = Guid.NewGuid().ToByteArray();

            for (var i = 0; i < GuidParts.Length; i++)
            {
                guidParts[i].uintValue = BitConverter.ToUInt32(bytes, i * 4);
            }

            property.serializedObject.ApplyModifiedProperties();
            output.text = BuildGuidString(guidParts);
            Debug.Log("GUID has been regenerated.");
        }

        private static string BuildGuidString(SerializedProperty[] guidParts)
        {
            return new StringBuilder()
                .AppendFormat("{0:X8}", guidParts[0].uintValue)
                .AppendFormat("{0:X8}", guidParts[1].uintValue)
                .AppendFormat("{0:X8}", guidParts[2].uintValue)
                .AppendFormat("{0:X8}", guidParts[3].uintValue)
                .ToString();
        }
    }
}
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace EditorRename
{
    [CanEditMultipleObjects]
    [CustomPropertyDrawer(typeof(RenameAttribute))]
    public class RenameEditor : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RenameAttribute rename = (RenameAttribute)attribute;
            label.text = rename.Name;

            bool isElement = Regex.IsMatch(property.displayName, "Element \\d+");
            if (isElement)
                label.text = property.displayName;
            if (property.propertyType == SerializedPropertyType.Enum)
            {
                DrawEnum(position, property, label);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        private void DrawEnum(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();

            Type type = fieldInfo.FieldType;
            string[] names = property.enumNames;
            string[] values = new string[names.Length];
            Array.Copy(names, values, names.Length);
            while (type != null && type.IsArray)
                type = type.GetElementType();

            for (int i = 0; i < names.Length; i++)
            {
                FieldInfo info = type.GetField(names[i]);
                RenameAttribute[] atts = (RenameAttribute[])info.GetCustomAttributes(typeof(RenameAttribute), true);
                if (atts.Length != 0)
                    values[i] = atts[0].Name;
            }

            int index = EditorGUI.Popup(position, label.text, property.enumValueIndex, values);
            if (EditorGUI.EndChangeCheck() && index != -1)
                property.enumValueIndex = index;
        }

    }

}


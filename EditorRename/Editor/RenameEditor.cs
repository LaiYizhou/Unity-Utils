using UnityEngine;
using System;
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Reflection;
#endif 

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RenameAttribute))]
public class RenameDrawer : PropertyDrawer
{
    private RenameAttribute renameAttribute => (RenameAttribute)attribute;
    private readonly Dictionary<string, string> customEnumNames = new Dictionary<string, string>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SetUpCustomEnumNames(property, property.enumNames);
        if (property.propertyType == SerializedPropertyType.Enum)
        {
            EditorGUI.BeginChangeCheck();
            string[] displayedOptions = property.enumNames
                .Where(enumName => customEnumNames.ContainsKey(enumName))
                .Select<string, string>(enumName => customEnumNames[enumName])
                .ToArray();
            int selectedIndex = EditorGUI.Popup(position, renameAttribute.Name, property.enumValueIndex, displayedOptions);
            if (EditorGUI.EndChangeCheck())
            {
                property.enumValueIndex = selectedIndex;
            }
        }
    }

    private void SetUpCustomEnumNames(SerializedProperty property, string[] enumNames)
    {
        Type type = property.serializedObject.targetObject.GetType();
        foreach (FieldInfo fieldInfo in type.GetFields())
        {
            object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(RenameAttribute), false);
            foreach (RenameAttribute customAttribute in customAttributes)
            {
                Type enumType = fieldInfo.FieldType;
                foreach (string enumName in enumNames)
                {
                    FieldInfo field = enumType.GetField(enumName);
                    if (field == null) continue;
                    RenameAttribute[] attrs = (RenameAttribute[])field.GetCustomAttributes(customAttribute.GetType(), false);

                    if (!customEnumNames.ContainsKey(enumName))
                    {
                        foreach (RenameAttribute labelAttribute in attrs)
                        {
                            customEnumNames.Add(enumName, labelAttribute.Name);
                        }
                    }
                }
            }
        }
    }
}
#endif

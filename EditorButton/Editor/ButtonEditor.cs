using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EditorButton
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class ObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var methods = this.target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetParameters().Length == 0);
            foreach (var method in methods)
            {
                var ba = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));

                if (ba != null)
                {
                    var wasEnabled = GUI.enabled;
                    GUI.enabled = ba.Mode == ButtonMode.Always || (EditorApplication.isPlaying ? ba.Mode == ButtonMode.PlayMode : ba.Mode == ButtonMode.EditorMode);

                    var buttonName = String.IsNullOrEmpty(ba.Name) ? ObjectNames.NicifyVariableName(method.Name) : ba.Name;
                    if (GUILayout.Button(buttonName))
                    {
                        foreach (var t in this.targets)
                        {
                            method.Invoke(t, null);
                        }
                    }

                    GUI.enabled = wasEnabled;
                }
            }

            
        }
    }
}

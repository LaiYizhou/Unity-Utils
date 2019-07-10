using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace EditorRename
{

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class RenameAttribute : PropertyAttribute
    {
        private string name = null;
        public string Name { get { return name; } }

        public RenameAttribute(string name)
        {
            this.name = name;
        }
    }

}



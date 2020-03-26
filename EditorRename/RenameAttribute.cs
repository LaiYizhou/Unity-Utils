using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
public sealed class RenameAttribute : PropertyAttribute
{
    private string name = null;
    public string Name { get { return name; } }

    public RenameAttribute(string name)
    {
        this.name = name;
    }
}
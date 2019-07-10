using System;

namespace EditorButton
{
    public enum ButtonMode
    {
        Always,
        PlayMode,
        EditorMode
    }

    /// <summary>
    /// Attribute to create a button in the inspector for calling the method it is attached to.
    /// The method must have no arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ButtonAttribute : Attribute
    {
        private string name = null;
        private ButtonMode mode = ButtonMode.Always;

        public string Name { get { return name; } }
        public ButtonMode Mode { get { return mode; } }

        public ButtonAttribute()
        {
        }

        public ButtonAttribute(string name)
        {
            this.name = name;
        }

        public ButtonAttribute(ButtonMode mode)
        {
            this.mode = mode;
        }

        public ButtonAttribute(string name, ButtonMode mode)
        {
            this.name = name;
            this.mode = mode;
        }

    }
}


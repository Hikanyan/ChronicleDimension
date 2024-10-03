using System;

namespace ChronicleDimensionProject.UI
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class AutoAssignByNameAttribute : Attribute
    {
        public AutoAssignByNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
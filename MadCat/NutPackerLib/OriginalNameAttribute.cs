using System;

namespace NutPackerLib
{
    /// <summary>
    /// Original name of something.
    /// </summary>
    public class OriginalNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public OriginalNameAttribute(string name)
        {
            Name = name;
        }
    }
}

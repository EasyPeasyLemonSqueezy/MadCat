using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutPackerLib
{
    public class OriginalNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public OriginalNameAttribute(string name)
        {
            Name = name;
        }
    }
}

using NutEngine.Physics.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        public static bool Collide(AABB a, Circle b, out Manifold manifold)
        {
            manifold = new Manifold();
            return true;
        }
    }
}

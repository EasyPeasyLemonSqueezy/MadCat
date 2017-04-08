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
        public static bool Collide(Circle a, AABB b, out Manifold manifold)
        {
            manifold = new Manifold();
            return true;
        }
    }
}

using NutEngine;
using NutEngine.Physics;

namespace MadCat
{
    public static class Director
    {
        public static Node World { get; set; }
        public static EntityManager Entities { get; set; }
        public static BodiesManager Bodies { get; set; }
    }
}

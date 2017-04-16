namespace NutEngine.Physics
{
    public class Collision
    {
        public Body A { get; set; }
        public Body B { get; set; }
        public Manifold Manifold { get; set; }

        public Collision(Body a, Body b, Manifold manifold)
        {
            A = a;
            B = b;
            Manifold = manifold;
        }
    }
}

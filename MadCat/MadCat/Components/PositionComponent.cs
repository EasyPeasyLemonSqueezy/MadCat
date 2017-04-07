using NutEngine;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class PositionComponent : Component
    {
        public Vector2 Position { get; set; }

        public PositionComponent(Vector2 position)
        {
            Position = position;
        }

        public override void Update(float deltaTime)
        {
        }
    }
}

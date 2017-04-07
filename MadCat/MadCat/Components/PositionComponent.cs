using System;
using NutEngine;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class PositionComponent : Component
    {
        public Vector2 Position { get; set; } 

        public override void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}

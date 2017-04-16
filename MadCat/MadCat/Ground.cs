﻿using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Ground
    {
        public Sprite Sprite { get; set; }
        public Body Body { get; private set; }

        public Ground()
        {
            Sprite = Assets.Ground;

            var maxPoint = new Vector2(Sprite.TextureRegion.Frame.Size.X,
                                       Sprite.TextureRegion.Frame.Size.Y);

            var offset = new Vector2(300, 300);

            var shape = new AABB(offset, offset + maxPoint);

            Body = new Body(shape) {
                Owner = this,
                Position = shape.Min,
                Velocity = new Vector2(.0001f)
            };

            Body.Mass.MassInv = 100;
            Body.Material.Restitution = .2f;

            Update();
        }

        public void Update()
        {
            Sprite.Position = Body.Shape.Sector.Min = Body.Position;
        }
    }
}

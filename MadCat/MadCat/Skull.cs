﻿using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Skull
    {
        public Sprite Sprite { get; set; }
        public RigidBody<AABB> Body { get; private set; }

        public Skull(Vector2 position)
        {
            Sprite = Assets.Skull;

            var size = Sprite.TextureRegion.Frame.Size;
            Body = new RigidBody<AABB>(new AABB(new Vector2(size.X, size.Y) / 2)) {
                Position = position,
                Owner = this,
            };

            Body.Mass.MassInv = 1;
            Body.Material.Restitution = .8f;

            Update();
        }

        public void Update()
        {
            Sprite.Position = Body.Position;
        }
    }
}
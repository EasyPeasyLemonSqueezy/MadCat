using NutEngine;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace MadCat
{
    public class AimComponent : Component
    {
        public Sprite Aim { get; set; }
        public float Angle { get; private set; }

        public AimComponent(Node node)
        {
            Aim = Assets.Aim;
            Aim.Scale = new Vector2(0.1f, 0.1f);
            node.AddChild(Aim);
        }

        public override void Update(float deltaTime)
        {
            var mouseState = Mouse.GetState();
            Aim.Position = new Vector2(mouseState.X, mouseState.Y);

            var sprite = Entity.GetComponent<SpriteComponent>().Sprite;

            var vec = (Aim.Position - sprite.Position);
            Angle = (float)Math.Atan2(vec.Y, vec.X);

            sprite.Rotation = Angle;
            Aim.Rotation = Angle;
        }
    }
}

using System;
using NutEngine;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class WeaponComponent : Component
    {
        public bool Shooting { get; set; }

        private float reloadTime = 0.15f;
        private float currentTime = 0;

        public override void Update(float deltaTime)
        {
            var body = Entity.GetComponent<BodyComponent>().Body;
            var angle = Entity.GetComponent<AimComponent>().Angle;

            if (Shooting && currentTime == 0) {
                var direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                var bullet = new Bullet(
                    body.Position,
                    direction
                );

                currentTime = 0.001f;
            }
            
            if (currentTime != 0) {
                currentTime += deltaTime;

                if (currentTime > reloadTime) {
                    currentTime = 0;
                }
            }
        }
    }
}

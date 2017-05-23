﻿using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Hero : Entity
    {
        private RigidBody<Circle> body;

        public Hero(Vector2 position)
        {
            var sprite = Assets.Hero;
            sprite.Scale = new Vector2(0.2f, 0.2f);

            body = new RigidBody<Circle>(new Circle(20f)) {
                Position = position,
                Owner = this
            };
            body.Mass.Mass = 5;
            body.Material.Restitution = 0.5f;

            AddComponents(
                new InputComponent(),
                new WeaponComponent(),
                new BodyComponent(body),
                new SpriteComponent(sprite),
                new AimComponent(Director.World),
                new HealthComponent(1)
            );

            Director.Entities.Add(this);
            Director.Bodies.AddBody(body);
            Director.World.AddChild(sprite);

            body.OnCollision = (collided) => {
                if (collided.Owner is Zombie) {
                    var zombie = collided.Owner as Zombie;
                    if (zombie.HasComponent<ZombieComponent>()) {
                        GetComponent<HealthComponent>().Health = 0;
                    }
                }
            };
        }
    }
}

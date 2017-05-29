using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutEngine;
using NutEngine.Physics;
using System;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private EntityManager entities;
        private BodiesManager bodies;

        private Sprite background;

        private Hero hero;

        public DemoScene(Application app) : base(app)
        {
            entities = new EntityManager();
            bodies = new BodiesManager();

            Director.World = World;
            Director.Entities = entities;
            Director.Bodies = bodies;

            Assets.Init(Content);

            background = Assets.Background;
            background.Position = new Vector2(App.ScreenWidth / 2.0f, App.ScreenHeight / 2.0f);
            background.ZOrder = -100;
            World.AddChild(background);

            Vector2[] positions = {
                new Vector2(-100f, 0f),
                new Vector2(960f, 0f),
                new Vector2(0, -100f),
                new Vector2(0, 540f),
            };

            Vector2[] sizes = {
                new Vector2(100f, 540f),
                new Vector2(100f, 540f),
                new Vector2(960f, 100f),
                new Vector2(960f, 100f),
            };

            for (int i = 0; i < 4; i++) {
                var wall1 = new Wall(
                    positions[i],
                    sizes[i]
                );
            }

            hero = new Hero(
                new Vector2(480f, 270f)
            );

            Random random = new Random();

            for (int i = 0; i < 5; i++) {
                var box = new Box(
                    new Vector2(random.Next(960), random.Next(540))
                );
            }
        }

        public override void Update(float deltaTime)
        {
            var keyboardState = NutEngine.Input.Keyboard.State;

            if (keyboardState.IsKeyDown(Keys.Escape)) {
                App.Exit();
            }

            if (keyboardState.IsKeyPressedRightNow(Keys.Space)) {
                Random random = new Random();

                for (int i = 0; i < 20; i++) {
                    var zombie = new Zombie(
                        new Vector2(200 + random.Next(760), 200 + random.Next(340)),
                        hero
                    );
                }
            }

            if (keyboardState.IsKeyPressedRightNow(Keys.Back)) {
                foreach (var entity in Director.Entities.Entities) {
                    if (entity is Zombie ||
                        entity is Blood) {
                        entity.Invalid = true;
                    }
                }
            }

            entities.Update(deltaTime);
            bodies.Update(deltaTime);
        }
    }
}
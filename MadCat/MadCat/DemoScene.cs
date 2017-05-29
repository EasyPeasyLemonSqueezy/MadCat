using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutEngine;
using NutPacker.Content;
using NutEngine.Physics;
using System;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private Sprite background;

        private EntityManager manager;
        private BodiesManager bodies;

        private Character character;
        private Map map;

        public DemoScene(Application app) : base(app)
        {
            Assets.Init(Content);
            App.IsMouseVisible = false;

            background = new Sprite(Assets.Texture, Graveyard.Tiles.BG) {
                  Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2)
                , Scale = new Vector2(.5f, .5f)
            };
            World.AddChild(background);

            manager = new EntityManager();
            bodies = new BodiesManager();

            manager.AddDependency<AnimationComponent>(
               typeof(VelocityComponent),
               typeof(CharacterComponent)
           );

            manager.AddDependency<ColliderComponent>(
                typeof(VelocityComponent),
                typeof(CharacterComponent)
            );

            manager.AddDependency<GravitationComponent>(
                typeof(CharacterComponent)
            );

            manager.AddDependency<SpriteComponent>(
                typeof(VelocityComponent)
            );

            manager.AddDependency<VelocityComponent>(
                typeof(GravitationComponent),
                typeof(CharacterComponent)
            );

            manager.AddDependency<CharacterComponent>(
            );

            manager.AddDependency<PositionComponent>(
            );

            character = new Character(World, manager, bodies);
            bodies.AddBody(character.Body);

            map = new Map(World, manager, bodies);
        }

        public override void Update(float deltaTime)
        {
            var keyboardState = NutInput.Keyboard.State;

            /// Input
            character.Input(keyboardState);

            bodies.Update(deltaTime);
            manager.Update(deltaTime);

            var position = character.GetComponent<ColliderComponent>().Body;
            background.Position = new Vector2(position.Position.X, App.ScreenHeight / 2);
            Camera.Position = new Vector2(position.Position.X - App.ScreenWidth / 2, -20);

            if (keyboardState.IsKeyPressedRightNow(Keys.Escape)) {
                App.Scenes.Push(new PauseScene(App));
            }
        }
    }
}
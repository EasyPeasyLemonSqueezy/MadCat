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
            App.IsMouseVisible = false;
            Assets.Init(Content);

            background = new Sprite(Assets.Texture, Graveyard.Tiles.BG) {
                  Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2)
                , Scale = new Vector2(.5f, .5f)
            };
            World.AddChild(background);

            manager = new EntityManager();
            bodies = new BodiesManager();

            EntityManager.AddDependency<AnimationComponent>(
               typeof(VelocityComponent),
               typeof(CharacterComponent)
           );

            EntityManager.AddDependency<ColliderComponent>(
                typeof(VelocityComponent),
                typeof(CharacterComponent)
            );

            EntityManager.AddDependency<GravitationComponent>(
                typeof(CharacterComponent)
            );

            EntityManager.AddDependency<SpriteComponent>(
                typeof(VelocityComponent)
            );

            EntityManager.AddDependency<VelocityComponent>(
                typeof(GravitationComponent),
                typeof(CharacterComponent)
            );

            EntityManager.AddDependency<CharacterComponent>(
            );

            EntityManager.AddDependency<PositionComponent>(
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
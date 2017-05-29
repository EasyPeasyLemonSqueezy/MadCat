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

        private Character[] characters;
        private Map map;

        public DemoScene(Application app) : base(app)
        {
            Assets.Init(Content);

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

            /// Create Characters.
            characters = new Character[3];

            characters[0] = new Character(World, manager, bodies);

            characters[1] = new Character(World, manager, bodies);
            characters[1].SetControls(new CharacterComponent.Controls {
                      RunRightKey = Keys.D
                    , RunLeftKey = Keys.A
                    , JumpKey = Keys.W
                    , ShootKey = Keys.Q
                    , MeleeKey = Keys.F
                    , SlideKey = Keys.S
            });

            characters[2] = new Character(World, manager, bodies);
            characters[2].SetControls(new CharacterComponent.Controls {
                      RunRightKey = Keys.L
                    , RunLeftKey = Keys.K
                    , JumpKey = Keys.O
                    , ShootKey = Keys.J
                    , MeleeKey = Keys.P
                    , SlideKey = Keys.M
            });

            bodies.AddBody(characters[0].Body);
            bodies.AddBody(characters[1].Body);
            bodies.AddBody(characters[2].Body);

            map = new Map(World, manager, bodies);
        }

        public override void Update(float deltaTime)
        {
            var keyboardState = NutInput.Keyboard.State;
            
            /// Input
            foreach (var character in characters) {
                character.Input(keyboardState);
            }

            bodies.Update(deltaTime);
            manager.Update(deltaTime);

            var position = characters[0].GetComponent<ColliderComponent>().Body;
            background.Position = new Vector2(position.Position.X, App.ScreenHeight / 2);
            Camera.Position = new Vector2(position.Position.X - App.ScreenWidth / 2, -20);

            if (keyboardState.IsKeyPressedRightNow(Keys.Escape)) {
                App.Scenes.Push(new PauseScene(App));
            }
        }
    }
}
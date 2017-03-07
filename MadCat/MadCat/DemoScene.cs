using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutEngine;
using NutPacker.Content;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private Texture2D Texture;
        private Node Ground;
        private Character Character1;
        private Character Character2;
        private Character Character3;

        public DemoScene(Application app) : base(app)
        {
            Texture = Content.Load<Texture2D>("Demo");

            var background = new Sprite(Texture, Graveyard.Tiles.BG) {
                  Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2)
                , Scale = new Vector2(.5f, .5f)
            };
            World.AddChild(background);

            Ground = new Node() {
                Position = new Vector2(0, App.ScreenHeight)
            };

            for (int i = 0; i < 10; i++) {
                var sprite = new Sprite(Texture, Graveyard.Tiles.Tile_2_) {
                    Position = new Vector2(i * Graveyard.Tiles.Tile_2_.Width, 0)
                };

                Ground.AddChild(sprite);
            }

            Ground.AddChild(new Sprite(Texture, Graveyard.Objects.TombStone_1_) {
                  Position = new Vector2(200, -105)
                , Scale = new Vector2(1.5f, 1.5f)
            });
            Ground.AddChild(new Sprite(Texture, Graveyard.Objects.TombStone_2_) {
                  Position = new Vector2(700, -120)
                , Scale = new Vector2(1.5f, 1.5f)
            });
            Ground.AddChild(new Sprite(Texture, Graveyard.Objects.Bush_1_) {
                  Position = new Vector2(400, -75)
                , Scale = new Vector2(.5f, .5f)
                , ZOrder = 10
            });

            Character1 = new Character(Texture);
            Character2 = new Character(Texture);
            Character2.SetButtons(Keys.D, Keys.A, Keys.W, Keys.Q, Keys.F, Keys.S);
            Character3 = new Character(Texture);
            Character3.SetButtons(Keys.L, Keys.K, Keys.O, Keys.J, Keys.P, Keys.M);

            Ground.AddChild(Character1);
            Ground.AddChild(Character2);
            Ground.AddChild(Character3);
            World.AddChild(Ground);
        }

        public override void Update(float deltaTime)
        {
            var keyboardState = NutInput.Keyboard.GetState();

            Character1.Input(keyboardState);
            Character2.Input(keyboardState);
            Character3.Input(keyboardState);

            Character1.Update(deltaTime);
            Character2.Update(deltaTime);
            Character3.Update(deltaTime);
        }
    }
}
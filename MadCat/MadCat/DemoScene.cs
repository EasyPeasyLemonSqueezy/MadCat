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
        private float ScreenHeight;
        private float ScreenWidth;

        private Texture2D TexturePlatform;
        private Texture2D Texture;
        private Node Ground;
        private LastAdventureGirl AdventureGirl1;
        private LastAdventureGirl AdventureGirl2;
        private LastAdventureGirl AdventureGirl3;

        public DemoScene(Application app) : base(app)
        {
            ScreenHeight = app.GraphicsDevice.PresentationParameters.BackBufferHeight;
            ScreenWidth = app.GraphicsDevice.PresentationParameters.BackBufferWidth;

            Texture = Content.Load<Texture2D>("Demo");
            TexturePlatform = Content.Load<Texture2D>("WPlatform");


            var background = new Sprite(Texture, Graveyard.Tiles.BG) {
                  Position = new Vector2(ScreenWidth / 2, ScreenHeight / 2)
                , Scale = new Vector2(.5f, .5f)
            };
            World.AddChild(background);

            Ground = new Node() {
                Position = new Vector2(0, ScreenHeight)
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
            Ground.AddChild(new Sprite(TexturePlatform, Graveyard.Objects.Bush_1_)  // просто прямоугольник белый для тестов
            {
                Position = new Vector2(800, -256)       //188 x 94            // 100 x 152
                ,
                Scale = new Vector2(1f, 1f)
                ,
                ZOrder = 10
            });

            AdventureGirl1 = new LastAdventureGirl(Texture, Keys.LeftControl, Keys.Right, Keys.Left, Keys.Up, Keys.LeftShift, Keys.Down);
            AdventureGirl2 = new LastAdventureGirl(Texture, Keys.Q, Keys.D, Keys.A, Keys.W, Keys.F, Keys.S);
            AdventureGirl3 = new LastAdventureGirl(Texture, Keys.J, Keys.L, Keys.K, Keys.O, Keys.P, Keys.M);

            Ground.AddChild(AdventureGirl1);
            Ground.AddChild(AdventureGirl2);
            Ground.AddChild(AdventureGirl3);
            World.AddChild(Ground);
        }

        public override void Update(float deltaTime)
        {
            AdventureGirl2.Update(deltaTime);
            AdventureGirl3.Update(deltaTime);
            AdventureGirl1.Update(deltaTime);       
        }
    }
}
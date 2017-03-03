using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NutEngine;
using System.Collections.Generic;


using NutEngine;
using NutPacker;
using NutPacker.Content;

namespace MadCat
{
    class MenuScene : Scene
    {
        private int screenWidth;
        private int screenHeight;

        class Cloud
        {
            public Sprite sprite;
            public Vector2 velocity;
            public int direction;
        }

        private Random random;
        private Texture2D buttonPlayTexture;
        private Texture2D cloudTexture;
        private Texture2D backgroundTexture;
        private List<Cloud> clouds;

        private Texture2D Stickers;
        private SpriteSheet AdventureTime;
        private SpriteSheet GravityFalls;

        private Animation CurrentAnimation;

        private void AddCloud()
        {
            Cloud cat = new Cloud();

            cat.velocity = new Vector2(500f,0f);
            cat.sprite = new Sprite(cloudTexture);

            cat.sprite.Position = new Vector2(0, 60);
            
            var scale = 0.3f;

            cat.sprite.Scale = new Vector2(scale, scale);


            clouds.Add(cat);
            RootNode.AddChild(cat.sprite);
        }

        public MenuScene(Application app) : base(app)
        {
            screenHeight = app.GraphicsDevice.PresentationParameters.BackBufferHeight;
            screenWidth = app.GraphicsDevice.PresentationParameters.BackBufferWidth;

            buttonPlayTexture = Content.Load<Texture2D>("buttonPlay"); /// Загрузить картинку
            cloudTexture = Content.Load<Texture2D>("clouds");
            backgroundTexture = Content.Load<Texture2D>("backgroud");

            var background = new Cloud();
            background.sprite = new Sprite(backgroundTexture);
            background.sprite.Position = new Vector2(400f, 220f);
            var scale = 0.75f;
            background.sprite.Scale = new Vector2(scale, scale);

            RootNode.AddChild(background.sprite);

            Stickers = Content.Load<Texture2D>("Stickers");

            AdventureTime = new Stickers.AdventureTime();
            GravityFalls = new Stickers.GravityFalls();

            CurrentAnimation = new Animation(Stickers, AdventureTime)
            {
                Duration = 10f
                ,
                Repeat = false
                ,
                Position = new Vector2(650f, 350f)
                ,
                Scale = new Vector2(0.5f, 0.5f)
            };

            RootNode.AddChild(CurrentAnimation);

            clouds = new List<Cloud>();
            

         



            /// Just testing
            var buttonPlay = new Cloud();
            buttonPlay.sprite = new Sprite(buttonPlayTexture);         
            buttonPlay.sprite.Position = new Vector2(150, screenHeight/2-50);

            scale = 0.75f;
            buttonPlay.sprite.Scale = new Vector2(scale, scale);

            RootNode.AddChild(buttonPlay.sprite);

            //clouds.Add(cloud1);

        }

        public override void Update(float deltaTime)
        {
            deltaTime *= 0.228f;

            CurrentAnimation.Update(deltaTime);

            if (!CurrentAnimation.Enabled)
            {
                if (CurrentAnimation.SpriteSheet is Stickers.AdventureTime)
                {
                    CurrentAnimation.SpriteSheet = GravityFalls;
                }
                else
                {
                    CurrentAnimation.SpriteSheet = AdventureTime;
                }

                CurrentAnimation.Enabled = true;
            }


            /// Выйти при нажатии escape. Точно также делается
            /// любая другая обработка нажатий.
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                App.Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                AddCloud();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                var startScene = new RotatingCatsScene(App); ///Запустить сцену игры, демо версии
                App.runWithScene(startScene);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Delete) && clouds.Count > 0)
            {
                RootNode.RemoveChild(clouds[0].sprite);
                clouds.RemoveAt(0);
            }

            foreach (var cloud in clouds)
            {
                cloud.velocity = new Vector2(cloud.velocity.X, (float)Math.Sin(cloud.sprite.Position.X * 0.01f) * 500.0f);

                cloud.sprite.Position += cloud.velocity * deltaTime;

                /// Отскакивание от границ окна.
                if (cloud.sprite.Position.X < 0.0f || cloud.sprite.Position.X > screenWidth)
                {
                    cloud.velocity.X *= -1.0f;
                    cloud.sprite.Position = new  Vector2(0.0f,60.0f);
                }

                if (cloud.sprite.Position.Y < 0.0f || cloud.sprite.Position.Y > screenHeight)
                {
                    cloud.velocity.Y *= +1.0f;
                }

                //cloud.sprite.Position = new Vector2(cloud.sprite.Position.X, cloud.sprite.Position.Y + (float)Math.Sin(cloud.sprite.Position.X * 20.0f) * 100.0f);
                //SomeNumber = SomeNumber + deltaTime
                //cloud.sprite.Position += new Vector2(0, Math.Sin());

            }
        }

        private float SomeNumber = 0f;


    }
}

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;

namespace MadCat
{
    public static class Assets
    {
        public static Texture2D Texture;
        public static Texture2D TextureBullet;
        public static SpriteFont Font;
        public static Sprite NutLogo;

        public static Animation AdventureGirlIdle;
        public static Animation AdventureGirlJump;
        public static Animation AdventureGirlMelee;
        public static Animation AdventureGirlRun;
        public static Animation AdventureGirlShoot;
        public static Animation AdventureGirlSlide;

        public static void Init(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("Demo");
            TextureBullet = Content.Load<Texture2D>("bullet");
            var textureLogo = Content.Load<Texture2D>("nutengine");
            Font = Content.Load<SpriteFont>("myFont");

            AdventureGirlIdle
                = new Animation(Texture, new NutPacker.Content.AdventureGirl.Idle()) {
                    Duration = .8f
                };
            AdventureGirlJump
                = new Animation(Texture, new NutPacker.Content.AdventureGirl.Jump()) {
                    Repeat = false,
                    Duration = .5f
                };
            AdventureGirlMelee
                = new Animation(Texture, new NutPacker.Content.AdventureGirl.Melee()) {
                    Repeat = false,
                    Duration = .35f
                };
            AdventureGirlRun
                = new Animation(Texture, new NutPacker.Content.AdventureGirl.Run()) {
                    Duration = .6f
                };
            AdventureGirlShoot
                = new Animation(Texture, new NutPacker.Content.AdventureGirl.Shoot()) {
                    Duration = .2f,
                    Repeat = false
                };
            AdventureGirlSlide
                = new Animation(Texture, new NutPacker.Content.AdventureGirl.Slide()) {
                    Duration = .5f,
                    Repeat = false
                };

            NutLogo = new Sprite(textureLogo);
        }
    }
}

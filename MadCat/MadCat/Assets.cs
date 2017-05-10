using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;

namespace MadCat
{
    public static class Assets
    {
        public static Texture2D Texture;

        public static void Init(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("Demo");
        }
    }
}

using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutEngine;
using NutEngine.Physics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private BodiesManager Bodies { get; set; }

        private List<Ground> Ground { get; set; }
        private List<Skull> Skulls { get; set; }

        private MouseState PrevMouseState;

        public DemoScene(Application app) : base(app)
        {
            PrevMouseState = Mouse.GetState();

            Assets.Init(Content);

            Bodies = new BodiesManager();
            Ground = new List<Ground>();
            Skulls = new List<Skull>();

            var groundSize = NutPacker.Content.Graveyard.Tiles.Tile_15_.Size.X;

            for (var pos = 0; pos < 960; pos += groundSize) {
                var ground = new Ground(new Vector2(pos, 500));
                Ground.Add(ground);
                World.AddChild(ground.Sprite);
                Bodies.AddBody(ground.Body);
            }

        }

        public override void Update(float dt)
        {
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && PrevMouseState.LeftButton == ButtonState.Released) {
                var skull = new Skull(new Vector2(mouseState.Position.X, mouseState.Position.Y));
                Skulls.Add(skull);
                World.AddChild(skull.Sprite);
                Bodies.AddBody(skull.Body);
            }


            Bodies.Update(dt);


            foreach (var ground in Ground) {
                ground.Update();
            }
            foreach (var skull in Skulls) {
                skull.Update();
            }

            PrevMouseState = mouseState;
        }
    }
}
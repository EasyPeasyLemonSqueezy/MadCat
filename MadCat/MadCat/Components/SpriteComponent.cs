using NutEngine;
using System;

namespace MadCat
{
    public class SpriteComponent : Component, IDisposable
    {
        public Sprite Sprite { get; set; }

        public SpriteComponent(Sprite sprite)
        {
            Sprite = sprite;
        }

        public override void Update(float deltaTime)
        {
            if (Entity.HasComponent<BodyComponent>()) {
                var body = Entity.GetComponent<BodyComponent>().Body;
                Sprite.Position = body.Position;
            }
        }

        public void Dispose()
        {
            Sprite.CommitSuicide();
        }
    }
}

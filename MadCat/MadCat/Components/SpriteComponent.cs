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
            var position = Entity.GetComponent<PositionComponent>();
            Sprite.Position = position.Position;
        }

        public void Dispose()
        {
            Sprite.CommitSuicide();
        }
    }
}

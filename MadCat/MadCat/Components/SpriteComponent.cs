using System;
using NutEngine;

namespace MadCat.Components
{
    public class SpriteComponent : Component
    {
        public Sprite Sprite { get; set; }

        public SpriteComponent(Sprite sprite)
        {
            Sprite = sprite;
        }

        public override void Update(float deltaTime)
        {
            var position = Entity.GetComponent<PositionComponent>();
            Sprite.Position = position.;
        }
    }
}

using System;
using NutEngine;

namespace MadCat
{
    public class AnimationComponent : Component, IDisposable
    {
        public Animation Animation { get; set; }

        public AnimationComponent(Animation animation)
        {
            Animation = animation;
        }

        public override void Update(float deltaTime)
        {
            var position = Entity.GetComponent<ColliderComponent>().Body;
            Animation.Position = position.Position;
            Animation.Update(deltaTime);
        }

        public void Dispose()
        {
            Animation.CommitSuicide();
        }
    }
}

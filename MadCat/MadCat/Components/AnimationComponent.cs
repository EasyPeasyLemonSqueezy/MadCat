﻿using System;
using NutEngine;

namespace MadCat
{
    public class AnimationComponent : Component, ICleanup
    {
        public override Type[] Dependencies { get; } = {
            typeof(VelocityComponent),
            typeof(CharacterComponent)
        };
        public Animation Animation { get; set; }

        public AnimationComponent(Animation animation)
        {
            Animation = animation;
        }

        public override void Update(float deltaTime)
        {
            var position = Entity.GetComponent<PositionComponent>();
            Animation.Position = position.Position;
            Animation.Update(deltaTime);
        }

        public void Cleanup()
        {
            Animation.CommitSuicide();
        }
    }
}

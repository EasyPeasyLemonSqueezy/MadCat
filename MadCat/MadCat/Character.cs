using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;

using NutInput = NutEngine.Input;

namespace MadCat
{
    public class Character : Entity
    {
        public Character(Node node, EntityManager manager)
        {
            var animation 
                = new Animation(Assets.Texture, new NutPacker.Content.AdventureGirl.Idle()) {
                Duration = .8f
            };

            animation.Effects = SpriteEffects.None; /// Flip
            animation.Scale = new Vector2(.3f, .3f);

            node.AddChild(animation);

            var name = new Label(Assets.Font, "MadCat") {
                ZOrder = 1,
                Color = Color.BlueViolet,
                Position = new Vector2(0, -300),
                Scale = new Vector2(2)
            };
            animation.AddChild(name);

            Collider = new AABB() {
                  X = 0.0f
                , Y = -500.0f
                , Width = 81.0f
                , Height = 130.0f
            };

            AddComponent<CharacterComponent>(node, manager);
            AddComponent<GravitationComponent>();
            AddComponent<VelocityComponent>(new Vector2());
            AddComponent<PositionComponent>(new Vector2(0.0f, -500.0f));
            AddComponent<ColliderComponent>(Collider);
            AddComponent<AnimationComponent>(animation);
        }

        public void Input(NutInput.KeyboardState keyboardState)
        {
            var character = GetComponent<CharacterComponent>();
            character.StateMachine.UpdateInput(keyboardState);
        }

        public void SetControls(CharacterComponent.Controls controls)
        {
            var character = GetComponent<CharacterComponent>();
            character.Control = controls;
        }
    }
}

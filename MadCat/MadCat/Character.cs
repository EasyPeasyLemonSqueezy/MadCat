using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;
using NutInput = NutEngine.Input;

namespace MadCat
{
    public class Character : Entity
    {
        public RigidBody<AABB> Body { get; private set; }

        public Character(Node node, EntityManager manager, BodiesManager bodies)
        {
            var animation 
                = new Animation(Assets.Texture, new NutPacker.Content.AdventureGirl.Idle()) {
                Duration = .8f
            };

            animation.Effects = SpriteEffects.None; /// Flip
            animation.Scale = new Vector2(.3f, .3f);

            node.AddChild(animation);

            var name = new Label(Assets.Font, "Adventure Girl") {
                ZOrder = 1,
                Color = Color.Black,
                Position = new Vector2(0, -300),
                Scale = new Vector2(1.5f)
            };
            animation.AddChild(name);

            Body = new RigidBody<AABB>(new AABB(new Vector2(81.0f / 2.0f, 130.0f / 2.0f))) {
                Position = new Vector2(0.0f, -500.0f),
                Owner = this,
                Acceleration = new Vector2(0.0f, 4000.0f)
            };

            Body.Mass.MassInv = 1;
            Body.Material.Restitution = .8f;

            var stateMachine = new StateMachine(new StandState(this));

            manager.Add(this);
            AddComponents(
                new CharacterComponent(node, manager, stateMachine, bodies),
                new ColliderComponent(Body),
                new AnimationComponent(animation)
            );
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

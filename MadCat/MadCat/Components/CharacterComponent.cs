using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NutEngine;
using NutEngine.Physics;

namespace MadCat
{
    public class CharacterComponent : Component
    {
        public StateMachine StateMachine;

        public enum Direction
        {
            RIGHT = 1
            , LEFT = -1
        }

        public Direction Dir = Direction.RIGHT;

        public struct Controls
        {
            public Keys RunRightKey;
            public Keys RunLeftKey;
            public Keys JumpKey;
            public Keys ShootKey;
            public Keys MeleeKey;
            public Keys SlideKey;
        };

        public Controls Control;

        private Label name;

        public Node Node;
        public EntityManager Manager;
        public BodiesManager Bodies;

        private float runVelocity = 400.0f;
        private float jumpVelocity = -800.0f;

        public CharacterComponent(Node node, EntityManager manager, StateMachine stateMachine, BodiesManager bodies)
        {
            StateMachine = stateMachine;

            Manager = manager;
            Bodies = bodies;
            Node = node;

            name = new Label(Assets.Font, "MadCat") {
                ZOrder = 1,
                Color = Color.BlueViolet,
                Position = new Vector2(0, -300),
                Scale = new Vector2(2)
            };

            Control = new Controls() {
                  RunRightKey = Keys.Right
                , RunLeftKey  = Keys.Left
                , JumpKey     = Keys.Up
                , ShootKey    = Keys.Z
                , MeleeKey    = Keys.X
                , SlideKey    = Keys.Down
            };
        }

        public override void Update(float deltaTime)
        {
            StateMachine.Update(deltaTime);

            var animation = Entity.GetComponent<AnimationComponent>().Animation;
            animation.Color = Color.White;
            
            /// Set flipped or not
            animation.Effects = Dir == Direction.RIGHT
                                     ? SpriteEffects.None
                                     : SpriteEffects.FlipHorizontally;
        }

        public void Stand()
        {
            var body = Entity.GetComponent<ColliderComponent>().Body;
            body.Velocity = new Vector2(0.0f, body.Velocity.Y);
        }

        public void Run(Direction direction)
        {
            var body = Entity.GetComponent<ColliderComponent>().Body;
            body.Velocity = new Vector2((int)direction * runVelocity, body.Velocity.Y);
            Dir = direction;
        }

        public void Jump()
        {
            var body = Entity.GetComponent<ColliderComponent>().Body;
            body.Velocity = new Vector2(body.Velocity.X, jumpVelocity);
        }
    }
}

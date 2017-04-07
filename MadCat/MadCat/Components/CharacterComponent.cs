using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NutEngine;

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

        private float runVelocity = 400.0f;
        private float jumpVelocity = -800.0f;

        public CharacterComponent(Node node, EntityManager manager)
        {
            StateMachine = new StateMachine(new StandState(this));

            Manager = manager;
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

        public void CollideWall(Wall wall)
        {
            var position = Entity.GetComponent<PositionComponent>();
            var velocity = Entity.GetComponent<VelocityComponent>();
            var collider = Entity.GetComponent<ColliderComponent>();

            var response = collider.Collider.Response(wall.Collider);

            /// If we don't do this, we will stuck in the wall
            if (response.Y != 0.0f) {
                velocity.Velocity = new Vector2(velocity.Velocity.X, 0.0f);
            }

            if (response.X != 0.0f) {
                velocity.Velocity = new Vector2(0.0f, velocity.Velocity.Y);
            }

            position.Position += response;
            collider.Collider.X = position.Position.X - collider.Collider.Width / 2.0f;
            collider.Collider.Y = position.Position.Y - collider.Collider.Height / 2.0f;
        }

        public void Stand()
        {
            var velocity = Entity.GetComponent<VelocityComponent>();
            velocity.Velocity = new Vector2(0.0f, velocity.Velocity.Y);
        }

        public void Run(Direction direction)
        {
            var velocity = Entity.GetComponent<VelocityComponent>();
            Dir = direction;
            velocity.Velocity = new Vector2((int)direction * runVelocity, velocity.Velocity.Y);
        }

        public void Jump()
        {
            var velocity = Entity.GetComponent<VelocityComponent>();
            velocity.Velocity = new Vector2(velocity.Velocity.X, jumpVelocity);
        }

        /// <summary>
        /// Just testing collision rules
        /// </summary>
        public void SetColor(Color color)
        {
            var animation = Entity.GetComponent<AnimationComponent>().Animation;
            animation.Color = color;
        }
    }
}

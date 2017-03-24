using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;

using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;

namespace MadCat
{
    public class Character : GameObject
    {
        private StateMachine stateMachine;

        public enum Direction
        {
              RIGHT =  1
            , LEFT  = -1
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

        public Animation CurrentAnimation { get; }
        private Label name;

        public Node Node;
        public GameObjectManager Manager;

        public Vector2 Position;
        public Vector2 Velocity;
        private Vector2 gravitation;

        private float runVelocity  =  400.0f;
        private float jumpVelocity = -800.0f;

        public Character(Node node, GameObjectManager manager)
        {
            stateMachine = new StateMachine(new StandState(this));

            Manager = manager;
            Node = node;

            CurrentAnimation 
                = new Animation(Assets.Texture, new NutPacker.Content.AdventureGirl.Idle()) {
                Duration = .8f
            };

            CurrentAnimation.Effects = SpriteEffects.None; /// Flip
            CurrentAnimation.Scale = new Vector2(.3f, .3f);

            node.AddChild(CurrentAnimation);

            name = new Label(Assets.Font, "MadCat") {
                ZOrder = 1,
                Color = Color.BlueViolet,
                Position = new Vector2(0, -300),
                Scale = new Vector2(2)
            };

            CurrentAnimation.AddChild(name);

            Control = new Controls() {
                  RunRightKey = Keys.Right
                , RunLeftKey  = Keys.Left
                , JumpKey     = Keys.Up
                , ShootKey    = Keys.Z
                , MeleeKey    = Keys.X
                , SlideKey    = Keys.Down
            };

            Position = new Vector2(0.0f, -500.0f);
            Velocity = new Vector2();
            gravitation = new Vector2(0, 2000);

            Collider = new AABB() {
                  X = Position.X
                , Y = Position.Y
                , Width = 81.0f
                , Height = 130.0f
            };
        }

        public void Input(NutInput.KeyboardState keyboardState)
        {
            stateMachine.UpdateInput(keyboardState);
        }

        public override void Update(float deltaTime)
        {
            CurrentAnimation.Color = Color.White;
            CurrentAnimation.Update(deltaTime);

            stateMachine.Update(deltaTime);

            Velocity = Physics.ApplyAccel(Velocity, gravitation, deltaTime);
            Position = Physics.ApplyVelocity(Position, Velocity, deltaTime);

            Collider.X = Position.X - Collider.Width / 2.0f;
            Collider.Y = Position.Y - Collider.Height / 2.0f;

            CurrentAnimation.Position = Position;

            /// Set flipped or not
            CurrentAnimation.Effects = Dir == Direction.RIGHT
                                     ? SpriteEffects.None
                                     : SpriteEffects.FlipHorizontally;
        }

        public void CollideWall(Wall wall)
        {
            var response = Collider.Response(wall.Collider);

            /// If we don't do this, we will stuck in the wall
            if (response.Y != 0.0f) {
                Velocity.Y = 0.0f;
            }

            if (response.X != 0.0f) {
                Velocity.X = 0.0f;
            }

            Position += response;
            Collider.X = Position.X - Collider.Width / 2.0f;
            Collider.Y = Position.Y - Collider.Height / 2.0f;
        }

        public void Stand()
        {
            Velocity.X = 0.0f;
        }

        public void Run(Direction direction)
        {
            Dir = direction;
            Velocity.X = (int)direction * runVelocity;
        }

        public void Jump()
        {
            Velocity.Y = jumpVelocity;
        }

        /// <summary>
        /// Just testing collision rules
        /// </summary>
        public void SetColor(Color color)
        {
            CurrentAnimation.Color = color;
        }
        
        public override void Cleanup()
        {
            CurrentAnimation.CommitSuicide();
        }
    }
}

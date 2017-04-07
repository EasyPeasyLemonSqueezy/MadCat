using NutEngine;

namespace MadCat
{
    class ShootState : IState
    {
        private CharacterComponent character;

        public ShootState(CharacterComponent character)
        {
            this.character = character;
        }

        public void Enter()
        {
            var animation = character.Entity.GetComponent<AnimationComponent>().Animation;
            animation.Change(Assets.AdventureGirlShoot);

            character.Stand();

            var position = character.Entity.GetComponent<PositionComponent>();
            Bullet bullet = new Bullet(position.Position, (float)character.Dir, character.Node);
            character.Manager.Add(bullet);
        }

        public IState UpdateInput(NutEngine.Input.KeyboardState keyboardState)
        {
            return null;
        }

        public IState Update(float deltaTime)
        {
            var animation = character.Entity.GetComponent<AnimationComponent>().Animation;

            if (!animation.Enabled) {
                return new StandState(character);
            }

            return null;
        }
    }
}

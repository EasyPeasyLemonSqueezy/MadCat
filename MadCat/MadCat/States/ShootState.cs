using NutEngine;

namespace MadCat
{
    class ShootState : IState
    {
        private Character character;

        public ShootState(Character character)
        {
            this.character = character;
        }

        public void Enter()
        {
            character.Stand();
            character.CurrentAnimation.Change(Assets.AdventureGirlShoot);

            Bullet bullet = new Bullet(character.Position, (float)character.Dir, character.Node);
            character.Manager.Add(bullet);
        }

        public IState UpdateInput(NutEngine.Input.KeyboardState keyboardState)
        {
            return null;
        }

        public IState Update(float deltaTime)
        {
            if (!character.CurrentAnimation.Enabled) {
                return new StandState(character);
            }

            return null;
        }
    }
}

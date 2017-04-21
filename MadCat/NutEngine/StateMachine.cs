namespace NutEngine
{
    public class StateMachine
    {
        private IState currentState;

        public StateMachine(IState firstState)
        {
            currentState = firstState;
            currentState.Enter();
        }

        public void UpdateInput(Input.KeyboardState keyboardState)
        {
            var newState = currentState.UpdateInput(keyboardState);

            if (newState != null) {
                currentState = newState;
                currentState.Enter();
            }
        }

        public void Update(float deltaTime)
        {
            var newState = currentState.Update(deltaTime);

            if (newState != null) {
                currentState = newState;
                currentState.Enter();
            }
        }
    }
}

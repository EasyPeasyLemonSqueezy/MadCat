namespace NutEngine
{
    public class StateMachine
    {
        private IState currentState;

        public StateMachine(IState firstState)
        {
            currentState = firstState;
        }

        public void Update(float deltaTime)
        {
            var newState = currentState.Update(deltaTime);

            if (newState != null) {
                currentState.Exit();
                currentState = newState;
                currentState.Enter();
            }
        }
    }
}

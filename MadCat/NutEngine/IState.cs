namespace NutEngine
{
    public interface IState
    {
        void Enter();
        void Exit();
        IState UpdateInput(Input.KeyboardState keyboardState);
        IState Update(float deltaTime);
    }
}

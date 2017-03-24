namespace NutEngine
{
    public interface IState
    {
        void Enter();
        IState UpdateInput(Input.KeyboardState keyboardState);
        IState Update(float deltaTime);
    }
}

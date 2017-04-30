using XnaInput = Microsoft.Xna.Framework.Input;

namespace NutEngine.Input
{
    /// <summary>
    /// Wrapper for monogame keyboard <see cref="XnaInput.Keyboard"/>.
    /// Allows getting keystrokes from keyboard.
    /// </summary>
    public static class Keyboard
    {
        /// Previous and current keyboard state.
        private static XnaInput.KeyboardState[] keyboardStates = new XnaInput.KeyboardState[2];
        private static bool current = false;

        public static void Update()
        {
            keyboardStates[current ? 1 : 0] = XnaInput.Keyboard.GetState();
            current = !current;
        }

        /// <summary>
        /// Return the <see cref="NutEngine.Input.KeyboardState"/>.
        /// </summary>
        public static KeyboardState State
            => new KeyboardState {
                  CurrentState = keyboardStates[current ? 0 : 1]
                , PrevState = keyboardStates[current ? 1 : 0]
            };
    }
}

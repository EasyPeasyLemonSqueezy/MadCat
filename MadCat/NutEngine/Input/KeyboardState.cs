using XnaInput = Microsoft.Xna.Framework.Input;

namespace NutEngine.Input
{
    /// <summary>
    /// Holds current and previous keyboard states <see cref="XnaInput.KeyboardState"/>
    /// </summary>
    public struct KeyboardState
    {
        public XnaInput.KeyboardState CurrentState { get; set; }
        public XnaInput.KeyboardState PrevState { get; set; }

        /// <summary>
        /// Gets whether given key is being pressed right now.
        /// </summary>
        /// <param name="key"> The key to query. </param>
        /// <returns>
        /// true if key is pressed on current state and was released on previous state;
        /// false otherwise.
        /// </returns>
        public bool IsKeyPressedRightNow(XnaInput.Keys key)
        {
            return PrevState.IsKeyUp(key) && CurrentState.IsKeyDown(key);
        }

        /// <summary>
        /// Gets whether given key is being released right now.
        /// </summary>
        /// <param name="key"> The key to query. </param>
        /// <returns>
        /// true if key was pressed on previous state, but now released;
        /// false otherwise.
        /// </returns>
        public bool IsKeyReleasedRightNow(XnaInput.Keys key)
        {
            return PrevState.IsKeyDown(key) && CurrentState.IsKeyUp(key);
        }

        /// <summary>
        /// Wrapper for getter <see cref="XnaInput.KeyboardState."/>.
        /// Get current state of <paramref name="key"/>
        /// </summary>
        /// <param name="key"> The key to query. </param>
        /// <returns>
        /// Current state of the key.
        /// </returns>
        public XnaInput.KeyState this[XnaInput.Keys key] {
            get {
                return CurrentState[key];
            }
        }

        /// <summary>
        /// Wrapper for <see cref="XnaInput.KeyboardState.GetPressedKeys"/>.
        /// Returns an array of values holding keys that are currently being pressed.
        /// </summary>
        /// <returns>
        /// The keys that are pressed at current state.
        /// </returns>
        public XnaInput.Keys[] GetPressedKeys()
        {
            return CurrentState.GetPressedKeys();
        }

        /// <summary>
        /// Wrapper for <see cref="XnaInput.KeyboardState.IsKeyDown(XnaInput.Keys)"/>.
        /// Gets whether given key is currently being pressed.
        /// </summary>
        /// <param name="key"> The key to query. </param>
        /// <returns>
        /// true if key pressed at current state;
        /// false otherwise.
        /// </returns>
        public bool IsKeyDown(XnaInput.Keys key)
        {
            return CurrentState.IsKeyDown(key);
        }

        /// <summary>
        /// Wrapper for <see cref="XnaInput.KeyboardState.IsKeyUp(XnaInput.Keys)"/>.
        /// Gets whether given key is currently being not pressed.
        /// </summary>
        /// <param name="key"> The key to query. </param>
        /// <returns>
        /// true if key not pressed at current state;
        /// false otherwise.
        /// </returns>
        public bool IsKeyUp(XnaInput.Keys key)
        {
            return CurrentState.IsKeyUp(key);
        }

        /// <summary>
        /// Wrapper for <see cref="XnaInput.KeyboardState.Equals(object)"/>.
        /// Compare only current state.
        /// </summary>
        public override bool Equals(object obj)
        {
            return CurrentState.Equals(obj);
        }

        /// <summary>
        /// Wrapper for <see cref="XnaInput.KeyboardState.GetHashCode"/>.
        /// Get hash code only for current state.
        /// </summary>
        public override int GetHashCode()
        {
            return CurrentState.GetHashCode();
        }

        /// <summary>
        /// Compare current states of <paramref name="a"/> and <paramref name="b"/>
        /// </summary>
        /// <returns>
        /// true if current states are equal;
        /// false otherwise.
        /// </returns>
        public static bool operator ==(KeyboardState a, KeyboardState b)
        {
            return a.CurrentState == b.CurrentState;
        }

        /// <summary>
        /// Compare current states of <paramref name="a"/> and <paramref name="b"/>
        /// </summary>
        /// <returns>
        /// true if current states are not equal;
        /// false otherwise.
        /// </returns>
        public static bool operator !=(KeyboardState a, KeyboardState b)
        {
            return a.CurrentState != b.CurrentState;
        }

        /// <summary>
        /// Conversion from <see cref="XnaInput.KeyboardState"/> to <see cref="KeyboardState"/>.
        /// Return new <see cref="KeyboardState"/> whith same state.
        /// </summary>
        public static implicit operator KeyboardState(XnaInput.KeyboardState keyboardState)
        {
            return new KeyboardState {
                  CurrentState = keyboardState
                , PrevState = keyboardState
            };
        }

        /// <summary>
        /// Conversion from <see cref="KeyboardState"/> to <see cref="XnaInput.KeyboardState"/>.
        /// Return new <see cref="XnaInput.KeyboardState"/> with pressed keys at current state.
        /// </summary>
        public static implicit operator XnaInput.KeyboardState(KeyboardState keyboardState)
        {
            return new XnaInput.KeyboardState(keyboardState.GetPressedKeys());
        }
    }
}

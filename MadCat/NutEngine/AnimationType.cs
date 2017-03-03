namespace NutEngine
{
    /// <summary>
    /// Easing functions specify the rate of change of a parameter over time.
    /// </summary>
    /// <param name="frames"> Total number of frames in animation. </param>
    /// <param name="duration"> Duration of animation. </param>
    /// <param name="time"> Elapsed time. </param>
    /// <returns> Number of current frame. </returns>
    public delegate int AnimationType(int frames, float duration, float time);

    /// <summary>
    /// Set of Easing functions.
    /// </summary>
    public static class AnimationTypes
    {
        public static AnimationType Linear = (frames, duration, time)
            => {
                return (int)((frames / duration) * time);
            };
    }
}

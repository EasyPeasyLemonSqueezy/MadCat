namespace NutEngine
{
    public delegate int AnimationType(int frames, float duration, float time);

    public static class AnimationTypes
    {
        public static AnimationType Linear = (frames, duration, time)
            => {
                return (int)((frames / duration) * time);
            };
    }
}

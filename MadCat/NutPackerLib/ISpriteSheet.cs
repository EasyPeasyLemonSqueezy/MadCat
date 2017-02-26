namespace NutPacker
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Interface for sprite sheets,
    /// contains Length property, and indexer for rectangles.
    /// </summary>
    public interface ISpriteSheet
    {
        int Length { get; }
        Rectangle this[int index] { get; }
    }
}

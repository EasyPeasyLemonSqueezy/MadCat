using Microsoft.Xna.Framework;

namespace NutEngine.Physics
{
    public interface IBody<out ShapeType>
    {
        ShapeType Shape { get; }
        MassData Mass { get; }
        Material Material { get; }
        object Owner { get; }

        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Vector2 Force { get; set; }
    }
}

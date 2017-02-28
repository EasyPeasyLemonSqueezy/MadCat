using Microsoft.Xna.Framework;

namespace NutEngine
{
    public class Camera : Node
    {
        /// TODO: Либо найти способ без доп. поля перегрузить Position,
        /// либо сделать полями Position, Scale и Rotation
        private Vector2 position;
        public override Vector2 Position
        {
            get { return position; }
            set { position = -value; }
        }

        public float Zoom
        {
            get { return Scale.X; }
            set { Scale = new Vector2(value, value); }
        }

        /// TODO: Сделать камере Origin,
        /// центр, относительно которого происходят преобразования
    }
}

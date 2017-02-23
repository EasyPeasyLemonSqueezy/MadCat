using System;
using Microsoft.Xna.Framework;

namespace NutEngine
{
    /// <summary>
    /// Обертка над матрицей преобразования.
    /// Рассчитывает ее на основе позиции, поворота
    /// и масштаба, а потом достает эту информацию
    /// из нее.
    /// </summary>
    public class Transform2D
    {
        private Matrix matrix;

        public Transform2D()
        {
            matrix = Matrix.Identity;
        }

        public void SetTransform(Vector2 scale, float rotation, Vector2 position)
        {
            /// Образовать матрицы масштаба, поворота и перемещения и
            /// перемножить их именно в этом порядке.
            /// Теперь эта матрица хранит всю нужную информацию.
            matrix =
                     Matrix.CreateScale(scale.X, scale.Y, 1.0f)
                   * Matrix.CreateRotationZ(rotation)
                   * Matrix.CreateTranslation(position.X, position.Y, 0.0f);
        }

        /// <summary>
        /// Достать информацию из матрицы. Если бы monogame был
        /// сделан по-человечески, то этот этап был бы не нужен и
        /// мы бы просто передавали матрицу в отрисовку. Но здесь
        /// так нельзя. Sad but true.
        /// </summary>
        public void Decompose(out Vector2 scale, out float rotation, out Vector2 position)
        {
            Vector3 position3, scale3;
            Quaternion quaternion;

            /// Получить информацию для 3D.
            matrix.Decompose(out scale3, out quaternion, out position3);

            /// Перевести информацию в 2D.
            var direction = Vector2.Transform(Vector2.UnitX, quaternion);
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            position = new Vector2(position3.X, position3.Y);
            scale = new Vector2(scale3.X, scale3.Y);
        }

        /// <summary>
        /// Умножить эту матрицу преобразования на какую-то другую.
        /// Это приведет к тому, что те же самые преобразовния уже
        /// будут в новой системе координат (родителя).
        /// </summary>
        public static Transform2D operator *(Transform2D tranform1, Transform2D transform2)
        {
            Transform2D result = new Transform2D();
            result.matrix = tranform1.matrix * transform2.matrix;
            return result;
        }
    }
}

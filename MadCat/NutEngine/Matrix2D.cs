using Microsoft.Xna.Framework;
using System;

namespace NutEngine
{
    /// <summary>
    /// Right-handed float 3x3 transformation matrix.
    /// </summary>
    public class Matrix2D
    {
        /// <summary>
        /// It's always 3x3,
        /// because this class have only private constructors.
        /// </summary>
        private float[,] matrix;

        /// <summary>
        /// Create Zeroes matrix.
        /// </summary>
        private Matrix2D()
        {
            matrix = new float[3, 3];
        }

        /// <summary>
        /// Create matrix.
        /// Make a copy of <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix"> Float two-dimensional array. </param>
        private Matrix2D(float[,] matrix)
        {
            this.matrix = (float[,])matrix.Clone();
        }

        /// <summary>
        /// Create matrix.
        /// Use reference to <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix"> Float two-dimensional array. </param>
        private Matrix2D(ref float[,] matrix)
        {
            this.matrix = matrix;
        }

        /// <summary>
        /// Get translation vector.
        /// </summary>
        public Vector2 Translation => new Vector2(matrix[0, 2], matrix[1, 2]);

        /// <summary>
        /// Get angle in radians.
        /// </summary>
        public float Rotation => (float)Math.Atan2(matrix[1, 0], matrix[1, 1]);

        /// <summary>
        /// Get angle in degrees.
        /// </summary>
        public float RotationDeg => (float)(Rotation / (Math.PI / 180));

        /// <summary>
        /// Get scale vector.
        /// </summary>
        public Vector2 Scale {
            get {
                var xScale = (float)Math.Sqrt(
                      matrix[0, 0] * matrix[0, 0]
                    + matrix[0, 1] * matrix[0, 1]);

                var yScale = (float)Math.Sqrt(
                      matrix[1, 0] * matrix[1, 0]
                    + matrix[1, 1] * matrix[1, 1]);

                return new Vector2(xScale, yScale);
            }
        }

        /// <summary>
        /// Create TRS Transformation matrix.
        /// </summary>
        /// <param name="translation"> Translation vector. </param>
        /// <param name="scale"> Scale vector. </param>
        /// <param name="rotation"> Angle in radians. </param>
        /// <returns>
        /// Transformation matrix.
        /// </returns>
        public static Matrix2D CreateTRS(Vector2 translation, Vector2 scale, float rotation)
        {
            var cos = (float)Math.Cos(rotation);
            var sin = (float)Math.Sin(rotation);

            var matrix = new float[3, 3] {
                  { scale.X * cos, -scale.X * sin, translation.X }
                , { scale.Y * sin,  scale.Y * cos, translation.Y }
                , {       0,              0,             1       }
            };

            return new Matrix2D(ref matrix);
        }

        /// <summary>
        /// Create SRT Transformation matrix.
        /// </summary>
        /// <param name="translation"> Translation vector. </param>
        /// <param name="scale"> Scale vector. </param>
        /// <param name="rotation"> Angle in radians. </param>
        /// <returns>
        /// Transformation matrix.
        /// </returns>
        public static Matrix2D CreateSRT(Vector2 translation, Vector2 scale, float rotation)
        {
            var cos = (float)Math.Cos(rotation);
            var sin = (float)Math.Sin(rotation);

            var SxCos = scale.X * cos;
            var SxSin = scale.X * sin;

            var SyCos = scale.Y * cos;
            var SySin = scale.Y * sin;

            var matrix = new float[3, 3] {
                  { SxCos, -SxSin, SxCos * translation.X - SxSin * translation.Y }
                , { SySin,  SyCos, SySin * translation.X + SyCos * translation.Y }
                , {   0,      0,                         1                       }
            };

            return new Matrix2D(ref matrix);
        }

        /// <summary>
        /// Create Translation matrix.
        /// </summary>
        /// <param name="position"> Translation vector </param>
        /// <returns></returns>
        public static Matrix2D CreateTranslation(Vector2 position)
        {
            var matrix = new float[3, 3] {
                  { 1, 0, position.X }
                , { 0, 1, position.Y }
                , { 0, 0,      1     }
            };

            return new Matrix2D(ref matrix);
        }

        /// <summary>
        /// Create Rotation matrix.
        /// </summary>
        /// <param name="radians"> Angle in radians. </param>
        /// <returns>
        /// Rotation matrix.
        /// </returns>
        public static Matrix2D CreateRotation(float radians)
        {
            var cos = (float)Math.Cos(radians);
            var sin = (float)Math.Sin(radians);

            var matrix = new float[3, 3] {
                  { cos, -sin, 0 }
                , { sin,  cos, 0 }
                , {  0,    0,  1 }
            };

            return new Matrix2D(ref matrix);
        }

        /// <summary>
        /// Create Rotation matrix.
        /// </summary>
        /// <param name="degrees"> Angle in degrees. </param>
        /// <returns>
        /// Rotation matrix
        /// </returns>
        public static Matrix2D CreateRotationFromDeg(float degrees)
        {
            var radians = (float)(degrees * (Math.PI / 180));
            return CreateRotation(radians);
        }

        /// <summary>
        /// Create Scale matrix.
        /// </summary>
        /// <param name="scale"> Scale by x and y axes </param>
        /// <returns>
        /// Scale matrix.
        /// </returns>
        public static Matrix2D CreateScale(Vector2 scale)
        {
            var matrix = new float[3, 3] {
                  { scale.X,    0,    0 }
                , {    0,    scale.Y, 0 }
                , {    0,       0,    1 }
            };

            return new Matrix2D(ref matrix);
        }

        /// <summary>
        /// Zeroes matrix.
        /// </summary>
        public static Matrix2D Zeroes { get; }
            = new Matrix2D();

        /// <summary>
        /// Identity Matrix.
        /// </summary>
        public static Matrix2D Identity { get; }
            = new Matrix2D(new float[3, 3] {
                  { 1, 0, 0 }
                , { 0, 1, 0 }
                , { 0, 0, 1 }
            });

        /// <summary>
        /// Matrix multiplication.
        /// It's reverse order multiplication,
        /// here <paramref name="m1"/> multiply by <paramref name="m2"/>
        /// (Be careful look at parameters order).
        /// </summary>
        /// <remarks>
        /// <code>
        /// for (int row = 0; row &lt 3; ++row) {
        ///     for (int column = 0; column &lt 3; ++column) {
        ///         for (int n = 0; n &lt 3; ++n) {
        ///             result[row, column] += m1[row, n] * m2[n, column];
        ///         }
        ///     }
        /// }
        /// </code>
        /// </remarks>
        /// <param name="m2"></param>
        /// <param name="m1"></param>
        /// <returns></returns>
        public static Matrix2D operator *(Matrix2D m2, Matrix2D m1)
        {
            var result = new Matrix2D();

            result[0, 0] = m1[0, 0] * m2[0, 0] + m1[0, 1] * m2[1, 0];
            result[0, 1] = m1[0, 0] * m2[0, 1] + m1[0, 1] * m2[1, 1];

            result[0, 2] = m1[0, 0] * m2[0, 2] + m1[0, 1] * m2[1, 2] + m1[0, 2];

            result[1, 0] = m1[1, 0] * m2[0, 0] + m1[1, 1] * m2[1, 0];
            result[1, 1] = m1[1, 0] * m2[0, 1] + m1[1, 1] * m2[1, 1];

            result[1, 2] = m1[1, 0] * m2[0, 2] + m1[1, 1] * m2[1, 2] + m1[1, 2];

            result[2, 2] = m1[2, 2] * m2[2, 2];

            return result;
        }

        public float this[int row, int column] {
            get => matrix[row, column];
            private set => matrix[row, column] = value;
        }
    }
}

﻿using Microsoft.Xna.Framework;

namespace NutEngine.Camera
{
    public class OrthographicSRTCamera : Camera2D
    {
        public override Matrix2D Transform
            => Matrix2D.CreateTranslation(-Origin)
             * Matrix2D.CreateSRT(-Position, new Vector2(Zoom, Zoom), Rotation)
             * Matrix2D.CreateTranslation(Origin);

        public OrthographicSRTCamera(Vector2 frame) : base(frame) { }
    }
}

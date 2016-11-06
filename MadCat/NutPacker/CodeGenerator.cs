namespace NutPacker
{
    using System.CodeDom;
    using System.Reflection;

    using System.Drawing;

    /// <summary>
    /// Generation code of sprite/spriteSheet classes.
    /// </summary>
    internal static class CodeGenerator
    {
        /// <summary>
        /// Generate code of new SpriteSheet class.
        /// </summary>
        /// <param name="spriteSheetName"> Name of new class/spriteSheet. </param>
        /// <returns>
        /// <code>
        /// public class <paramref name="spriteSheetName"/> : <see cref="ISpriteSheet"/> { }
        /// </code>
        /// </returns>
        public static CodeTypeDeclaration GenerateSpriteSheetClass(
            string spriteSheetName)
        {
            /// New public class with <param name="spriteSheetName"></param> name.
            var spriteSheetClass = new CodeTypeDeclaration() {
                  Name = spriteSheetName
                , IsClass = true
                , TypeAttributes = TypeAttributes.Public
            };

            /// Inherited from <see cref="ISpriteSheet"/>.
            spriteSheetClass.BaseTypes.Add(new CodeTypeReference(typeof(ISpriteSheet)));

            return spriteSheetClass;
        }

        /// <summary>
        /// Generate code of new Sprite class.
        /// </summary>
        /// <param name="spriteName"> Name of new class/sprite. </param>
        /// <param name="rectangles"> Array of rectangles <see cref="Sprite.Frames"/>. </param>
        /// <returns>
        /// <code>
        /// public class <paramref name="spriteName"/> : <see cref="Sprite"/>
        /// {
        ///     public <paramref name="spriteName"/>() {
        ///         this.Frames = new Rectangle[] {
        ///             <paramref name="rectangles"/>
        ///         }
        ///     }
        /// }
        /// </code>
        /// </returns>
        public static CodeTypeDeclaration GenerateSpriteClass(
              string spriteName
            , params Rectangle[] rectangles)
        {
            /// New public class with <param name="spriteName"></param> name.
            CodeTypeDeclaration spriteClass = new CodeTypeDeclaration() {
                  Name = spriteName
                , IsClass = true
                , TypeAttributes = TypeAttributes.Public
            };

            /// Inherited from <see cref="Sprite"/>.
            spriteClass.BaseTypes.Add(new CodeTypeReference(typeof(Sprite)));

            /// Array of expressions which create rectangles.
            CodeExpression[] createRectangles = new CodeExpression[rectangles.Length];

            for (int i = 0; i < rectangles.Length; i++) {
                /// One rectangle
                /// new Rectangle(X, Y, Height, Width);
                CodeExpression rectangle = new CodeObjectCreateExpression(typeof(Rectangle)
                    , new CodePrimitiveExpression(rectangles[i].X)
                    , new CodePrimitiveExpression(rectangles[i].Y)
                    , new CodePrimitiveExpression(rectangles[i].Height)
                    , new CodePrimitiveExpression(rectangles[i].Width)
                    );

                createRectangles[i] = rectangle;
            }

            /// Array of rectangles.
            /// new Rectangles[rectangles.Length] { <param name="rectangles"></param> };
            CodeArrayCreateExpression createArray = new CodeArrayCreateExpression(
                  new CodeTypeReference(typeof(Rectangle))
                , createRectangles);

            /// Create field - "Frames".
            /// this.Frames;
            CodeFieldReferenceExpression frames = new CodeFieldReferenceExpression(
                  new CodeThisReferenceExpression()
                , "Frames");

            /// Frames = array of rectangles:
            /// this.Frames = new Rectangles[rectangles.Length] {
            ///     <param name="rectangles"></param>
            /// };
            CodeAssignStatement assign = new CodeAssignStatement(frames, createArray);

            /// Create constructor.
            CodeConstructor constructor = new CodeConstructor() {
                  Name = spriteName
                , Attributes = MemberAttributes.Public
            };

            /// Add assignment to constructor.
            constructor.Statements.Add(assign);
            /// Add constuctor to class.
            spriteClass.Members.Add(constructor);

            return spriteClass;
        }
    }
}

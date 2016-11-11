using System.CodeDom;
using System.Reflection;

using System.Drawing;

namespace NutPacker
{
    /// <summary>
    /// Generation code of sprite/spriteSheet classes.
    /// </summary>
    internal static class CodeGenerator
    {
        /// <summary>
        /// Generate code of new spriteGroup class.
        /// </summary>
        /// <param name="spriteGroup"> Name of new class/spriteGroup. </param>
        /// <returns>
        /// <code>
        /// public class <paramref name="spriteGroup"/> : <see cref="ISpriteGroup"/> { }
        /// </code>
        /// </returns>
        public static CodeTypeDeclaration GenerateSpriteGroupClass(
            string spriteGroup)
        {
            /// New public class with <param name="spriteGroup"></param> name.
            var spriteGroupClass = new CodeTypeDeclaration() {
                  Name = spriteGroup
                , IsClass = true
                , TypeAttributes = TypeAttributes.Public
            };

            /// Inherited from <see cref="ISpriteGroup"/>.
            spriteGroupClass.BaseTypes.Add(new CodeTypeReference(typeof(ISpriteGroup)));

            return spriteGroupClass;
        }

        /// <summary>
        /// Generate code of new SpriteSheet class.
        /// </summary>
        /// <param name="spriteSheetName"> Name of new class/spriteSheet. </param>
        /// <param name="rectangles"> Array of rectangles <see cref="SpriteSheet.Frames"/>. </param>
        /// <returns>
        /// <code>
        /// public class <paramref name="spriteSheetName"/> : <see cref="SpriteSheet"/>
        /// {
        ///     public <paramref name="spriteSheetName"/>() {
        ///         this.Frames = new Rectangle[] {
        ///             <paramref name="rectangles"/>
        ///         }
        ///     }
        /// }
        /// </code>
        /// </returns>
        public static CodeTypeDeclaration GenerateSpriteSheetClass(
              string spriteSheetName
            , params Rectangle[] rectangles)
        {
            /// New public class with <param name="spriteSheetName"></param> name.
            CodeTypeDeclaration spriteSheetClass = new CodeTypeDeclaration() {
                  Name = spriteSheetName
                , IsClass = true
                , TypeAttributes = TypeAttributes.Public
            };

            /// Inherited from <see cref="SpriteSheet"/>.
            spriteSheetClass.BaseTypes.Add(new CodeTypeReference(typeof(SpriteSheet)));

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
                  Name = spriteSheetName
                , Attributes = MemberAttributes.Public
            };

            /// Add assignment to constructor.
            constructor.Statements.Add(assign);
            /// Add constuctor to class.
            spriteSheetClass.Members.Add(constructor);

            return spriteSheetClass;
        }
    }
}

using System.CodeDom;
using System.Reflection;

using System.Drawing;
using System.IO;

namespace NutPacker
{
    /// <summary>
    /// Generation code of sprite/spriteSheet classes.
    /// </summary>
    public static class CodeGenerator
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

        /// <summary>
        /// Generate code of new pictureGroup class
        /// </summary>
        /// <param name="pictureGroup"> Name of new class. </param>
        /// <returns>
        /// <code>
        /// public class <paramref name="pictureGroup"/> : <see cref="NutPacker.IPictureGroup"/> { }
        /// </code>
        /// </returns>
        public static CodeTypeDeclaration GeneratePictureGroupClass(
              string pictureGroup)
        {
            /// New public class with <param name="pictureGroup"></param> name.
            var pictureGroupClass = new CodeTypeDeclaration() {
                  Name = pictureGroup
                , IsClass = true
                , TypeAttributes = TypeAttributes.Public
            };

            /// Inherited from <see cref="IPictureGroup"/>.
            pictureGroupClass.BaseTypes.Add(new CodeTypeReference(typeof(IPictureGroup)));

            return pictureGroupClass;
        }

        /// <summary>
        /// Generate new property <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="picture"> Name of new property. </param>
        /// <param name="rectangle"> Location in atlas. </param>
        /// <returns>
        /// <code>
        /// public static <see cref="Rectangle"/> <paramref name="picture"/> {
        ///     get {
        ///         // r - <paramref name="rectangle"/>.
        ///         return new <see cref="Rectangle"/>(r.X, r.Y, r.Height, r.Width);
        ///     }
        /// }
        /// </code>
        /// </returns>
        public static CodeMemberProperty GeneratePictureProperty(
              string picture
            , Rectangle rectangle)
        {
            /// New public static class, with getter and type <see cref="Rectangle"/>.
            CodeMemberProperty pic = new CodeMemberProperty() {
                  Attributes = MemberAttributes.Public | MemberAttributes.Static
                , Name = Path.GetFileNameWithoutExtension(picture)
                , HasGet = true
                , Type = new CodeTypeReference(typeof(Rectangle))
            };

            /// Add expression to getter: return new <see cref="Rectangle"/>(r.X, r.Y, r.Height, r.Width);
            /// r - <param name="rectangle"></param>
            pic.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeObjectCreateExpression(typeof(Rectangle)
                        , new CodePrimitiveExpression(rectangle.X)
                        , new CodePrimitiveExpression(rectangle.Y)
                        , new CodePrimitiveExpression(rectangle.Height)
                        , new CodePrimitiveExpression(rectangle.Width)
                )));
            
            return pic;
        }
    }
}

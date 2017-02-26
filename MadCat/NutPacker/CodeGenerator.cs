using System.CodeDom;
using System.Reflection;

using System.Drawing;
using Xna = Microsoft.Xna.Framework;
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
        /// <remarks>
        /// Ya, all this code will be generated for every class,
        /// but it's only way to use indexer with static field.
        /// And ya, we need static field, cause we don't wanna create instance of array
        /// each time when we use it.
        /// So, it's some kinda singleton.
        /// </remarks>
        /// <param name="spriteSheetName"> Name of new class/spriteSheet. </param>
        /// <param name="rectangles"> Array of rectangles. </param>
        /// <returns>
        /// <code>
        /// public class <paramref name="spriteSheetName"/> : <see cref="ISpriteSheet"/>
        /// {
        ///     private static <see cref="Xna.Rectangle"/>[] Frames =
        ///         new <see cref="Xna.Rectangle"/>[] { }
        ///     
        ///     public int Length {
        ///         get {
        ///             return Frames.Length; // Here will be just number.
        ///         }
        ///     }
        ///     
        ///     public int this[int index] { get { return Frames[index] } }
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

            /// Inherited from <see cref="ISpriteSheet"/>.
            spriteSheetClass.BaseTypes.Add(new CodeTypeReference(typeof(ISpriteSheet)));

            /// Array of expressions which create rectangles.
            CodeExpression[] createRectangles = new CodeExpression[rectangles.Length];

            for (int i = 0; i < rectangles.Length; i++) {
                /// One rectangle
                /// new Rectangle(X, Y, Width, Height);
                CodeExpression rectangle = new CodeObjectCreateExpression(typeof(Xna.Rectangle)
                    , new CodePrimitiveExpression(rectangles[i].X)
                    , new CodePrimitiveExpression(rectangles[i].Y)
                    , new CodePrimitiveExpression(rectangles[i].Width)
                    , new CodePrimitiveExpression(rectangles[i].Height)
                    );

                createRectangles[i] = rectangle;
            }

            /// Array of rectangles.
            /// new Rectangles[rectangles.Length] { <param name="rectangles"></param> };
            CodeArrayCreateExpression createArray = new CodeArrayCreateExpression(
                  new CodeTypeReference(typeof(Xna.Rectangle))
                , createRectangles);


            /// private static field with array of <see cref="Xna.Rectangle"/> which called Frames.
            CodeMemberField frames = new CodeMemberField() {
                  Attributes = MemberAttributes.Private | MemberAttributes.Static | MemberAttributes.Final
                , Type = new CodeTypeReference(typeof(Xna.Rectangle[]))
                , Name = "Frames"
                , InitExpression = createArray
            };

            /// Length property,
            /// only one getter which return number of rectangles in Frames field.
            CodeMemberProperty length = new CodeMemberProperty() {
                  Attributes = MemberAttributes.Public | MemberAttributes.Final
                , Type = new CodeTypeReference(typeof(int))
                , Name = "Length"
                , HasGet = true
            };

            /// Expression which return number of rectangles.
            CodeMethodReturnStatement retn =
                new CodeMethodReturnStatement(new CodePrimitiveExpression(rectangles.Length));
            /// Add return expression to length getter.
            length.GetStatements.Add(retn);

            /// Indexer.
            /// I don't know why, but it's only way to create indexer.
            /// Ya, we MUST create PROPERTY which called "Item", not "item" or "indexer" or somthing else,
            /// just fucking "Item".
            /// And after thar, we MUST add declaration of variable to PROPERTY parameters.
            /// Microsoft what the fuck?
            /// Documentation says we should use <see cref="CodeIndexerExpression"/>,
            /// but it's not work.
            /// Anyway, I found ONE post from 2003 with correct example
            /// source: https://forums.asp.net/post/354445.aspx
            /// thanks man, this is gonna suck when your post will be deleted.
            CodeMemberProperty indexer = new CodeMemberProperty() {
                  Attributes = MemberAttributes.Public | MemberAttributes.Final
                , Type = new CodeTypeReference(typeof(Xna.Rectangle))
                , Name = "Item"
                , HasGet = true
            };

            /// Declaration of variable.
            /// int index
            CodeParameterDeclarationExpression index =
                new CodeParameterDeclarationExpression(
                      new CodeTypeReference(typeof(int))
                    , "index"
                    );

            /// Magic continues.
            indexer.Parameters.Add(index);

            /// Create getter.
            /// get { return Frames[index]; }
            indexer.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeArrayIndexerExpression(
                      new CodeVariableReferenceExpression("Frames")
                    , new CodeVariableReferenceExpression("index"))
                ));
            
            /// Add Frames field, Length property, and indexer to the class.
            spriteSheetClass.Members.Add(frames);
            spriteSheetClass.Members.Add(length);
            spriteSheetClass.Members.Add(indexer);

            return spriteSheetClass;
        }

        /// <summary>
        /// Generate code of new tile set class
        /// </summary>
        /// <param name="tileSet"> Name of new class. </param>
        /// <returns>
        /// <code>
        /// public class <paramref name="tileSet"/> : <see cref="NutPacker.ITileSet"/> { }
        /// </code>
        /// </returns>
        public static CodeTypeDeclaration GenerateTileSetClass(
              string tileSet)
        {
            /// New public class with <param name="tileSet"></param> name.
            var tileSetClass = new CodeTypeDeclaration() {
                  Name = tileSet
                , IsClass = true
                , TypeAttributes = TypeAttributes.Public
            };

            /// Inherited from <see cref="ITileSet"/>.
            tileSetClass.BaseTypes.Add(new CodeTypeReference(typeof(ITileSet)));

            return tileSetClass;
        }

        /// <summary>
        /// Generate new property <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="picture"> Name of new property. </param>
        /// <param name="rectangle"> Location in texture atlas. </param>
        /// <returns>
        /// <code>
        /// public static <see cref="Rectangle"/> <paramref name="picture"/> {
        ///     get {
        ///         // r - <paramref name="rectangle"/>.
        ///         return new <see cref="Rectangle"/>(r.X, r.Y, r.Width, r.Height);
        ///     }
        /// }
        /// </code>
        /// </returns>
        public static CodeMemberProperty GenerateTileProperty(
              string picture
            , Rectangle rectangle)
        {
            /// New public static class, with getter and type <see cref="Rectangle"/>.
            CodeMemberProperty pic = new CodeMemberProperty() {
                  Attributes = MemberAttributes.Public | MemberAttributes.Static
                , Name = Path.GetFileNameWithoutExtension(picture)
                , HasGet = true
                , Type = new CodeTypeReference(typeof(Xna.Rectangle))
            };

            /// Add expression to getter: return new <see cref="Rectangle"/>(r.X, r.Y, r.Width, r.Height);
            /// r - <param name="rectangle"></param>
            pic.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeObjectCreateExpression(typeof(Xna.Rectangle)
                        , new CodePrimitiveExpression(rectangle.X)
                        , new CodePrimitiveExpression(rectangle.Y)
                        , new CodePrimitiveExpression(rectangle.Width)
                        , new CodePrimitiveExpression(rectangle.Height)
                )));
            
            return pic;
        }
    }
}

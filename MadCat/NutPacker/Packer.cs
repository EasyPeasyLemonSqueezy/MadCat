using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace NutPacker
{
    public static class Packer
    {
        /// <summary>
        /// Create atlas,
        /// and .dll with classes which contains rectangles.
        /// </summary>
        /// <param name="name">
        /// Name of result files.
        /// </param>
        /// <param name="sprites">
        /// Path to the directory with sprites.
        /// </param>
        /// <param name="pictures">
        /// Path to the directory with pictures.
        /// </param>
        /// <param name="output">
        /// Path to the directory where atlas and .dll will be saved.
        /// </param>
        /// <param name="generateSourceFile">
        /// Generate .cs file or not.
        /// </param>
        public static void Pack(
              string name
            , string sprites
            , string pictures
            , string output
            , bool generateSourceFile = false)
        {
            if (sprites == null && pictures == null) {
                throw new ApplicationException("Nothing to pack here.");
            }

            /// Delete previous atlas.
            File.Delete(Path.Combine(output, String.Concat(name, ".png")));

            /// Dictionary: full filename -> rectangle in output image.
            Dictionary<string, Rectangle> outputMap;
            /// Sprite.
            Bitmap outputImageBitmap;
            /// Paths to all images.
            var images = new List<string>();

            /// Get all ".jpg" and ".png" files in folder and all subfolders.
            if (sprites != null) {
                var spritesDirectory = new DirectoryInfo(sprites);

                images.AddRange(Walkthrough.GetFileNames(spritesDirectory));
            }
            if (pictures != null) {
                var picturesDirectory = new DirectoryInfo(pictures);
                
                images.AddRange(Walkthrough.GetFileNames(picturesDirectory));
            }


            /// Packer from sspack.
            var imagePacker = new sspack.ImagePacker();

            /// Create sprite and dictionary.
            imagePacker.PackImage(
                  images                /// Paths to all sprites and pictures,
                , false                 /// Power of two,
                , false                 /// Require square image,
                , 256 * 256 // 2^16     /// Max width,
                , 256 * 256             /// Max height,
                , 0                     /// Image padding,
                , true                  /// Generate dictionary,
                , out outputImageBitmap /// Output image,
                , out outputMap         /// Dictionary.
                );


            var codeUnit = new CodeCompileUnit();

            var codeNameSpace = new CodeNamespace("NutPacker.Content");
            codeUnit.Namespaces.Add(codeNameSpace);

            /// Generate code.
            if (sprites != null) {
                var spritesDirectory = new DirectoryInfo(sprites);

                codeNameSpace.Types.Add(Walkthrough.GenerateAtlasCodeDom(spritesDirectory, outputMap));
            }
            if (pictures != null) {
                var picturesDirectory = new DirectoryInfo(pictures);

                codeNameSpace.Types.Add(Walkthrough.GeneratePicturesCodeDom(picturesDirectory, outputMap));
            }

            var codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            var generatorOptions = new CodeGeneratorOptions {
                BracingStyle = "C"
                , BlankLinesBetweenMembers = false
                , VerbatimOrder = true
            };

            /// Create file with source code.
            if (generateSourceFile) {
                using (var sourceWriter =
                    new StreamWriter(Path.Combine(output, String.Concat(name, ".cs")))) {
                    codeDomProvider.GenerateCodeFromCompileUnit(
                          codeUnit
                        , sourceWriter
                        , generatorOptions);
                }
            }


            var cp = new CompilerParameters(
                  new string[] {
                        "sspack.exe"
                      , "NutPackerLib.dll"
                      , "System.Drawing.dll"
                  }
                , Path.Combine(output, String.Concat(output, ".dll"))
                , false
                ) {
                    GenerateInMemory = false
                };
            
            /// Compile the CodeDom.
            var compile = codeDomProvider.CompileAssemblyFromDom(cp, codeUnit);

            /// Print errors.
            foreach (var e in compile.Errors) {
                Console.Error.WriteLine(e.ToString());
            }

            /// If no error - save the sprite.
            if (compile.Errors.Count == 0) {
                using (var streamWriter =
                    new StreamWriter(Path.Combine(output, String.Concat(name, ".png")))) {
                    outputImageBitmap.Save(
                          streamWriter.BaseStream
                        , System.Drawing.Imaging.ImageFormat.Png
                        );
                }
            }
        }
    }
}

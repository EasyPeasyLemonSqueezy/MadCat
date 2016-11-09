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
        /// Create sprite
        /// and .dll with classes which contains rectangles.
        /// </summary>
        /// <param name="path">
        /// Path to the directory with pictures (or folders with pictures).
        /// </param>
        /// <param name="outputImagePath">
        /// Path to the directory where sprite and .dll will be saved.
        /// </param>
        /// <param name="generateSourceFile">
        /// Generate .cs file or not.
        /// </param>
        public static void Pack(string path, string outputImagePath, bool generateSourceFile = false)
        {
            var directory = new DirectoryInfo(path);

            /// Delete previous sprite.
            File.Delete(Path.Combine(outputImagePath, directory.Name + ".png"));

            /// Dictionary: full filename -> rectangle in output image.
            Dictionary<string, Rectangle> outputMap;
            /// Sprite.
            Bitmap outputImageBitmap;

            /// Get all ".jpg" and ".png" files in folder and all subfolders.
            var images = Walkthrough.GetFileNames(directory).ToArray();

            /// Packer from sspack.
            var imagePacker = new sspack.ImagePacker();

            /// Create sprite and dictionary.
            imagePacker.PackImage(
                  images                /// Array with full path to images,
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

            var codeNameSpace = new CodeNamespace("NutPacker.SpriteSheet");
            codeUnit.Namespaces.Add(codeNameSpace);

            /// Generate code.
            codeNameSpace.Types.Add(Walkthrough.GenerateCodeDom(directory, outputMap));

            var codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            var generatorOptions = new CodeGeneratorOptions {
                BracingStyle = "C"
                , BlankLinesBetweenMembers = false
                , VerbatimOrder = true
            };

            /// Create file with source code.
            if (generateSourceFile) {
                using (var sourceWriter =
                    new StreamWriter(Path.Combine(outputImagePath, directory.Name + ".cs"))) {
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
                , Path.Combine(outputImagePath, directory.Name + ".dll")
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
                using (var streamWriter = new StreamWriter(Path.Combine(outputImagePath, directory.Name + ".png"))) {
                    outputImageBitmap.Save(streamWriter.BaseStream, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
}

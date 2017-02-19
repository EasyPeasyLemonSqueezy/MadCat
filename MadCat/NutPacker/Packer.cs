using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
        public static void Pack(IPackOptions opt)
        {
            if (opt.Sprites == null && opt.Pictures == null) {
                throw new ApplicationException("Nothing to pack here.");
            }

            if (!opt.GenerateSource && !opt.GenerateLib) {
                throw new ApplicationException("Generate source code or dll library.");
            }

            /// Delete previous atlas.
            File.Delete(Path.Combine(opt.Output, String.Concat(opt.Name, ".png")));

            /// Dictionary: full filename -> rectangle in output image.
            Dictionary<string, Rectangle> outputMap;
            /// Sprite.
            Bitmap outputImageBitmap;
            /// Paths to all images.
            var images = new List<string>();

            /// Get all ".jpg" and ".png" files in folder and all subfolders.
            if (opt.Sprites != null) {
                var spritesDirectory = new DirectoryInfo(opt.Sprites);

                images.AddRange(Walkthrough.GetFileNames(spritesDirectory));
            }
            if (opt.Pictures != null) {
                var picturesDirectory = new DirectoryInfo(opt.Pictures);
                
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
            if (opt.Sprites != null) {
                var spritesDirectory = new DirectoryInfo(opt.Sprites);

                codeNameSpace.Types.Add(Walkthrough.GenerateAtlasCodeDom(spritesDirectory, outputMap));
            }
            if (opt.Pictures != null) {
                var picturesDirectory = new DirectoryInfo(opt.Pictures);

                codeNameSpace.Types.Add(Walkthrough.GeneratePicturesCodeDom(picturesDirectory, outputMap));
            }

            var codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            var generatorOptions = new CodeGeneratorOptions {
                BracingStyle = "C"
                , BlankLinesBetweenMembers = false
                , VerbatimOrder = true
            };

            /// Create file with source code.
            if (opt.GenerateSource) {
                using (var sourceWriter =
                    new StreamWriter(Path.Combine(opt.Output, String.Concat(Walkthrough.VariableName(opt.Name), ".cs")))) {
                    codeDomProvider.GenerateCodeFromCompileUnit(
                          codeUnit
                        , sourceWriter
                        , generatorOptions);
                }
            }

            CompilerParameters cp;
            if (opt.GenerateLib) {
                cp = new CompilerParameters(
                      new string[] {
                                        "sspack.exe"
                                      , "NutPackerLib.dll"
                                      , "MonoGame.Framework.dll"
                                      , "System.Runtime.dll"
                      }
                    , Path.Combine(opt.Output, String.Concat(Walkthrough.VariableName(opt.Name), ".dll"))
                    , false
                    ) {
                    GenerateInMemory = false
                };

            }
            else {
                cp = new CompilerParameters(
                      new string[] {
                        "sspack.exe"
                      , "NutPackerLib.dll"
                      , "MonoGame.Framework.dll"
                      , "System.Runtime.dll"
                      }
                    ) {
                    GenerateInMemory = true
                };
            }
            
            /// Compile the CodeDom.
            var compile = codeDomProvider.CompileAssemblyFromDom(cp, codeUnit);

            /// Print errors.
            foreach (var e in compile.Errors) {
                Console.Error.WriteLine(e.ToString());
            }

            /// If no error - save the sprite.
            if (compile.Errors.Count == 0) {
                using (var streamWriter =
                    new StreamWriter(Path.Combine(opt.Output, String.Concat(Walkthrough.VariableName(opt.Name), ".png")))) {
                    outputImageBitmap.Save(
                          streamWriter.BaseStream
                        , System.Drawing.Imaging.ImageFormat.Png
                        );
                }
            }
        }
    }
}

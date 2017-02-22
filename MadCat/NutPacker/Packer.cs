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
        /// and generate .dll or(and) source code
        /// with classes which contains rectangles.
        /// </summary>
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
                foreach (var sprites in opt.Sprites) {
                    var spritesDirectory = new DirectoryInfo(sprites);

                    images.AddRange(Walkthrough.GetFileNames(spritesDirectory));
                }
            }
            if (opt.Pictures != null) {
                foreach (var pics in opt.Pictures) {
                    var picturesDirectory = new DirectoryInfo(pics);

                    images.AddRange(Walkthrough.GetFileNames(picturesDirectory));
                }
            }

            /// Find same paths.
            var groups = images.GroupBy(name => name).Where(group => group.Count() != 1);
            if (groups.Count() != 0) {
                throw new ApplicationException(
                    "Found nested paths. Check input parameters."
                    );
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
                foreach (var sprites in opt.Sprites) {
                    var spritesDirectory = new DirectoryInfo(sprites);

                    codeNameSpace.Types.Add(Walkthrough.GenerateAtlasCodeDom(spritesDirectory, outputMap));
                }
            }
            if (opt.Pictures != null) {
                foreach (var pics in opt.Pictures) {
                    var picturesDirectory = new DirectoryInfo(pics);

                    codeNameSpace.Types.Add(Walkthrough.GeneratePicturesCodeDom(picturesDirectory, outputMap));
                }
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


            var assemblyNames = new string[] {
                  "NutPackerLib.dll"
                , "MonoGame.Framework.dll"
                , "System.Runtime.dll"
            };

            CompilerParameters cp;
            if (opt.GenerateLib) {
                cp = new CompilerParameters(
                      assemblyNames
                    , Path.Combine(opt.Output, String.Concat(Walkthrough.VariableName(opt.Name), ".dll"))
                    , false
                    ) {
                    GenerateInMemory = false
                };

            }
            else {
                cp = new CompilerParameters(assemblyNames) {
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

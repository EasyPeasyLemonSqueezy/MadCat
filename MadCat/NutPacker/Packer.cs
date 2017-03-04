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
            if (opt.Sprites.Length == 0 && opt.Tiles.Length == 0) {
                throw new ApplicationException("Nothing to pack here.");
            }

            /// Delete previous atlas.
            File.Delete(Path.Combine(opt.Output, String.Concat(opt.Name, ".png")));

            /// Dictionary: full filename -> rectangle in output image.
            Dictionary<string, Rectangle> outputMap;
            /// Texture atlas.
            Bitmap outputImageBitmap;
            /// Paths to all dirs.
            var allDirs = opt.Sprites.Concat(opt.Tiles);
            /// Paths to all images.
            var images = new List<string>();

            foreach (var dir in allDirs) {
                var dirInfo = new DirectoryInfo(dir);
                images.AddRange(Walkthrough.GetPictures(dirInfo).Select(file => file.FullName));
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
                  images
                , opt.PowerOfTwo
                , opt.Square
                , opt.MaxWidth
                , opt.MaxHeight
                , opt.Padding
                , true  /// Generate dictionary,
                , out outputImageBitmap
                , out outputMap 
                );


            var codeUnit = new CodeCompileUnit();

            var codeNameSpace = new CodeNamespace("NutPacker.Content");
            codeUnit.Namespaces.Add(codeNameSpace);

            /// Generate code.
            if (opt.Sprites.Length != 0) {
                foreach (var sprites in opt.Sprites) {
                    var spritesDirectory = new DirectoryInfo(sprites);

                    codeNameSpace.Types.Add(Walkthrough.GenerateSpriteCodeDom(spritesDirectory, outputMap));
                }
            }
            if (opt.Tiles.Length != 0) {
                foreach (var pics in opt.Tiles) {
                    var picturesDirectory = new DirectoryInfo(pics);

                    codeNameSpace.Types.Add(Walkthrough.GenerateTileCodeDom(picturesDirectory, outputMap));
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

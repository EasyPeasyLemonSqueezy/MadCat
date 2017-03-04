using System;
using System.IO;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace NutPacker
{
    internal class Walkthrough
    {
        /// <summary>
        /// sspack can recognize only these extensions.
        /// </summary>
        public static string[] Extensions = { ".png", ".jpg", ".bmp" };

        /// <summary>
        /// Get filenames from directory <paramref name="directory"/>
        /// and from all subdirectories.
        /// </summary>
        /// <param name="directory"> Directory. </param>
        /// <param name="searchOption">
        /// <see cref="SearchOption"/>,
        /// by default <see cref="SearchOption.AllDirectories"/>.
        /// </param>
        /// <returns>
        /// Array of full names (with path) of files in folder and in all subfolders.
        /// </returns>
        public static IEnumerable<FileInfo> GetPictures(
              DirectoryInfo directory
            , SearchOption searchOption = SearchOption.AllDirectories)
        {
            return directory.EnumerateFiles("*", searchOption)
                            .Where(file => Extensions.Contains(file.Extension));
        }

        /// <summary>
        /// Generate code which implements <see cref="ISpriteGroup"/>
        /// or inherited from <see cref="ISpriteSheet"/>.
        /// </summary>
        /// <remarks>
        /// Using DFS to get CodeDom.
        /// </remarks>
        /// <param name="directory"> Current directory. </param>
        /// <param name="map">
        /// Dictionary, where key - fullname of picture;
        /// value - <see cref="Rectangle"/> location in texture atlas.
        /// </param>
        /// <returns>
        /// SpriteSheet or SpriteGroup.
        /// </returns>
        public static CodeTypeDeclaration GenerateSpriteCodeDom(
              DirectoryInfo directory
            , Dictionary<string, Rectangle> map)
        {
            var dirs = directory.EnumerateDirectories();
            var pics = GetPictures(directory, SearchOption.TopDirectoryOnly).ToArray();

            /// Groups by name without spaces.
            /// check that the names don't match.
            var groups = dirs.GroupBy(dir => VariableName(dir.Name)).Where(g => g.Count() != 1);
            if (groups.Count() != 0) {
                throw new ApplicationException(String.Format(
                      "I don't know what I should do with these directories: {0}"
                    , String.Join(", ", groups.Select(g => String.Join(", ", g)))
                    ));
            }

            /// If in this directory not only files or not only another directories
            /// (It's not an <see cref="ISpriteSheet"/> and not an <see cref="ISpriteGroup"/>).
            if (dirs.Count() != 0 && pics.Count() != 0) {
                throw new ApplicationException(
                     $"Directory `{directory.Name}` "
                    + "cannot contain files and another directories at the same time.");
            }


            CodeTypeDeclaration currentClass;

            if (pics.Count() != 0) {
                /// Sort files by name,
                /// <see cref="NaturalFileInfoNameComparer"/> - uses StrCmpLogicalW from winapi
                /// Why not just <see cref="Array.Sort(Array)"/>?
                /// look:
                /// <example>
                /// <see cref="Array.Sort(Array)"/> result:
                /// file1.txt, file10.txt, file11.txt, ..., file2.txt, file20.txt, ...
                /// <see cref="Array.Sort(Array, System.Collections.IComparer)"/> result:
                /// file1.txt, file2.txt, ..., file9.txt, file10.txt, file11.txt, ...
                /// </example>
                Array.Sort(pics, new NaturalFileInfoNameComparer());

                /// Generate sprite.
                currentClass = CodeGenerator.GenerateSpriteSheetClass(
                      VariableName(directory.Name)
                    , pics.Select(pic => map[pic.FullName])
                          .ToArray()
                    );
            }
            else {
                /// Generate group of sprites.
                currentClass = CodeGenerator.GenerateSpriteGroupClass(
                    VariableName(directory.Name));
                
                foreach (var dir in dirs) {
                    /// Generate class for subdirectory.
                    var newClass = GenerateSpriteCodeDom(dir, map);
                    /// Add to current class.
                    currentClass.Members.Add(newClass);
                }
            }

            return currentClass;
        }

        /// <summary>
        /// Generate code which implemented <see cref="ITileSet"/>.
        /// </summary>
        /// <remarks>
        /// Using DFS to get CodeDom.
        /// </remarks>
        /// <param name="directory"> Current directory. </param>
        /// <param name="map">
        /// Dictionary, where key - fullname of picture;
        /// value - <see cref="Rectangle"/> location in atlas.
        /// </param>
        /// <returns>
        /// Code of tileset class.
        /// </returns>
        public static CodeTypeDeclaration GenerateTileCodeDom(
              DirectoryInfo directory
            , Dictionary<string, Rectangle> map)
        {
            var dirs = directory.EnumerateDirectories();
            var pics = GetPictures(directory, SearchOption.TopDirectoryOnly);

            /// Groups by name without spaces.
            /// check that the names don't match.
            var groups = dirs.Select(dir => dir.Name)
                             .Union(pics.Select(file => file.Name))
                             .GroupBy(name => VariableName(name))
                             .Where(g => g.Count() != 1);

            if (groups.Count() != 0) {
                throw new ApplicationException(String.Concat(
                      "I don't know what I should do with these directories or files: "
                    , String.Join(", ", groups.Select(g => String.Join(", ", g)))
                    ));
            }

            /// Generate tile set.
            CodeTypeDeclaration currentClass = CodeGenerator.GenerateTileSetClass(
                VariableName(directory.Name));

            /// Generate tiles property and add them to current class.
            foreach (var dir in dirs) {
                var newClass = GenerateTileCodeDom(dir, map);
                currentClass.Members.Add(newClass);
            }
            /// Generate tile properties and add them to current class.
            foreach (var pic in pics) {
                currentClass.Members.Add(CodeGenerator.GenerateTileProperty(
                      VariableName(Path.GetFileNameWithoutExtension(pic.Name))
                    , map[pic.FullName])
                    );
            }

            return currentClass;
        }

        /// <summary>
        /// Generate variable name.
        /// </summary>
        /// <remarks>
        /// Rules:
        /// 1. If first symbol is number - add "_" before.
        /// 2. Find first letter and make it capital.
        /// 3. Find all letters after spaces and make them capital too.
        /// 4. Replace all symbols not from [a-zA-Z0-9] to "_".
        /// </remarks>
        /// <returns>
        /// Correct name of variable.
        /// </returns>
        public static string VariableName(string name)
        {
            var r = Regex.Replace(name, @"^(\d)", m => String.Concat((m.Groups[0].Success ? "_" : String.Empty), m.Groups[0].Value));
                r = Regex.Replace(r, @"^.*?([a-zA-Z])", m => m.Groups[0].Value.ToUpper());
                r = Regex.Replace(r, @"(\s+(?<char>.))", m => m.Groups["char"].Value.ToUpper());
                r = Regex.Replace(r, @"\W", "_");

            return r;
        }
    }
}

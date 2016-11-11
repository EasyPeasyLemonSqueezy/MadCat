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
        /// Get filenames from directory <paramref name="directory"/>
        /// and from all subdirectories.
        /// </summary>
        /// <param name="directory"> Directory. </param>
        /// <param name="searchOption">
        /// <see cref="SearchOption"/>,
        /// by default <see cref="SearchOption.AllDirectories"/>.
        /// </param>
        /// <param name="extensions">
        /// Acceptable file extensions. (with dot before),
        /// by default = { ".jpg", ".png" }.
        /// </param>
        /// <returns>
        /// Array of full names (with path) of files in folder and in all subfolders.
        /// </returns>
        public static IEnumerable<string> GetFileNames(
              DirectoryInfo directory
            , SearchOption searchOption = SearchOption.AllDirectories
            , string[] extensions = null)
        {
            extensions = extensions ?? new string[] { ".jpg", ".png" };

            return GetFiles(directory, searchOption, extensions)
                   .Select(file => file.FullName);
        }

        /// <summary>
        /// Generate code which implements <see cref="ISpriteGroup"/>
        /// or inherited from <see cref="SpriteSheet"/>.
        /// </summary>
        /// <remarks>
        /// Using DFS to get CodeDom.
        /// </remarks>
        /// <param name="directory"> Current directory. </param>
        /// <param name="map">
        /// Dictionary, where key - fullname of picture;
        /// value - <see cref="Rectangle"/> location in sprite.
        /// </param>
        /// <param name="extensions">
        /// Acceptable file extensions (with dot before),
        /// by default = { ".jpg", ".png" }
        /// </param>
        /// <returns>
        /// Code of sprite/spritesheet.
        /// </returns>
        public static CodeTypeDeclaration GenerateCodeDom(
              DirectoryInfo directory
            , Dictionary<string, Rectangle> map
            , string[] extensions = null)
        {
            extensions = extensions ?? new string[] { ".jpg", ".png" };

            var dirs = directory.EnumerateDirectories();
            var files = GetFiles(directory, SearchOption.TopDirectoryOnly, extensions)
                .ToArray();

            /// Groups by name without spaces.
            /// check that the names don't match.
            var groups = dirs.GroupBy(dir => RemoveSpaces(dir.Name)).Where(g => g.Count() != 1);
            if (groups.Count() != 0) {
                throw new ApplicationException(String.Format(
                      "I don't know what I should do with these directories: {0}"
                    , String.Join(", ", groups.Select(g => String.Join(", ", g)))
                    ));
            }

            /// If in this directory not only files or not only another directories
            /// (It's not <see cref="SpriteSheet"/> and not <see cref="ISpriteGroup"/>).
            if (dirs.Count() != 0 && files.Count() != 0) {
                throw new ApplicationException(
                     $"Directory `{directory.Name}` "
                    + "cannot contain files and another directories at the same time.");
            }


            CodeTypeDeclaration currentClass;

            if (files.Count() != 0) {
                /// Sort file by name,
                /// <see cref="NaturalFileInfoNameComparer"/> - uses StrCmpLogicalW from winapi
                /// Why not just <see cref="Array.Sort(Array)"/>?
                /// look:
                /// <example>
                /// <see cref="Array.Sort(Array)"/> result:
                /// file1.txt, file10.txt, file11.txt, ..., file2.txt, file20.txt, ...
                /// <see cref="Array.Sort(Array, System.Collections.IComparer)"/> result:
                /// file1.txt, file2.txt, ..., file9.txt, file10.txt, file11.txt, ...
                /// </example>
                Array.Sort(files, new NaturalFileInfoNameComparer());

                /// Generate sprite.
                currentClass = CodeGenerator.GenerateSpriteSheetClass(
                      RemoveSpaces(directory.Name)
                    , files.Select(pic => map[pic.FullName])
                           .ToArray()
                    );
            }
            else {
                /// Generate group of sprites.
                currentClass = CodeGenerator.GenerateSpriteGroupClass(RemoveSpaces(directory.Name));
                
                foreach (var dir in dirs) {
                    /// Generate class for subdirectory.
                    var newClass = GenerateCodeDom(dir, map);
                    /// Add to current class.
                    currentClass.Members.Add(newClass);
                }
            }

            return currentClass;
        }

        /// <summary>
        /// Get files from directory <paramref name="directory"/>.
        /// </summary>
        /// <param name="directory"> Directory. </param>
        /// <param name="searchOption">
        /// <see cref="SearchOption"/>
        /// by default - <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <param name="extensions">
        /// Acceptable file extensions. (with dot before)
        /// </param>
        /// <returns>
        /// Files that meet the requirements.
        /// </returns>
        public static IEnumerable<FileInfo> GetFiles(
              DirectoryInfo directory
            , SearchOption searchOption = SearchOption.TopDirectoryOnly
            , string[] extensions = null)
        {
            return directory.EnumerateFiles("*", searchOption)
                            .Where(file => extensions?.Contains(file.Extension) ?? true);
        }

        private static string RemoveSpaces(string name)
        {
            return Regex.Replace(name, @"\s+", String.Empty);
        }
    }
}

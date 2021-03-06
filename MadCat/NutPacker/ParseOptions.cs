﻿using System;
using System.Linq;

using CommandLine;
using CommandLine.Text;

namespace NutPacker
{
    class ParseOptions : IPackOptions
    {
        [Option('n', "name", Required = true, HelpText = "Name of result files.")]
        public string Name { get; set; }

        [OptionArray('s', "sprites", HelpText = "Path to folder with sprites.")]
        public string[] Sprites { get; set; } = new string[0];

        [OptionArray('t', "tileset", HelpText = "Path to folder with tilesets.")]
        public string[] Tiles { get; set; } = new string[0];

        [Option('o', "output", Required = true, HelpText = "Path to output folder.")]
        public string Output { get; set; }

        [Option("generate-source", HelpText = "Generate .cs file with source code.")]
        public bool GenerateSource { get; set; }

        [Option("generate-dll", DefaultValue = false, HelpText = "Generate .dll lib.")]
        public bool GenerateLib { get; set; }

        [Option("require-power-of-two", DefaultValue = false, HelpText = "Require power of two output.")]
        public bool PowerOfTwo { get; set; }

        [Option("require-square", DefaultValue = false, HelpText = "Require square output.")]
        public bool Square { get; set; }

        [Option("width", DefaultValue = 256 * 256, HelpText = "Maximum atlas width.")]
        public int MaxWidth { get; set; }

        [Option("height", DefaultValue = 256 * 256, HelpText = "Maximum atlas height.")]
        public int MaxHeight { get; set; }

        [Option("padding", DefaultValue = 0, HelpText = "Image padding.")]
        public int Padding { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText {
                  Heading = new HeadingInfo("NutPacker", "0.3")
                , Copyright = new CopyrightInfo("EasyPeasyLemonSqueezy", 2017)
                , AddDashesToOption = true
                , MaximumDisplayWidth = Console.BufferWidth
            };

            help.AddPreOptionsLine("Usage: NutPacker [--sprites[-s] PATH_ONE [PATH_TWO [...]]] [--tileset[-t] PATH_ONE [PATH_TWO [...]]] [--name[-n] NAME] [--generate-source|--generate-dll]");
            help.AddOptions(this);

            if (LastParserState?.Errors.Any() == true) {
                var errors = help.RenderParsingErrorsText(this, 2); // Indent with two spaces.

                if (!String.IsNullOrEmpty(errors)) {
                    help.AddPreOptionsLine(String.Concat(Environment.NewLine, "Error:"));
                    help.AddPreOptionsLine(errors);
                }
            }

            return help;
        }

        [ParserState]
        public IParserState LastParserState { get; set; }
    }
}

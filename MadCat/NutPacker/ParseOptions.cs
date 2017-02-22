using System;
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
        public string[] Sprites { get; set; }

        [OptionArray('p', "pictures", HelpText = "Path to folder with pictures.")]
        public string[] Pictures { get; set; }

        [Option('o', "output", Required = true, HelpText = "Path to output folder.")]
        public string Output { get; set; }

        [Option("generate-source", DefaultValue = false, HelpText = "Generate .cs file with source code.")]
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
                  Heading = new HeadingInfo("NutPacker", "0.2")
                , Copyright = new CopyrightInfo("EasyPeasyLemonSqueezy", 2016)
                , AddDashesToOption = true
                , MaximumDisplayWidth = Console.BufferWidth
            };

            help.AddPreOptionsLine("Usage: NutPacker [--sprites[-s] PATH_ONE [PATH_TWO [...]]] [--pictures[-p] PATH_ONE [PATH_TWO [...]]] [--output[-o] NAME] [--generate-source [= false]]");
            help.AddOptions(this);

            if (LastParserState?.Errors.Any() == true) {
                var errors = help.RenderParsingErrorsText(this, 2); // Indent with two spaces.

                if (!string.IsNullOrEmpty(errors)) {
                    help.AddPreOptionsLine(string.Concat(Environment.NewLine, "Error:"));
                    help.AddPreOptionsLine(errors);
                }
            }

            return help;
        }

        [ParserState]
        public IParserState LastParserState { get; set; }
    }
}

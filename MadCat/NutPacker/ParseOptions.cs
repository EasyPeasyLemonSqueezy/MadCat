using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutPacker
{
    class ParseOptions
    {
        [Option('s', "sprites", HelpText = "Path to folder with sprites.")]
        public string Sprites { get; set; }

        [Option('p', "pictures", HelpText = "Path to folder with pictures.")]
        public string Pictures { get; set; }

        [Option('o', "output", Required = true, HelpText = "Path to output folder.")]
        public string Output { get; set; }

        [Option("generate-source", DefaultValue = false, HelpText = "Generate .cs file with source code.")]
        public bool GenerateSource { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText {
                  Heading = new HeadingInfo("NutPacker", "0.2")
                , Copyright = new CopyrightInfo("EasyPeasyLemonSqueezy", 2016)
                , AddDashesToOption = true
            };

            help.AddPreOptionsLine("Usage: NutPacker [--sprites[-s] PATH] [--pictures[-p] PATH] [--output[-o] PATH] [--generate-source [= false]]");
            help.AddOptions(this);

            if (LastParserState?.Errors.Any() == true) {
                var errors = help.RenderParsingErrorsText(this, 2); // indent with two spaces.

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

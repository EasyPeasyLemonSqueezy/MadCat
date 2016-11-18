using System;

namespace NutPacker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var opt = new ParseOptions();

            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, opt)) {
                try {
                    Packer.Pack(
                          opt.Name
                        , opt.Sprites
                        , opt.Pictures
                        , opt.Output
                        , opt.GenerateSource
                        );
                }
                catch (ApplicationException e) {
                    Console.Error.WriteLine(e.Message);
                }
            }
        }
    }
}
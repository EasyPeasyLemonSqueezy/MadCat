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
                    /// Generate source code by default.
                    if (!(opt.GenerateLib || opt.GenerateSource)) {
                        opt.GenerateSource = true;
                    }

                    Packer.Pack(opt);
                }
                catch (ApplicationException e) {
                    Console.Error.WriteLine($"Error: {e.Message}");
                }
                catch (OutOfMemoryException e) {
                    Console.Error.WriteLine($"Error: {e.Message}\nSplit your texture on a few parts.");
                }
            }
        }
    }
}
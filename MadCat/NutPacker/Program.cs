using System;

namespace NutPacker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /// In one wonderful day there will be the normal parameters parser, but not now :(
            if (args.Length != 3) {
                Console.Error.WriteLine("Invalid path.");
                Console.Error.WriteLine(
                      "3 arguments require: "
                    + "Path to content; "
                    + "Path to output folder; "
                    + "true/false - Save source code?");

                return;
            }
            else {
                bool generateSourceCode;

                switch (args[2]) {
                    case "true": generateSourceCode = true; break;
                    case "false": generateSourceCode = false; break;
                    default: Console.Error.WriteLine("Third argument must be \"true\" or \"false\"."); return;
                }

                try {
                    Packer.Pack(args[0], args[1], generateSourceCode);
                }
                catch (Exception e) {
                    Console.Error.WriteLine(e.Message);
                }
            }
        }
    }
}
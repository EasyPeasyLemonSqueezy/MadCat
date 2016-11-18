namespace NutPacker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var opt = new ParseOptions();
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, opt)) {
                Packer.Pack(opt.Sprites, opt.Output, opt.GenerateSource);
            }
        }
    }
}
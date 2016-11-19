﻿using CommandLine;

namespace NutPacker
{
    public interface IPackOptions
    {
        [Option] string Name { get; set; }
        [Option] string Sprites { get; set; }
        [Option] string Pictures { get; set; }
        [Option] string Output { get; set; }
        [Option] bool GenerateSource { get; set; }
        [Option] bool PowerOfTwo { get; set; }
        [Option] bool Square { get; set; }
        [Option] int MaxWidth { get; set; }
        [Option] int MaxHeight { get; set; }
        [Option] int Padding { get; set; }
    }
}

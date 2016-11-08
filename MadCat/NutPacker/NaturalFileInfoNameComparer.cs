using System.Collections.Generic;
using System.IO;

using System.Runtime.InteropServices;
using System.Security;

namespace NutPacker
{
    /// <summary>
    /// StrCmpLogicalW from winapi.
    /// Forgive me Linux... :(
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string str1, string str2);
    }

    /// <summary>
    /// IComparer for FileInfo,
    /// compare by <see cref="FileInfo.FullName"/>,
    /// using <see cref="SafeNativeMethods.StrCmpLogicalW(string, string)"/>.
    /// </summary>
    internal sealed class NaturalFileInfoNameComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo file1, FileInfo file2)
        {
            return SafeNativeMethods.StrCmpLogicalW(file1.FullName, file2.FullName);
        }
    }
}

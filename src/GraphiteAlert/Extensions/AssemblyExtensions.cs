using System;
using System.IO;
using System.Reflection;

namespace GraphiteAlert.Extensions
{
    public static class AssemblyExtensions
    {
        public static string GetDirectory(this Assembly assembly)
        {
            var codeBase = assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
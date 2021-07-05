using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace HHL.Core.Helpers
{
    public static class EmbeddedResourceHelper
    {
        public static string GetResourceAsString(Assembly assembly, string path)
        {
            string result;

            using (var stream = assembly.GetManifestResourceStream(path))
            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}

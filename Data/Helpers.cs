using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Data
{
    public static class Helpers
    {
        public static List<string> LoadStrings(string path)
        {
            var strings = new List<string>();

            using (var file = new StreamReader(path))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    strings.Add(line);
                }
            }

            return strings;
        }

        public static string LoadString(string path, int lineNo)
        {
            string line = File.ReadLines(path).Skip(lineNo).Take(1).First();

            return line;
        }
    }
}

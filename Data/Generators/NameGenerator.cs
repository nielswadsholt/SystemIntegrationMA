using System;
using System.Collections.Generic;

namespace Data.Generators
{
    public static class NameGenerator
    {
        static Random rnd = new Random();

        public static string GenerateFirstName(string gender)
        {
            string path = @"..\..\..\Data\Samples\";

            if (gender.ToLower() == "m")
            {
                path += "boynames.txt";
            }
            else
            {
                path += "girlnames.txt";
            }

            var firstnames = new List<string>();
            firstnames = Helpers.LoadStrings(path);
            var firstname = firstnames[rnd.Next(firstnames.Count)];

            return firstname;
        }

        public static string GenerateSurname()
        {
            string path = @"..\..\..\Data\Samples\surnames.txt";

            var surnames = new List<string>();
            surnames = Helpers.LoadStrings(path);
            var surname = surnames[rnd.Next(surnames.Count)];

            return surname;
        }
    }
}

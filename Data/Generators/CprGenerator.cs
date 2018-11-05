using System;

namespace Data.Generators
{
    public static class CprGenerator
    {
        private static int batchNo = 1;
        private static int BatchNo
        {
            get { return batchNo++; }
        }

        public static string Generate()
        {
            var rnd = new Random();
            var gender = new string[] { "f", "m" }[rnd.Next(0, 2)];

            return Generate(DateTime.Now, gender);
        }

        public static string Generate(string gender)
        {
            return Generate(DateTime.Now, gender);
        }

        public static string Generate(DateTime date, string gender)
        {
            string dateStr = date.ToString("ddMMyy");
            string identifier = BatchNo.ToString().PadLeft(4, '0');
            string cpr = String.Format($"{dateStr}-{identifier}");

            return cpr;
        }
    }
}

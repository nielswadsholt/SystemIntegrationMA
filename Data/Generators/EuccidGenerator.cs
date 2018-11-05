using System;

namespace Data.Generators
{
    public static class EuccidGenerator
    {
        private static int batchNo = 1;
        private static int BatchNo
        {
            get { return batchNo++; }
        }

        public static string Generate()
        {
            return Generate(DateTime.Now);
        }

        public static string Generate(DateTime date)
        {
            string dateStr = date.ToString("ddMMyyyy");
            string identifier = BatchNo.ToString().PadLeft(6, '0');
            string euccid = String.Format($"{dateStr}-{identifier}");

            return euccid;
        }
    }
}

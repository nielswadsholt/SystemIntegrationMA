using System;

namespace Data.Models
{
    public class EUCCID
    {
        public string ChristianName { get; set; }
        public string FamilyName { get; set; }
        public string EuccidNo { get; set; }
        public string Gender { get; set; }
        public string StreetAndHouseNo { get; set; }
        public string ApartmentNo { get; set; }
        public string County { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string BirthCountry { get; set; }
        public string CountryOfResidence { get; set; }

        public override string ToString()
        {
            return $"{ChristianName} {FamilyName} - {EuccidNo}";
        }

        public string ToXml()
        {
            return Serializer.Serialize(this);
        }

        public void PrintAll()
        {
            Console.WriteLine("ChristianName = " + ChristianName);
            Console.WriteLine("FamilyName = " + FamilyName);
            Console.WriteLine("EuccidNo = " + EuccidNo);
            Console.WriteLine("Gender = " + Gender);
            Console.WriteLine("StreetAndHouseNo = " + StreetAndHouseNo);
            Console.WriteLine("ApartmentNo = " + ApartmentNo);
            Console.WriteLine("County = " + County);
            Console.WriteLine("PostalCode = " + PostalCode);
            Console.WriteLine("City = " + City);
            Console.WriteLine("BirthCountry = " + BirthCountry);
            Console.WriteLine("CountryOfResidence = " + CountryOfResidence);
        }
    }
}

using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class CPR
    {
        public string CprNo { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string MaritalStatus { get; set; }

        // NB! Family IDs are changed from CPR to EUCCID
        public string Spouse { get; set; }
        public List<string> Children { get; set; }
        public string Mother { get; set; }
        public string Father { get; set; }

        public string DoctorCVR { get; set; }

        // New fields to comply with EUCCID
        public string EuccidNo { get; set; }
        public string Gender { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {Surname} - {EuccidNo}";
        }

        public string ToXml()
        {
            return Serializer.Serialize(this);
        }

        public void PrintAll()
        {
            Console.WriteLine("CprNo = " + CprNo);
            Console.WriteLine("FirstName = " + FirstName);
            Console.WriteLine("Surname = " + Surname);
            Console.WriteLine("Address1 = " + Address1);
            Console.WriteLine("Address2 = " + Address2);
            Console.WriteLine("PostalCode = " + PostalCode);
            Console.WriteLine("City = " + City);
            Console.WriteLine("MaritalStatus = " + MaritalStatus);
            Console.WriteLine("Spouse = " + Spouse);
            Console.WriteLine("Children = " + string.Join(", ", Children));
            Console.WriteLine("Mother = " + Mother);
            Console.WriteLine("Father = " + Father);
            Console.WriteLine("DoctorCVR = " + DoctorCVR);
            Console.WriteLine("EuccidNo = " + EuccidNo);
            Console.WriteLine("Gender = " + Gender);
        }
    }
}

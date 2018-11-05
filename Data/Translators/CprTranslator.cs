using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Translators
{
    public static class CprTranslator
    {
        public struct CprAddress
        {
            public string street;
            public string houseNo;
            public string apartmentNo;
        }

        public static CDM CprToCdm(CPR cpr)
        {
            CprAddress cprAddress = splitCprAddress(cpr.Address1);

            CDM cdm = new CDM()
            {
                EuccidNo = cpr.EuccidNo,
                ChristianName = cpr.FirstName,
                FamilyName = cpr.Surname,
                Gender = cpr.Gender,
                Street = cprAddress.street,
                HouseNo = cprAddress.houseNo,
                ApartmentNo = cprAddress.apartmentNo,
                //County = "",
                PostalCode = cpr.PostalCode,
                City = cpr.City,
                //BirthCountry = "",
                CountryOfResidence = "Denmark"
            };

            return cdm;
        }

        public static CPR CdmToCpr(CDM cdm)
        {
            var children = new List<string>();

            CPR cpr = new CPR()
            {
                //CprNo = "",
                EuccidNo = cdm.EuccidNo,
                FirstName = cdm.ChristianName,
                Gender = cdm.Gender,
                Surname = cdm.FamilyName,
                Address1 = assembleCprAddress(cdm),
                //Address2 = "",
                PostalCode = cdm.PostalCode,
                City = cdm.City,
                //MaritalStatus = "",
                //Spouse = "",
                Children = children,
                //Father = "",
                //Mother = "",
                //DoctorCVR = "",
            };

            return cpr;
        }

        public static CprAddress splitCprAddress(string address)
        {
            CprAddress cprAddress = new CprAddress();

            cprAddress.street = new string(address
                .TakeWhile(c => !char.IsDigit(c))
                .ToArray())
                .Trim();

            cprAddress.houseNo = new string(address
                .SkipWhile(c => !char.IsDigit(c))
                .TakeWhile(c => c != ',')
                .ToArray());

            cprAddress.apartmentNo = new string(address
                .SkipWhile(c => !char.IsDigit(c))
                .SkipWhile(c => c != ',')
                .SkipWhile(c => c == ',')
                .TakeWhile(c => true)
                .ToArray())
                .Trim();

            return cprAddress;
        }

        private static string assembleCprAddress(CDM cdm)
        {
            var cprAddress = String.Format($"{cdm.Street} {cdm.HouseNo}");

            if (!" ".Contains(cdm.ApartmentNo))
            {
                cprAddress = String.Format($"{cprAddress}, {cdm.ApartmentNo}");
            }

            return cprAddress;
        }

        private static char getGenderFromCPR(string cprNo)
        {
            char lastChar = cprNo.Last();

            if (char.IsNumber(lastChar))
            {
                throw new ArgumentException("The given CPR number does not end with a number.");
            }

            if ((int)char.GetNumericValue(lastChar) % 2 == 1)
            {
                return 'M';
            }
            else
            {
                return 'F';
            }
        }
    }
}

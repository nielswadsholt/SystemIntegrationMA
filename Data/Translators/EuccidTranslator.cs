using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Translators
{
    public static class EuccidTranslator
    {
        public static CDM EuccidToCdm(EUCCID euccid)
        {
            CDM cdm = new CDM()
            {
                EuccidNo = euccid.EuccidNo,
                ChristianName = euccid.ChristianName,
                FamilyName = euccid.FamilyName,
                Gender = euccid.Gender,
                Street = new string(euccid.StreetAndHouseNo
                .TakeWhile(c => !char.IsDigit(c))
                .ToArray())
                .Trim(),
                HouseNo = new string(euccid.StreetAndHouseNo
                .SkipWhile(c => !char.IsDigit(c))
                .TakeWhile(c => c != ',')
                .ToArray()),
                ApartmentNo = euccid.ApartmentNo,
                County = euccid.County,
                PostalCode = euccid.PostalCode,
                City = euccid.City,
                BirthCountry = euccid.BirthCountry,
                CountryOfResidence = euccid.CountryOfResidence
            };

            return cdm;
        }

        public static EUCCID CdmToEuccid(CDM cdm)
        {
            var children = new List<string>();

            EUCCID euccid = new EUCCID()
            {
                EuccidNo = cdm.EuccidNo,
                ChristianName = cdm.ChristianName,
                FamilyName = cdm.FamilyName,
                Gender = cdm.Gender,
                StreetAndHouseNo = String.Format($"{cdm.Street} {cdm.HouseNo}"),
                ApartmentNo = cdm.ApartmentNo,
                County = cdm.County,
                PostalCode = cdm.PostalCode,
                City = cdm.City,
                BirthCountry = cdm.BirthCountry,
                CountryOfResidence = cdm.CountryOfResidence
            };

            return euccid;
        }
    }
}

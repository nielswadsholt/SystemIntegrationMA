using Data.Generators;
using Data.Models;
using Data.Translators;
using System;
using System.Collections.Generic;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== Test: Split address ==========\n");

            string[] addresses = new string[]
            {
                "Ove Sprogøes Gade 25, 1 th",
                "Raadhuspladsen 1, 4. sal, lejlighed 364",
                "Magnoliavænget 22 A"
            };

            foreach (string address in addresses)
            {
                Console.WriteLine("Input:\n" + address);

                CprTranslator.CprAddress splitAddress = CprTranslator.splitCprAddress(address);

                Console.WriteLine("\nOutput:");
                Console.WriteLine("Street = '" + splitAddress.street + "'");
                Console.WriteLine("House number = '" + splitAddress.houseNo + "'");
                Console.WriteLine("Apartment number = '" + splitAddress.apartmentNo + "'\n");

                Console.ReadKey();
            }

            Console.WriteLine("\n========== Test: Translate from CPR via CDM to EU-CCID and back ==========");

            var cprTest = new CPR()
            {
                CprNo = "230287-8273",
                FirstName = "Bettina",
                Surname = "Møller-Hansen",
                Address1 = "Marselis Boulevard 25, 3 tv",
                Address2 = "c/o Hanne Weber",
                PostalCode = "8000",
                City = "Aarhus C",
                MaritalStatus = "Ugift",
                Spouse = "",
                Children = new List<string>() { "11102010-325742", "24032015-349521" },
                Mother = "06121955-342065",
                Father = "15071952-316257",
                DoctorCVR = "39227491",
                EuccidNo = "23021987-904534",
                Gender = "F",
            };

            Console.WriteLine("Input: CPR");
            cprTest.PrintAll();

            var cdmFromCprTest = CprTranslator.CprToCdm(cprTest);

            Console.WriteLine("\nMediator: CDM");
            cdmFromCprTest.PrintAll();

            Console.ReadKey();

            var euccidFromCdmTest = EuccidTranslator.CdmToEuccid(cdmFromCprTest);

            Console.WriteLine("\nOutput: EU-CCID");
            euccidFromCdmTest.PrintAll();

            Console.ReadKey();

            var cdmFromEuccidTest = EuccidTranslator.EuccidToCdm(euccidFromCdmTest);

            Console.WriteLine("\nMediator: CDM");
            cdmFromEuccidTest.PrintAll();

            Console.ReadKey();

            var cprFromCdmTest = CprTranslator.CdmToCpr(cdmFromEuccidTest);

            Console.WriteLine("\nOutput: CPR");
            cprFromCdmTest.PrintAll();

            Console.ReadKey();

            Console.WriteLine("\n========== Test: Generate names ==========");

            var boyname = NameGenerator.GenerateFirstName("M");
            Console.WriteLine(boyname);

            var girlname = NameGenerator.GenerateFirstName("F");
            Console.WriteLine(girlname);

            var surname = NameGenerator.GenerateSurname();
            Console.WriteLine(surname);

            Console.ReadKey();


            Console.WriteLine("\n========== Test: Generate euccid ==========");

            var euccid1 = EuccidGenerator.Generate();
            Console.WriteLine("euccid1: " + euccid1);

            var euccid2 = EuccidGenerator.Generate();
            Console.WriteLine("euccid2: " + euccid2);

            var euccid3 = EuccidGenerator.Generate();
            Console.WriteLine("euccid3: " + euccid3);

            Console.ReadKey();
        }
    }
}

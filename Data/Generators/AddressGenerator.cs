using Data.Models;
using System;

namespace Data.Generators
{
    public static class AddressGenerator
    {
        public static Address Generate()
        {
            Random rnd = new Random();
            string path = @"..\..\..\Data\Samples\addresses.txt";

            var addressLine = Helpers.LoadString(path, rnd.Next(7196));
            var addressSplit = addressLine.Split("\t".ToCharArray());

            var address = new Address()
            {
                Street = addressSplit[0],
                HouseNo = addressSplit[1],
                ApartmentNo = addressSplit[2],
                Address2 = addressSplit[3],
                PostalCode = addressSplit[4],
                City = addressSplit[5]
            };

            return address;
        }
    }
}

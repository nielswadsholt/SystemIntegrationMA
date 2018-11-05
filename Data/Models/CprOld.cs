using System.Collections.Generic;

namespace Data.Models
{
    public class CprOld
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string CprNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string MaritalStatus { get; set; }
        public string Spouse { get; set; }
        public List<string> Children { get; set; }
        public string Mother { get; set; }
        public string Father { get; set; }
        public string DoctorCVR { get; set; }
    }
}

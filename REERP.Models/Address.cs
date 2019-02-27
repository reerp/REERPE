using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REERP.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string LandLineNo { get; set; }
        public string CellPhoneNo { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string StreetNo { get; set; }
        public string HouseNo { get; set; }
    }
}

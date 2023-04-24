using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBancoMaster.DomainModel.Models
{
    public class Routes
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Value { get; set; }
    }
}

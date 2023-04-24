using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBancoMaster.DomainModel.Models;

namespace TestBancoMaster.DomainModel.Dto
{
    public class RoutesDto : Routes
    {
        public string Connections { get; set; }
        public string Mensage { get; set; }
    }
}

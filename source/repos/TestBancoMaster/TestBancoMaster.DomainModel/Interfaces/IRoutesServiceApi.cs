using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBancoMaster.DomainModel.Dto;
using TestBancoMaster.DomainModel.Models;

namespace TestBancoMaster.DomainModel.Interfaces
{
    public interface IRoutesServiceApi
    {
        Task AddRoutesService(List<Routes> routes);
        Task<RoutesDto> GetRoutesServiceApi(string origin, string destination);
    }
}

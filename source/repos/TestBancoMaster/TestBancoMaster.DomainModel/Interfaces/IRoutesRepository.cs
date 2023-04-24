using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBancoMaster.DomainModel.Models;

namespace TestBancoMaster.DomainModel.Interfaces
{
    public interface IRoutesRepository
    {
        Task AddRouteAsync(List<Routes> route, string path);
        Task<IEnumerable<Routes>> GetRoutesAsync(string path);
    }
}

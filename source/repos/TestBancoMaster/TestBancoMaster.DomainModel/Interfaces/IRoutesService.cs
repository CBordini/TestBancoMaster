using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBancoMaster.DomainModel.Dto;
using TestBancoMaster.DomainModel.Models;

namespace TestBancoMaster.DomainModel.Interfaces
{
    public interface IRoutesService
    {
        Task AddRoute(List<Routes> route,string path);
        Task<RoutesDto> GetCheapestRoute(string origin, string destination, string path);
        Task<IEnumerable<Routes>> GetRoutes(string path);
        //Task<RoutesDto> FindCheapestRoute(IEnumerable<Routes> routes, string origin, string destination);
        //Task DFS(IEnumerable<Routes> routes, string current, string destination, decimal cost, ref Routes cheapestRoute, HashSet<string> visited);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBancoMaster.DomainModel.Dto;
using TestBancoMaster.DomainModel.Interfaces;
using TestBancoMaster.DomainModel.Models;

namespace TestBancoMaster.Services
{
    public class RoutesService : IRoutesService
    {
        private readonly IRoutesRepository _routesRepository;

        public RoutesService(IRoutesRepository routesRepository)
        {
            _routesRepository = routesRepository;
        }

        public async Task AddRoute(List<Routes> routes,string path)
        {
            await _routesRepository.AddRouteAsync(routes, path);
        }

        public async Task<RoutesDto> GetCheapestRoute(string origin, string destination, string path)

        {
            var routes = await _routesRepository.GetRoutesAsync(path);
            return FindCheapestRoute(routes, origin, destination);
        }

        public async Task<IEnumerable<Routes>> GetRoutes(string path)
        {
            return await _routesRepository.GetRoutesAsync(path);
        }

        private RoutesDto FindCheapestRoute(IEnumerable<Routes> routes, string origin, string destination)
        {
            var cheapestRoute = new Routes { Origin = origin, Destination = destination, Value = decimal.MaxValue };
            var visited = new HashSet<string>();

            if (!routes.ToList().Any(r => r.Origin == origin) || !routes.ToList().Any(r => r.Destination == destination))
            {
                return new RoutesDto { Mensage = ($"Não foi possível encontrar uma rota de {origin} para {destination}.") };
            }

            DFS(routes, origin, destination, 0, ref cheapestRoute, visited);

            if (cheapestRoute.Value != decimal.MaxValue)
            {
                return new RoutesDto { Origin = cheapestRoute.Origin, Destination = cheapestRoute.Destination, Value = cheapestRoute.Value, Connections = string.Join("-", visited) };
            }
            else
            {
                return null;
            }
        }

        private void DFS(IEnumerable<Routes> routes, string current, string destination, decimal cost, ref Routes cheapestRoute, HashSet<string> visited)
        {
            if (current == destination && cost < cheapestRoute.Value)
            {
                cheapestRoute.Value = cost;
            }

            visited.Add(current);

            foreach (var route in routes.ToList().Where(r => r.Origin == current))
            {
                var next = route.Destination;

                if (!visited.Contains(next) && cost + route.Value < cheapestRoute.Value)
                {
                    DFS(routes, next, destination, cost + route.Value, ref cheapestRoute, visited);
                }
            }
        }
    }
}

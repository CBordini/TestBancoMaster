using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestBancoMaster.DomainModel.Dto;
using TestBancoMaster.DomainModel.Interfaces;
using TestBancoMaster.DomainModel.Models;

namespace TestBancoMaster.ConsoleApp.Service
{
    public class RoutesServiceApi : IRoutesServiceApi
    {
        private const string _urlPost = "addRoutes";
        private const string _urlGet = "getCheapestRoute";

        private readonly HttpClient _clienteHttp;

        public RoutesServiceApi(HttpClient httpClient) {
            _clienteHttp = httpClient;
        }

        public async Task AddRoutesService(List<Routes> routes)
        {
            var json = JsonSerializer.Serialize(routes);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _clienteHttp.PostAsync(_urlPost, content);
  
        }

        public async Task<RoutesDto> GetRoutesServiceApi(string origin,string destination)
        {
            RoutesDto routes = new RoutesDto();
            string query = $"?origin={origin}&destination={destination}";

            using (var response = await _clienteHttp.GetAsync(_urlGet + query))
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                routes =  await JsonSerializer.DeserializeAsync<RoutesDto>(apiResponse, options);
            }
            return routes;

        }
    }
}

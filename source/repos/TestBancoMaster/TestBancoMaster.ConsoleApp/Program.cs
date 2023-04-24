using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using TestBancoMaster.ConsoleApp.Service;
using TestBancoMaster.DomainModel.Interfaces;
using TestBancoMaster.DomainModel.Models;
using TestBancoMaster.Infra.Data.Repositories;
using TestBancoMaster.Services;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

internal class Program
{
    static async Task Main(string[] args)
    {

        try
        {
            

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Config.json", optional: false);

            IConfiguration configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRoutesService, RoutesService>()
                .AddSingleton<IRoutesRepository, RotesRepository>();

            serviceProvider.AddHttpClient<IRoutesServiceApi, RoutesServiceApi>(client =>
            {
                var urlbase = configuration["UrlBase"];

                client.BaseAddress = new Uri(urlbase);
            });

            var provider = serviceProvider.BuildServiceProvider();

            var route = provider.GetService<IRoutesService>();
            var routeApi = provider.GetService<IRoutesServiceApi>();

            Console.WriteLine("Informe o caminho para o arquivo CSV:");
            var path = Console.ReadLine();

            var getRoutes = route.GetRoutes(path);
            await routeApi.AddRoutesService(getRoutes.Result.ToList());

            string routeUsr;
            Regex regex = new Regex(@"^[a-zA-Z]+-[a-zA-Z]+$");
            do
            {
                do
                {
                    Console.WriteLine("Informe a rota desejada:");
                    routeUsr = Console.ReadLine();

                    if (!regex.IsMatch(routeUsr))
                    {
                        Console.WriteLine($"Formato de rota invalido, favor informar rota em um formato valido (DE-PARA)");
                    }
                } while (!regex.IsMatch(routeUsr));

                var origin = routeUsr.Split("-")[0].ToUpper();
                var destination = routeUsr.Split("-")[1].ToUpper();

                var cheapestRoute = await routeApi.GetRoutesServiceApi(origin, destination);

                if (cheapestRoute != null)
                {
                    Console.WriteLine($"Melhor Rota: {cheapestRoute.Connections} ao custo de {cheapestRoute.Value:C2}.");
                }
                else
                {
                    Console.WriteLine($"Não foi possível encontrar uma rota de {origin} para {destination}.");
                }
            } while (Console.ReadLine() != null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu erro durante a execução:");
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBancoMaster.DomainModel.Interfaces;
using TestBancoMaster.DomainModel.Models;

namespace TestBancoMaster.Infra.Data.Repositories
{
    public class RotesRepository : IRoutesRepository
    {
        private readonly List<Routes> _routes = new();
        private readonly string fileName = "//rotas.csv";

        public async Task AddRouteAsync(List<Routes> routes, string path)
        {
            string info;
            if (File.Exists(path + fileName))
            {
                var routesPath = GetRoutesAsync(path);
                foreach (var item in routes)
                {
                    info = $"{item.Origin},{item.Destination},{item.Value}";
                    if (!(routesPath.Result.ToList().Where(r => r.Origin == item.Origin && r.Destination == item.Destination).Count() > 0))
                    {
                        using (StreamWriter sw = File.AppendText(path + fileName))
                        {
                            sw.WriteLine(info);
                        }
                    }
                }
            }
            else
            {
                foreach(var item in routes)
                {
                    info = $"{item.Origin},{item.Destination},{item.Value}";
                    using (StreamWriter sw = File.AppendText(path + fileName))
                    {
                        sw.WriteLine(info);
                    }
                }
            }
        }

        public async Task<IEnumerable<Routes>> GetRoutesAsync(string path)
        {
            using (var reader = new StreamReader(path+fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length == 3 && decimal.TryParse(values[2], out decimal value))
                    {
                        var route = new Routes
                        {
                            Origin = values[0],
                            Destination = values[1],
                            Value = value
                        };

                        _routes.Add(route);
                    }
                }
            }
            return _routes;
        }
    }
}

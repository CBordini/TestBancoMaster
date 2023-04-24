using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using TestBancoMaster.DomainModel.Interfaces;
using FluentAssertions;
using TestBancoMaster.DomainModel.Models;
using TestBancoMaster.Services;

namespace TestBancoMaster.Tests.Services
{
    public class RoutesServiceTest
    {
        private readonly Mock<IRoutesRepository> _routesRepository;
        public RoutesServiceTest()
        {
            _routesRepository = new Mock<IRoutesRepository>();
        }

        [Fact(DisplayName = "01 - Get cheapest route fail by routes not registered")]
        public async Task Test_01()
        {
            //arrange
            var routesResult = new List<Routes>();
            _routesRepository.Setup(r => r.GetRoutesAsync(It.IsAny<string>())).ReturnsAsync(routesResult);

            var routesService = new RoutesService(_routesRepository.Object);

            //act

            var result = await routesService.GetCheapestRoute("GRU", "CDG", It.IsAny<string>());

            //assert

            result.Should().NotBeNull();
            result.Mensage.Should().Be("Não foi possível encontrar uma rota de GRU para CDG.");
        }

        [Fact(DisplayName = "02 - Get cheapest route fail by exceeded max value")]
        public async Task Test_02()
        {
            //arrange
            var routesResult = new List<Routes> { new Routes {Origin = "GRU", Destination = "CDG",Value = decimal.MaxValue } };

            _routesRepository.Setup(r => r.GetRoutesAsync(It.IsAny<string>())).ReturnsAsync(routesResult);

            var routesService = new RoutesService(_routesRepository.Object);

            //act

            var result = await routesService.GetCheapestRoute("GRU", "CDG", It.IsAny<string>());

            //assert

            result.Should().BeNull();
        }

        [Fact(DisplayName = "03 - Get cheapest route success with one route")]
        public async Task Test_03()
        {
            //arrange
            var routesResult = new List<Routes> { new Routes { Origin = "GRU", Destination = "CDG", Value = 10 } };

            _routesRepository.Setup(r => r.GetRoutesAsync(It.IsAny<string>())).ReturnsAsync(routesResult);

            var routesService = new RoutesService(_routesRepository.Object);

            //act

            var result = await routesService.GetCheapestRoute("GRU", "CDG", It.IsAny<string>());

            //assert

            result.Should().NotBeNull();
            result.Origin.Should().Be("GRU");
            result.Destination.Should().Be("CDG");
            result.Value.Should().Be(10);
            result.Connections.Should().Be("GRU-CDG");
            result.Mensage.Should().BeNull();
        }

        [Fact(DisplayName = "04 - Get cheapest route success with many routes")]
        public async Task Test_04()
        {
            //arrange
            var routesResult = new List<Routes> { new Routes{Origin = "GRU",Destination = "ORL",Value = 1 },
                                                  new Routes{Origin = "ORL",Destination = "CDG",Value = 3},
                                                  new Routes { Origin = "GRU", Destination = "CDG", Value = 10 }};

            IEnumerable<Routes> rotesEnum = routesResult.Select(r => r);

            _routesRepository.Setup(r => r.GetRoutesAsync(It.IsAny<string>())).ReturnsAsync(rotesEnum);

            var routesService = new RoutesService(_routesRepository.Object);

            //act

            var result = await routesService.GetCheapestRoute("GRU", "CDG", It.IsAny<string>());

            //assert

            result.Should().NotBeNull();
            result.Origin.Should().Be("GRU");
            result.Destination.Should().Be("CDG");
            result.Value.Should().Be(4);
            result.Connections.Should().Be("GRU-ORL-CDG");
            result.Mensage.Should().BeNull();
        }
    }
}

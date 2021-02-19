using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Abstract.Models;
using WarhammerCore.Tests.Integration.Tools;
using Xunit;

namespace WarhammerCore.Tests.Unit
{
    public class ProfessionsTests : IClassFixture<ServiceFixture>
    {
        private readonly ServiceFixture _fx;

        public ProfessionsTests(ServiceFixture fx)
        {
            _fx = fx;
        }

        [Fact]
        public async Task GetProfessions_ShouldReturnProfessions()
        {
            // Arrange
            System.IServiceProvider scope = _fx.GetScope();
            IDataRepo service = scope.GetRequiredService<IDataRepo>();
            List<string> expectedResult = new List<string>() { "ABBOT", "GAMBLER", "PIT_FIGHTER", "SERGEANT" };

            // Act
            IEnumerable<string> result = await service.GetProfessionsAsync();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("ABBOT")]
        [InlineData("GAMBLER")]
        [InlineData("PIT_FIGHTER")]
        [InlineData("SERGEANT")]
        public async Task GetProfession_ShouldReturnProfession(string professionId)
        {
            // Arrange
            System.IServiceProvider scope = _fx.GetScope();
            IDataRepo service = scope.GetRequiredService<IDataRepo>();

            // Act
            Profession result = await service.GetProfessionAsync(professionId);

            // Assert
            Assert.Equal(professionId, result.Id);
            Assert.False(result.IsAdvanced);
            Assert.Equal("profession-description", result.Description);
            Assert.Equal("profession-label", result.Label);
            Assert.Equal(100, result.MainProfile.Ws);
            Assert.Equal(100, result.SecondaryProfile.A);
            Assert.Equal(2, result.NumberOfAdvances);
        }
    }
}
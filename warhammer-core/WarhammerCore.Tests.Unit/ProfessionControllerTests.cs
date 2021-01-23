using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Abstract.Models;
using WarhammerCore.Business;
using WarhammerCore.Tests.Unit.Tools;
using WarhammerCore.WebApi.Controllers;
using WarhammerCore.WebApi.Models.Enums;
using WarhammerCore.WebApi.Models.Request;
using WarhammerCore.WebApi.Models.Response;
using Xunit;

namespace WarhammerCore.Tests.Unit
{
    public class ProfessionsControllerTests : IClassFixture<ServiceFixture>
    {
        private readonly ServiceFixture _fx;

        public ProfessionsControllerTests(ServiceFixture fx)
        {
            _fx = fx;
        }

        [Fact]
        public async Task GetProfessions_ShouldReturnProfessions()
        {
            // Arrange
            List<string> expectedResult = new List<string>() { "ABBOT", "GAMBLER", "PIT_FIGHTER", "SERGEANT" };
            Mock<IDataRepo> mockRepo = new Mock<IDataRepo>();
            mockRepo.Setup(repo => repo.GetProfessionsAsync()).ReturnsAsync(expectedResult);
            ProfessionService mockService = new ProfessionService(mockRepo.Object);
            ProfessionController controller = new ProfessionController(mockService);

            // Act
            var response = await controller.GetProfessions();
            var result = response.Result;

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(expectedResult, okObjectResult.Value);
        }

        [Fact]
        public async Task GetProfession_ReturnsOk_ForValidId()
        {
            // Arrange
            string professionId = "ANIMAL_TRAINER";
            Profession profession = new Profession()
            {
                Id = professionId,
                Label = "Label",
                Description = "Description",
                Role = "Role",
                Notes = "Notes",
                Source = "Source",
                IsAdvanced = false,
                MainProfile = null,
                SecondaryProfile = null,
                AdvanceFrom = new List<string>() { "ABBOT" },
                AdvanceTo = new List<string>() { "ADMIRAL" },
                Skills = null,
                Trappings = null,
                Talents = null,
                NumberOfAdvances = 2,
                NumberOfSkills = 0,
                NumberOfTalents = 0
            };
            Mock<IDataRepo> mockRepo = new Mock<IDataRepo>();
            mockRepo.Setup(repo => repo.GetProfessionAsync(professionId)).ReturnsAsync(profession);
            ProfessionService mockService = new ProfessionService(mockRepo.Object);
            ProfessionController controller = new ProfessionController(mockService);

            // Act
            GetProfessionRequest request = new GetProfessionRequest() { ProfessionId = professionId };
            var response = await controller.GetProfession(request);
            var result = response.Result;

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(profession, okObjectResult.Value);
        }

        [Fact]
        public async Task GetProfession_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            string professionId = "ANIMAL_TRAINER";
            Mock<IDataRepo> mockRepo = new Mock<IDataRepo>();
            // Return null.
            mockRepo.Setup(repo => repo.GetProfessionAsync(professionId)).ReturnsAsync(() => null);
            ProfessionService mockService = new ProfessionService(mockRepo.Object);
            ProfessionController controller = new ProfessionController(mockService);

            // Act
            GetProfessionRequest request = new GetProfessionRequest() { ProfessionId = professionId };
            var response = await controller.GetProfession(request);
            var result = response.Result;

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            var error = Assert.IsType<ErrorResponse>(notFoundObjectResult.Value);

            Assert.Equal(404, notFoundObjectResult.StatusCode);
            Assert.Null(error.Description);
            Assert.Equal(ErrorCode.ProfessionNotFound.ToString(), error.ErrorCode);
        }
    }
}
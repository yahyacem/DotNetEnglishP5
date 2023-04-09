using DotNetEnglishP5.Models.Repositories;
using DotNetEnglishP5.Models.Services;
using DotNetEnglishP5.Tests.Data;
using DotNetEnglishP5.Tests.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP5.Tests.Unit
{
    public class MakeServiceUnitTests : UnitTests
    {
        [Fact]
        public async void GetMakeById()
        {
            var make = SeedData.MakeEntities[0];
            var mockMakeRepository = new Mock<IMakeRepository>();
            mockMakeRepository.Setup(x => x.GetByIdAsync(make.Id)).ReturnsAsync(make);

            var mockCarRepository = new Mock<ICarRepository>();
            var mockModelRepository = new Mock<IModelRepository>();
            var mockImageRepository = new Mock<IImageRepository>();


            IMakeService makeService = new MakeService(mockCarRepository.Object, mockMakeRepository.Object, mockModelRepository.Object, mockImageRepository.Object);
            var makeResult = await makeService.GetMakeByIdAsync(make.Id);

            Assert.NotNull(makeResult);
            Assert.Equal(make.Id, makeResult.Id);
        }
        [Fact]
        public async void GetMakeByName()
        {
            var make = SeedData.MakeEntities[0];
            var mockMakeRepository = new Mock<IMakeRepository>();
            mockMakeRepository.Setup(x => x.GetByNameAsync(make.Name)).ReturnsAsync(make);

            var mockCarRepository = new Mock<ICarRepository>();
            var mockModelRepository = new Mock<IModelRepository>();
            var mockImageRepository = new Mock<IImageRepository>();


            IMakeService makeService = new MakeService(mockCarRepository.Object, mockMakeRepository.Object, mockModelRepository.Object, mockImageRepository.Object);
            var makeResult = await makeService.GetMakeByNameAsync(make.Name);

            Assert.NotNull(makeResult);
            Assert.Equal(make.Id, makeResult.Id);
        }
        [Fact]
        public async void GetAllMakes()
        {
            var listMakes = SeedData.MakeEntities;
            var mockMakeRepository = new Mock<IMakeRepository>();
            mockMakeRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(listMakes);

            var mockCarRepository = new Mock<ICarRepository>();
            var mockModelRepository = new Mock<IModelRepository>();
            var mockImageRepository = new Mock<IImageRepository>();


            IMakeService makeService = new MakeService(mockCarRepository.Object, mockMakeRepository.Object, mockModelRepository.Object, mockImageRepository.Object);
            var makesResult = await makeService.GetAllMakesAsync();

            Assert.NotNull(makesResult);
            Assert.Equal(listMakes.Count, makesResult.Count);
        }
    }
}
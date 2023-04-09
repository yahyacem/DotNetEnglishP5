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
    public class ModelServiceUnitTests : UnitTests
    {
        [Fact]
        public async void GetModelById()
        {
            var model = SeedData.ModelEntities[0];
            var mockModelRepository = new Mock<IModelRepository>();
            mockModelRepository.Setup(x => x.GetByIdAsync(model.Id)).ReturnsAsync(model);

            var mockCarRepository = new Mock<ICarRepository>();
            var mockMakeRepository = new Mock<IMakeRepository>();
            var mockImageRepository = new Mock<IImageRepository>();

            IModelService modelService = new ModelService(mockCarRepository.Object, mockMakeRepository.Object, mockModelRepository.Object, mockImageRepository.Object);
            var modelResult = await modelService.GetModelByIdAsync(model.Id);

            Assert.NotNull(modelResult);
            Assert.Equal(model.Id, modelResult.Id);
        }
        [Fact]
        public async void GetModelByName()
        {
            var model = SeedData.ModelEntities[0];
            var mockModelRepository = new Mock<IModelRepository>();
            mockModelRepository.Setup(x => x.GetByNameAsync(model.Name)).ReturnsAsync(model);

            var mockCarRepository = new Mock<ICarRepository>();
            var mockMakeRepository = new Mock<IMakeRepository>();
            var mockImageRepository = new Mock<IImageRepository>();

            IModelService modelService = new ModelService(mockCarRepository.Object, mockMakeRepository.Object, mockModelRepository.Object, mockImageRepository.Object);
            var modelResult = await modelService.GetModelByNameAsync(model.Name);

            Assert.NotNull(modelResult);
            Assert.Equal(model.Id, modelResult.Id);
        }
        [Fact]
        public async void GetAllModels()
        {
            var listModels = SeedData.ModelEntities;
            var mockModelRepository = new Mock<IModelRepository>();
            mockModelRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(listModels);

            var mockCarRepository = new Mock<ICarRepository>();
            var mockMakeRepository = new Mock<IMakeRepository>();
            var mockImageRepository = new Mock<IImageRepository>();


            IModelService modelService = new ModelService(mockCarRepository.Object, mockMakeRepository.Object, mockModelRepository.Object, mockImageRepository.Object);
            var modelsResult = await modelService.GetAllModelsAsync();

            Assert.NotNull(modelsResult);
            Assert.Equal(listModels.Count, modelsResult.Count);
        }
    }
}
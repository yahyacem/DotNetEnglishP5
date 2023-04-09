using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models;
using DotNetEnglishP5.Models.Repositories;
using DotNetEnglishP5.Models.Services;
using DotNetEnglishP5.Tests.Data;
using DotNetEnglishP5.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP5.Tests.Integration
{
    [Collection("Sequential")]
    public class ModelServiceIntegrationTests : IntegrationTests
    {
        [Fact]
        public async void GetModelById()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Model.Add(SeedData.ModelEntities[0]);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IModelService modelService = new ModelService(carRepository, makeRepository, modelRepository, imageRepository);

                // Act
                var model = await modelService.GetModelByIdAsync(1);

                // Assert
                Assert.NotNull(model);
                Assert.Equal(1, model.Id);
                Assert.Equal(SeedData.ModelEntities[0].Name, model.Name);
            }
        }
        [Fact]
        public async void GetModelByName()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Model.Add(SeedData.ModelEntities[0]);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IModelService modelService = new ModelService(carRepository, makeRepository, modelRepository, imageRepository);

                // Act
                var model = await modelService.GetModelByNameAsync(SeedData.ModelEntities[0].Name);

                // Assert
                Assert.NotNull(model);
                Assert.Equal(1, model.Id);
                Assert.Equal(SeedData.ModelEntities[0].Name, model.Name);
            }
        }
        [Fact]
        public async void GetAllModels()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Model.AddRange(SeedData.ModelEntities);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IModelService modelService = new ModelService(carRepository, makeRepository, modelRepository, imageRepository);

                // Act
                var models = await modelService.GetAllModelsAsync();

                // Assert
                Assert.NotNull(models);
                Assert.Equal(SeedData.ModelEntities.Count, models.Count);
            }
        }
        [Fact]
        public async void CreateModel()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IModelService modelService = new ModelService(carRepository, makeRepository, modelRepository, imageRepository);

                ModelViewModel modelToAdd = SeedData.ModelViewModels[0];

                // Act
                await modelService.SaveModelAsync(modelToAdd);

                var listModels = await modelService.GetAllModelsAsync();

                // Assert
                Assert.NotNull(listModels);
                Assert.NotNull(listModels.FirstOrDefault(x => x.Id == modelToAdd.Id));
                Assert.Equal(1, listModels.Count);
            }
        }
        [Fact]
        public async void DeleteModel()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Model.Add(SeedData.ModelEntities[0]);
                context.Model.Add(SeedData.ModelEntities[1]);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IModelService modelService = new ModelService(carRepository, makeRepository, modelRepository, imageRepository);

                int modelToDelete = SeedData.ModelEntities[0].Id;

                // Act
                await modelService.DeleteModelAsync(modelToDelete);
                var listModels = await modelService.GetAllModelsAsync();

                // Assert
                Assert.NotNull(listModels);
                Assert.Null(listModels.FirstOrDefault(x => x.Id == modelToDelete));
                Assert.Equal(1, listModels.Count);
            }
        }
    }
}

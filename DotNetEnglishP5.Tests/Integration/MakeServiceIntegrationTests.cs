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
    public class MakeServiceIntegrationTests : IntegrationTests
    {
        [Fact]
        public async void GetMakeById()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Make.Add(SeedData.MakeEntities[0]);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IMakeService makeService = new MakeService(carRepository, makeRepository, modelRepository, imageRepository);

                // Act
                var make = await makeService.GetMakeByIdAsync(1);

                // Assert
                Assert.NotNull(make);
                Assert.Equal(1, make.Id);
            }
        }
        [Fact]
        public async void GetMakeByName()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Make.Add(SeedData.MakeEntities[0]);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IMakeService makeService = new MakeService(carRepository, makeRepository, modelRepository, imageRepository);

                // Act
                var make = await makeService.GetMakeByNameAsync(SeedData.MakeEntities[0].Name);

                // Assert
                Assert.NotNull(make);
                Assert.Equal(1, make.Id);
                Assert.Equal(SeedData.MakeEntities[0].Name, make.Name);
            }
        }
        [Fact]
        public async void GetAllMakes()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Make.AddRange(SeedData.MakeEntities);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IMakeService makeService = new MakeService(carRepository, makeRepository, modelRepository, imageRepository);

                // Act
                var makes = await makeService.GetAllMakesAsync();

                // Assert
                Assert.NotNull(makes);
                Assert.Equal(SeedData.MakeEntities.Count, makes.Count);
            }
        }
        [Fact]
        public async void CreateMake()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IMakeService makeService = new MakeService(carRepository, makeRepository, modelRepository, imageRepository);

                MakeViewModel makeToAdd = SeedData.MakeViewModels[0];

                // Act
                await makeService.SaveMakeAsync(makeToAdd);

                var listMakes = await makeRepository.GetAllAsync();

                // Assert
                Assert.NotNull(listMakes);
                Assert.NotNull(listMakes.FirstOrDefault(x => x.Id == makeToAdd.Id));
                Assert.Equal(1, listMakes.Count);
            }
        }
        [Fact]
        public async void DeleteMake()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Make.Add(SeedData.MakeEntities[0]);
                context.Make.Add(SeedData.MakeEntities[1]);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IMakeService makeService = new MakeService(carRepository, makeRepository, modelRepository, imageRepository);

                int makeToDelete = SeedData.MakeEntities[0].Id;

                // Act
                await makeService.DeleteMakeAsync(makeToDelete);
                var listMakes = await makeService.GetAllMakesAsync();

                // Assert
                Assert.NotNull(listMakes);
                Assert.Null(listMakes.FirstOrDefault(x => x.Id == makeToDelete));
                Assert.Equal(1, listMakes.Count);
            }
        }
    }
}

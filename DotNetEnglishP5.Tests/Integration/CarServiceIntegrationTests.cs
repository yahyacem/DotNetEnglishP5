using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models.Repositories;
using DotNetEnglishP5.Models.Services;
using DotNetEnglishP5.Models;
using DotNetEnglishP5.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace DotNetEnglishP5.Tests.Integration
{
    [Collection("Sequential")]
    public class CarServiceIntegrationTests
    {
        private DbContextOptions<ApplicationDbContext> GetOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("ProductServiceRead" + Guid.NewGuid().ToString())
            .Options;
        }
        [Fact]
        public async void GetCarById()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Car.Add(SeedData.CarEntities[0]);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                ICarService carService = new CarService(carRepository, makeRepository, modelRepository, imageRepository);

                // Act
                var car = await carService.GetCarByIdAsync(1);

                // Assert
                Assert.NotNull(car);
                Assert.Equal(1, car.Id);
            }
        }
        [Fact]
        public async void GetAllCars()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Car.AddRange(SeedData.CarEntities);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                ICarService carService = new CarService(carRepository, makeRepository, modelRepository, imageRepository);

                // Act
                var cars = await carService.GetAllCarsAsync();

                // Assert
                Assert.NotNull(cars);
                Assert.Equal(SeedData.CarEntities.Count, cars.Count);
            }
        }
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
        public async void CreateCar()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                ICarService carService = new CarService(carRepository, makeRepository, modelRepository, imageRepository);

                CarViewModel carToAdd = SeedData.CarViewModels[0];

                // Act
                await carService.SaveCarAsync(carToAdd);
                var listCars = await carService.GetAllCarsAsync();

                // Assert
                Assert.NotNull(listCars);
                Assert.NotNull(listCars.FirstOrDefault(x => x.Id == carToAdd.Id));
                Assert.Equal(1, listCars.Count);
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
        public async void CreateImage()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IImageService imageService = new ImageService(carRepository, makeRepository, modelRepository, imageRepository);

                ImageViewModel imageToAdd = SeedData.ImageViewModels[0];

                // Act
                await imageService.SaveImageAsync(imageToAdd);
                var listImages = await imageService.GetAllImagesAsync();

                // Assert
                Assert.NotNull(listImages);
                Assert.NotNull(listImages.FirstOrDefault(x => x.Id == imageToAdd.Id));
                Assert.Equal(1, listImages.Count);
            }
        }
        [Fact]
        public async void CreateImages()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IImageService imageService = new ImageService(carRepository, makeRepository, modelRepository, imageRepository);

                List<ImageViewModel> imagesToAdd = SeedData.ImageViewModels;

                // Act
                await imageService.SaveImagesAsync(imagesToAdd);
                var listImages = await imageService.GetAllImagesAsync();

                // Assert
                Assert.NotNull(listImages);
                Assert.Equal(imagesToAdd.Count, listImages.Count);
            }
        }
        [Fact]
        public async void DeleteCar()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Car.Add(SeedData.CarEntities[0]);
                context.Car.Add(SeedData.CarEntities[1]);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                ICarService carService = new CarService(carRepository, makeRepository, modelRepository, imageRepository);

                int carToDelete = SeedData.CarEntities[0].Id;

                // Act
                await carService.DeleteCarAsync(carToDelete);
                var listCars = await carService.GetAllCarsAsync();

                // Assert
                Assert.NotNull(listCars);
                Assert.Null(listCars.FirstOrDefault(x => x.Id == carToDelete));
                Assert.Equal(1, listCars.Count);
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
        [Fact]
        public async void DeleteImage()
        {
            // Arrange
            using (var context = new ApplicationDbContext(GetOptions()))
            {
                context.Image.Add(SeedData.ImageEntities[0]);
                context.Image.Add(SeedData.ImageEntities[1]);
                context.SaveChanges();

                ICarRepository carRepository = new CarRepository(context);
                IMakeRepository makeRepository = new MakeRepository(context);
                IModelRepository modelRepository = new ModelRepository(context);
                IImageRepository imageRepository = new ImageRepository(context);
                IImageService imageService = new ImageService(carRepository, makeRepository, modelRepository, imageRepository);

                int imageToDelete = SeedData.ImageEntities[0].Id;

                // Act
                await imageService.DeleteImageAsync(imageToDelete);
                var listImages = await imageService.GetAllImagesAsync();

                // Assert
                Assert.NotNull(listImages);
                Assert.Null(listImages.FirstOrDefault(x => x.Id == imageToDelete));
                Assert.Equal(1, listImages.Count);
            }
        }
    }
}
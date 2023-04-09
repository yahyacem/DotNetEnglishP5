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
using DotNetEnglishP5.Tests.Models;

namespace DotNetEnglishP5.Tests.Integration
{
    [Collection("Sequential")]
    public class CarServiceIntegrationTests : IntegrationTests
    {
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
    }
}
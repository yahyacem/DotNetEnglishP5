using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using DotNetEnglishP5.Controllers;
using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models;
using DotNetEnglishP5.Models.Repositories;
using DotNetEnglishP5.Models.Services;
using DotNetEnglishP5.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using MockQueryable.Moq;
using X.PagedList;
using DotNetEnglishP5.Tests.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace DotNetEnglishP5.Tests.Unit
{
    public class CarServiceUnitTests : UnitTests
    {
        [Fact]
        public async void GetCarById()
        {
            var car = SeedData.CarEntities[0];
            var mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(x => x.GetByIdAsync(car.Id)).ReturnsAsync(car);

            var mockMakeRepository = new Mock<IMakeRepository>();
            var mockModelRepository = new Mock<IModelRepository>();
            var mockImageRepository = new Mock<IImageRepository>();


            ICarService carService = new CarService(mockCarRepository.Object, mockMakeRepository.Object, mockModelRepository.Object, mockImageRepository.Object);
            var carResult = await carService.GetCarByIdAsync(car.Id);

            Assert.NotNull(carResult);
            Assert.Equal(car.Id, carResult.Id);
        }
        [Fact]
        public async void GetAllCars()
        {
            var listCars = SeedData.CarEntities;
            var mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(listCars);

            var mockMakeRepository = new Mock<IMakeRepository>();
            var mockModelRepository = new Mock<IModelRepository>();
            var mockImageRepository = new Mock<IImageRepository>();


            ICarService carService = new CarService(mockCarRepository.Object, mockMakeRepository.Object, mockModelRepository.Object, mockImageRepository.Object);
            var carsResult = await carService.GetAllCarsAsync();

            Assert.NotNull(carsResult);
            Assert.Equal(listCars.Count, carsResult.Count);
        }
    }
}
using DotNetEnglishP5.Data;
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
    public class ImageServiceUnitTests : UnitTests
    {
        [Fact]
        public async void GetImageById()
        {
            var image = SeedData.ImageEntities[0];
            var mockImageRepository = new Mock<IImageRepository>();
            mockImageRepository.Setup(x => x.GetByIdAsync(image.Id)).ReturnsAsync(image);

            var mockCarRepository = new Mock<ICarRepository>();
            var mockMakeRepository = new Mock<IMakeRepository>();
            var mockModelRepository = new Mock<IModelRepository>();

            IImageService imageService = new ImageService(mockCarRepository.Object, mockMakeRepository.Object, mockModelRepository.Object, mockImageRepository.Object);
            var imageResult = await imageService.GetImageByIdAsync(image.Id);

            Assert.NotNull(imageResult);
            Assert.Equal(image.Id, imageResult.Id);
            Assert.NotNull(imageResult.Data);
        }
        [Fact]
        public async void GetAllImages()
        {
            var listImages = SeedData.ImageEntities;
            var mockImageRepository = new Mock<IImageRepository>();
            mockImageRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(listImages);

            var mockCarRepository = new Mock<ICarRepository>();
            var mockMakeRepository = new Mock<IMakeRepository>();
            var mockModelRepository = new Mock<IModelRepository>();

            IImageService imageService = new ImageService(mockCarRepository.Object, mockMakeRepository.Object, mockModelRepository.Object, mockImageRepository.Object);
            var imagesResult = await imageService.GetAllImagesAsync();

            Assert.NotNull(imagesResult);
            Assert.Equal(listImages.Count, imagesResult.Count);
        }

    }
}
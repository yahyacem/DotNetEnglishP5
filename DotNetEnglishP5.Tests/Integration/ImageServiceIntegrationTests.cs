using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models.Repositories;
using DotNetEnglishP5.Models.Services;
using DotNetEnglishP5.Models;
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
    public class ImageServiceIntegrationTests : IntegrationTests
    {
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

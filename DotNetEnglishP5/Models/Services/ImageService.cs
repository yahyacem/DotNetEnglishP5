using AutoMapper;
using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models.Mappers;
using DotNetEnglishP5.Models.Repositories;

namespace DotNetEnglishP5.Models.Services
{
    public class ImageService : IImageService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMakeRepository _makeRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IImageRepository _imageRepository;
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();

        public ImageService(ICarRepository carRepository, IMakeRepository makeRepository, IModelRepository modelRepository, IImageRepository imageRepository)
        {
            _carRepository = carRepository;
            _makeRepository = makeRepository;
            _modelRepository = modelRepository;
            _imageRepository = imageRepository;
        }
        public async Task<ImageViewModel> GetImageByIdAsync(int id)
        {
            var imageEntity = await _imageRepository.GetByIdAsync(id);
            return _mapper.Map<ImageViewModel>(imageEntity);
        }
        public async Task<IList<ImageViewModel>> GetImagesByCarAsync(CarViewModel car)
        {
            var imagesEntities = await _imageRepository.GetByCarAsync(_mapper.Map<Car>(car));
            var imageViewModels = new List<ImageViewModel>();

            foreach (var imageEntity in imagesEntities)
            {
                imageViewModels.Add(_mapper.Map<ImageViewModel>(imageEntity));
            }
            return imageViewModels;
        }
        public async Task<IList<ImageViewModel>> GetAllImagesAsync()
        {
            var imageEntities = await _imageRepository.GetAllAsync();
            var imageViewModels = new List<ImageViewModel>();

            foreach(Image imageEntity in imageEntities)
            {
                imageViewModels.Add(_mapper.Map<ImageViewModel>(imageEntity));
            }
            return imageViewModels;
        }
        public async Task SaveImageAsync(ImageViewModel image)
        {
            var imageToAdd = _mapper.Map<Image>(image);
            await _imageRepository.SaveAsync(imageToAdd);
        }
        public async Task SaveImagesAsync(IFormFile[] images, int carId)
        {
            foreach (IFormFile i in images)
            {
                // Get and set informations of file to upload
                var fileName = Path.GetFileName(i.FileName);
                var fileExtension = Path.GetExtension(fileName);
                var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                var imageToAdd = new Image()
                {
                    CarId = carId,
                    FileName = newFileName,
                    FileType = fileExtension,
                };

                // Convert image data to byte[]
                using (var target = new MemoryStream())
                {
                    await i.CopyToAsync(target);
                    imageToAdd.Data = target.ToArray();
                }

                // Save image
                await _imageRepository.SaveAsync(imageToAdd);
            }
        }
        public async Task SaveImageAsync(ImageViewModel images, int carId)
        {
            var imageToAdd = _mapper.Map<Image>(images);
            imageToAdd.CarId = carId;
            await _imageRepository.SaveAsync(imageToAdd);
        }
        public async Task SaveImagesAsync(IEnumerable<ImageViewModel> images)
        {
            foreach (var image in images)
            {
                var imageToAdd = _mapper.Map<Image>(image);
                await _imageRepository.SaveAsync(imageToAdd);
            }
        }
        public async Task DeleteImageAsync(int id)
        {
            await _imageRepository.DeleteAsync(id);
        }
    }
}

using AutoMapper;
using DotNetEnglishP5.Data;
using DotNetEnglishP5.Helpers;
using DotNetEnglishP5.Models.Mappers;
using DotNetEnglishP5.Models.Repositories;
using System.Linq;

namespace DotNetEnglishP5.Models.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMakeRepository _makeRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IImageRepository _imageRepository;
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();

        public CarService(ICarRepository carRepository, IMakeRepository makeRepository, IModelRepository modelRepository, IImageRepository imageRepository)
        {
            _carRepository = carRepository;
            _makeRepository = makeRepository;
            _modelRepository = modelRepository;
            _imageRepository = imageRepository;
        }
        public async Task<CarViewModel?> GetCarByIdAsync(int id)
        {
            // Get car entity from car repository
            Car? carEntity = await _carRepository.GetByIdAsync(id);

            if (carEntity == null)
            {
                return null;
            }

            // Get images from repository
            List<ImageViewModel> imageViewModels = new List<ImageViewModel>();
            if (carEntity != null)
            {
                var imagesEntities = await _imageRepository.GetByCarAsync(carEntity);
                if (imagesEntities.Count() > 0)
                {
                    foreach (var imageEntity in imagesEntities)
                    {
                        imageViewModels.Add(_mapper.Map<ImageViewModel>(imageEntity));
                    }
                }
            }

            return _mapper.Map<Car, CarViewModel>(carEntity, opt =>
            opt.AfterMap((s, d) => d.Images = imageViewModels));
        }
        public async Task<IList<CarViewModel>> GetAllCarsAsync()
        {
            IList<Car> carEntities = await _carRepository.GetAllAsync();
            List<CarViewModel> carViewModels = new List<CarViewModel>();

            foreach (Car car in carEntities)
            {
                // Get images of car
                var imageEntities = await _imageRepository.GetByCarAsync(car);
                List<ImageViewModel> imageViewModels= new List<ImageViewModel>();

                if (imageEntities.Count() > 0)
                {
                    foreach (Image imageEntity in imageEntities)
                    {
                        imageViewModels.Add(_mapper.Map<Image, ImageViewModel>(imageEntity));
                    }
                }

                // Add images to CarViewModel object
                carViewModels.Add(_mapper.Map<Car, CarViewModel>(car, opt => opt.AfterMap((s, d) => d.Images = imageViewModels)));
            }
            return carViewModels;
        }
        public async Task SaveCarAsync(CarViewModel car)
        {
            var model = await _modelRepository.GetByNameAsync(car.Model, car.Make);
            var make = await _makeRepository.GetByNameAsync(car.Make);

            if (model == null || object.Equals(model, default(Model)))
            {
                var modelToAdd = new Model()
                {
                    Name = car.Model,
                    Make = make == null ? new Make()
                    {
                        Name = car.Make
                    } : make
                };

                // Save new Model
                await _modelRepository.SaveAsync(modelToAdd);
                model = await _modelRepository.GetByNameAsync(car.Model);
            };

            var carToAdd = _mapper.Map<CarViewModel, Car>(car, opt => opt.AfterMap((s, d) =>
            {
                d.ModelId = model.Id;
            }));
            var carAdded = await _carRepository.SaveAsync(carToAdd);

            if (carAdded != null && car.ImagesInput != null)
            {
                foreach (IFormFile i in car.ImagesInput)
                {
                    var fileName = Path.GetFileName(i.FileName);
                    var fileExtension = Path.GetExtension(fileName);
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var imageToAdd = new Image()
                    {
                        CarId = carAdded.Id,
                        FileName = fileName,
                        FileType = fileExtension
                    };

                    using (var target = new MemoryStream())
                    {
                        i.CopyTo(target);
                        imageToAdd.Data = target.ToArray();
                    }

                    await _imageRepository.SaveAsync(imageToAdd);
                }
            }
        }
        public async Task UpdateCarAsync(CarViewModel car)
        {
            // Get make and model
            var model = await _modelRepository.GetByNameAsync(car.Model, car.Make);
            var make = await _makeRepository.GetByNameAsync(car.Make);

            // If model doesn't exist, create model
            if (model == null || object.Equals(model, default(Model)))
            {
                // Set up Model object and create a new Make if it doesn't exist
                var modelToAdd = new Model()
                {
                    Name = car.Model,
                    Make = make == null ? new Make()
                    {
                        Name = car.Make
                    } : make
                };

                // Save new Model
                await _modelRepository.SaveAsync(modelToAdd);
                model = await _modelRepository.GetByNameAsync(car.Model);
            };

            var carToAdd = _mapper.Map<CarViewModel, Car>(car, opt => opt.AfterMap((s, d) =>
            {
                d.ModelId = model.Id;
            }));

            // Updatte car
            var carAdded = await _carRepository.UpdateAsync(carToAdd);
        }
        public async Task DeleteCarAsync(int id)
        {
            // Delete images of car
            var imagesToRemove = await _imageRepository.GetByCarIdAsync(id);
            foreach (var image in imagesToRemove)
            {
                await _imageRepository.DeleteAsync(image.Id);
            }

            // Delete car
            await _carRepository.DeleteAsync(id);
        }
    }
}
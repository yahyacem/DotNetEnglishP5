using AutoMapper;
using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models.Mappers;
using DotNetEnglishP5.Models.Repositories;

namespace DotNetEnglishP5.Models.Services
{
    public class ModelService : IModelService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMakeRepository _makeRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IImageRepository _imageRepository;
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();

        public ModelService(ICarRepository carRepository, IMakeRepository makeRepository, IModelRepository modelRepository, IImageRepository imageRepository)
        {
            _carRepository = carRepository;
            _makeRepository = makeRepository;
            _modelRepository = modelRepository;
            _imageRepository = imageRepository;
        }
        public async Task<ModelViewModel> GetModelByIdAsync(int id)
        {
            var modelEntity = await _modelRepository.GetByIdAsync(id);
            return _mapper.Map<ModelViewModel>(modelEntity);
        }
        public async Task<ModelViewModel> GetModelByNameAsync(string name)
        {
            var modelEntity = await _modelRepository.GetByNameAsync(name);
            return _mapper.Map<ModelViewModel>(modelEntity);
        }
        public async Task<ModelViewModel> GetModelByNameAsync(string modelName, string makeName)
        {
            var modelEntity = await _modelRepository.GetByNameAsync(modelName, makeName);
            return _mapper.Map<ModelViewModel>(modelEntity);
        }
        public async Task<IList<ModelViewModel>> GetAllModelsAsync()
        {
            var modelEntitites = await _modelRepository.GetAllAsync();
            var modelViewModels = new List<ModelViewModel>();

            foreach(Model m in modelEntitites)
            {
                modelViewModels.Add(_mapper.Map<ModelViewModel>(m));
            }
            return modelViewModels;
        }
        public async Task SaveModelAsync(ModelViewModel model)
        {
            var modelToAdd = _mapper.Map<Model>(model);
            await _modelRepository.SaveAsync(modelToAdd);
        }
        public async Task DeleteModelAsync(int id)
        {
            await _modelRepository.DeleteAsync(id);
        }
    }
}

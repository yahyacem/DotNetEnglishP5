using AutoMapper;
using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models.Mappers;
using DotNetEnglishP5.Models.Repositories;

namespace DotNetEnglishP5.Models.Services
{
    public class MakeService : IMakeService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMakeRepository _makeRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IImageRepository _imageRepository;
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();

        public MakeService(ICarRepository carRepository, IMakeRepository makeRepository, IModelRepository modelRepository, IImageRepository imageRepository)
        {
            _carRepository = carRepository;
            _makeRepository = makeRepository;
            _modelRepository = modelRepository;
            _imageRepository = imageRepository;
        }
        public async Task<MakeViewModel> GetMakeByIdAsync(int id)
        {
            var makeEntity = await _makeRepository.GetByIdAsync(id);
            return _mapper.Map<MakeViewModel>(makeEntity);
        }
        public async Task<MakeViewModel> GetMakeByNameAsync(string name)
        {
            var makeEntity = await _makeRepository.GetByNameAsync(name);
            return _mapper.Map<MakeViewModel>(makeEntity);
        }
        public async Task<IList<MakeViewModel>> GetAllMakesAsync()
        {
            var makeEntities = await _makeRepository.GetAllAsync();
            var makeViewModels = new List<MakeViewModel>();

            foreach (var make in makeEntities)
            {
                makeViewModels.Add(_mapper.Map<MakeViewModel>(make));
            }
            return makeViewModels;
        }
        public async Task SaveMakeAsync(MakeViewModel model)
        {
            var makeToAdd = _mapper.Map<Make>(model);
            await _makeRepository.SaveAsync(makeToAdd);
        }
        public async Task DeleteMakeAsync(int id)
        {
            await _makeRepository.DeleteAsync(id);
        }
    }
}
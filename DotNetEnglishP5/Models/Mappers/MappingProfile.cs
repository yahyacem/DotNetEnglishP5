using AutoMapper;
using AutoMapper.Execution;
using DotNetEnglishP5.Data;

namespace DotNetEnglishP5.Models.Mappers
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Car, CarViewModel>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Model.Make.Name));
                cfg.CreateMap<Make, MakeViewModel>();
                cfg.CreateMap<Model, ModelViewModel>();
                cfg.CreateMap<Image, ImageViewModel>()
                .ForSourceMember(src => src.CarId, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Car, opt => opt.DoNotValidate());

                cfg.CreateMap<CarViewModel, Car>()
                .ForMember(dest => dest.Model, opt => opt.Ignore())
                .ForSourceMember(src => src.Make, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Images, opt => opt.DoNotValidate());
                cfg.CreateMap<MakeViewModel, Make>();
                cfg.CreateMap<ModelViewModel, Model>();
                cfg.CreateMap<ImageViewModel, Image>();
            });
            return config;
        }
    }
}

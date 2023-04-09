using AutoMapper;
using DotNetEnglishP5.Data;
using DotNetEnglishP5.Helpers;
using DotNetEnglishP5.Models;
using DotNetEnglishP5.Models.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP5.Tests.Data
{
    public static class SeedData
    {
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
        public static List<Car> CarEntities 
        {  
            get
            {
                return new List<Car>()
                {
                    new Car()
                    {
                        Id = 1,
                        VIN = "123456789",
                        ModelId = 1,
                        Model = new Model()
                        {
                            Name = "A1",
                            MakeId = 1,
                            Make = new Make()
                            {
                                Name = "Audi"
                            }
                        },
                        Trim = "Sportback",
                        Year = 2018,
                        PurchaseDate = DateTime.Now,
                        PurchasePrice = 1250.00f,
                        Repairs = null,
                        RepairCost = 0,
                        LotDate = DateTime.Now,
                        SellingPrice = 2500,
                        SaleDate = DateTime.Now
                    }, 
                    new Car()
                    {
                        Id = 2,
                        VIN = "6363463463",
                        ModelId = 3,
                        Model = new Model()
                        {
                            Name = "A3",
                            MakeId = 1,
                            Make = new Make()
                            {
                                Name = "Audi"
                            }
                        },
                        Trim = "e-tron",
                        Year = 2020,
                        PurchaseDate = DateTime.Now,
                        PurchasePrice = 1750,
                        Repairs = null,
                        RepairCost = 0,
                        LotDate = DateTime.Now,
                        SellingPrice = 3000,
                        SaleDate = DateTime.Now
                    },
                    new Car()
                    {
                        Id = 3,
                        VIN = "087057878966",
                        ModelId = 3,
                        Model = new Model()
                        {
                            Name = "X2",
                            MakeId = 2,
                            Make = new Make()
                            {
                                Name = "BMW"
                            }
                        },
                        Trim = null,
                        Year = 2013,
                        PurchaseDate = DateTime.Now,
                        PurchasePrice = 1500,
                        Repairs = null,
                        RepairCost = 500,
                        LotDate = DateTime.Now,
                        SellingPrice = 2500,
                        SaleDate = DateTime.Now
                    },
                    new Car()
                    {
                        Id = 4,
                        VIN = "087057878966",
                        ModelId = 3,
                        Model = new Model()
                        {
                            Name = "Mercedes",
                            MakeId = 2,
                            Make = new Make()
                            {
                                Name = "CLA 250"
                            }
                        },
                        Trim = "AMG",
                        Year = 2021,
                        PurchaseDate = DateTime.Now,
                        PurchasePrice = 1500,
                        Repairs = null,
                        RepairCost = 500,
                        LotDate = DateTime.Now,
                        SellingPrice = 2500,
                        SaleDate = DateTime.Now
                    }
                };
            }
        }
        public static List<Model> ModelEntities { 
            get
            {
                return new List<Model>()
                {
                    new Model()
                    {
                        Id = 1,
                        Name = "A1",
                        MakeId = 1,
                        Make = new Make()
                        {
                            Name = "Audi"
                        }
                    }, new Model()
                    {
                        Id = 2,
                        Name = "A2",
                        MakeId = 1,
                        Make = new Make()
                        {
                            Name = "Audi"
                        }
                    }, new Model()
                    {
                        Id = 3,
                        Name = "A3",
                        MakeId = 1,
                        Make = new Make()
                        {
                            Name = "Audi"
                        }
                    }, new Model()
                    {
                        Id = 4,
                        Name = "X1",
                        MakeId = 2,
                        Make = new Make()
                        {
                            Name = "BMW"
                        }
                    }, new Model()
                    {
                        Id = 5,
                        Name = "X2",
                        MakeId = 2,
                        Make = new Make()
                        {
                            Name = "BMW"
                        }
                    }
                };
            } 
        }
        public static List<Make> MakeEntities { 
            get
            {
                return new List<Make>()
                {
                    new Make()
                    {
                        Id = 1,
                        Name = "Audi"
                    },
                    new Make()
                    {
                        Id = 2,
                        Name = "BMW"
                    }
                };
            } 
        }
        public static List<Image> ImageEntities
        {
            get
            {
                var imageEntities = new List<Image>();

                var image1 = _mapper.Map<Image>(Helper.GetImageFromURL("Assets/JPEG/Audi-A1.jpg"));
                image1.Id = 1;
                image1.CarId = 1;
                var image2 = _mapper.Map<Image>(Helper.GetImageFromURL("Assets/JPEG/Audi-A2.jpeg"));
                image1.Id = 2;
                image2.CarId = 2;
                var image3 = _mapper.Map<Image>(Helper.GetImageFromURL("Assets/JPEG/Audi-A3.jpeg"));
                image1.Id = 3;
                image3.CarId = 3;
                var image4 = _mapper.Map<Image>(Helper.GetImageFromURL("Assets/JPEG/BMW-X1-1.jpeg"));
                image1.Id = 4;
                image4.CarId = 4;
                var image5 = _mapper.Map<Image>(Helper.GetImageFromURL("Assets/JPEG/BMW-X1-2.jpeg"));
                image1.Id = 5;
                image5.CarId = 4;
                var image6 = _mapper.Map<Image>(Helper.GetImageFromURL("Assets/JPEG/BMW-X2.jpeg"));
                image1.Id = 6;
                image6.CarId = 5;

                imageEntities.Add(image1);
                imageEntities.Add(image2);
                imageEntities.Add(image3);
                imageEntities.Add(image4);
                imageEntities.Add(image5);
                imageEntities.Add(image6);

                return imageEntities;
            }
        }
        public static List<CarViewModel> CarViewModels
        {
            get
            {
                var images = ImageViewModels;
                return new List<CarViewModel>()
                {
                    new CarViewModel()
                    {
                        Id = 1,
                        VIN = "123456789",
                        Make = "Audi",
                        Model = "A1",
                        Trim = "Sportback",
                        Year = 2018,
                        PurchaseDate = DateTime.Now,
                        PurchasePrice = 1250.00f,
                        Repairs = null,
                        RepairCost = 0,
                        LotDate = DateTime.Now,
                        SellingPrice = 2500,
                        SaleDate = DateTime.Now,
                        Images = new List<ImageViewModel>()
                        {
                            images[0]
                        }
                    },
                    new CarViewModel()
                    {
                        Id = 2,
                        VIN = "6363463463",
                        Make = "Audi",
                        Model = "A3",
                        Trim = "e-tron",
                        Year = 2020,
                        PurchaseDate = DateTime.Now,
                        PurchasePrice = 1750,
                        Repairs = null,
                        RepairCost = 0,
                        LotDate = DateTime.Now,
                        SellingPrice = 3000,
                        SaleDate = DateTime.Now,
                        Images = new List<ImageViewModel>()
                        {
                            images[3]
                        }
                    },
                    new CarViewModel()
                    {
                        Id = 3,
                        VIN = "087057878966",
                        Make = "BMW",
                        Model = "X2",
                        Trim = null,
                        Year = 2013,
                        PurchaseDate = DateTime.Now,
                        PurchasePrice = 1500,
                        Repairs = null,
                        RepairCost = 500,
                        LotDate = DateTime.Now,
                        SellingPrice = 2500,
                        SaleDate = DateTime.Now,
                        Images = new List<ImageViewModel>()
                        {
                            images[5]
                        }
                    },
                    new CarViewModel()
                    {
                        Id = 4,
                        VIN = "4506546405045064",
                        Make = "Mercedes",
                        Model = "CLA 250",
                        Trim = "AMG",
                        Year = 2021,
                        PurchaseDate = DateTime.Now,
                        PurchasePrice = 1500,
                        Repairs = null,
                        RepairCost = 500,
                        LotDate = DateTime.Now,
                        SellingPrice = 2500,
                        SaleDate = DateTime.Now,
                        Images = new List<ImageViewModel>()
                        {
                            images[3],
                            images[4]
                        }
                    }
                };
            }
        }
        public static List<ModelViewModel> ModelViewModels
        {
            get
            {
                return new List<ModelViewModel>
                {
                    new ModelViewModel()
                    {
                        Id = 1,
                        Name = "A1",
                        Make = new MakeViewModel()
                        {
                            Id = 1,
                            Name = "Audi"
                        }
                    },
                    new ModelViewModel()
                    {
                        Id = 2,
                        Name = "A2",
                        Make = new MakeViewModel()
                        {
                            Id = 1,
                            Name = "Audi"
                        }
                    },
                    new ModelViewModel()
                    {
                        Id = 3,
                        Name = "A3",
                        Make = new MakeViewModel()
                        {
                            Id = 1,
                            Name = "Audi"
                        }
                    },
                    new ModelViewModel()
                    {
                        Id = 4,
                        Name = "X1",
                        Make = new MakeViewModel()
                        {
                            Id = 2,
                            Name = "BMW"
                        }
                    },
                    new ModelViewModel()
                    {
                        Id = 5,
                        Name = "X2",
                        Make = new MakeViewModel()
                        {
                            Id = 2,
                            Name = "BMW"
                        }
                    }
                };
            }
        }
        public static List<MakeViewModel> MakeViewModels
        {
            get
            {
                return new List<MakeViewModel>
                {
                    new MakeViewModel()
                    {
                        Id = 1,
                        Name = "Audi"
                    },
                    new MakeViewModel()
                    {
                        Id = 2,
                        Name = "BMW"
                    }
                };
            }
        }
        public static List<ImageViewModel> ImageViewModels
        {
            get
            {
                var listImageViewModels = new List<ImageViewModel>();

                var image1 = Helper.GetImageFromURL("Assets/JPEG/Audi-A1.jpg");
                image1.Id = 1;
                var image2 = Helper.GetImageFromURL("Assets/JPEG/Audi-A2.jpeg");
                image2.Id = 2;
                var image3 = Helper.GetImageFromURL("Assets/JPEG/Audi-A3.jpeg");
                image3.Id = 3;
                var image4 = Helper.GetImageFromURL("Assets/JPEG/BMW-X1-1.jpeg");
                image4.Id = 4;
                var image5 = Helper.GetImageFromURL("Assets/JPEG/BMW-X1-2.jpeg");
                image5.Id = 5;
                var image6 = Helper.GetImageFromURL("Assets/JPEG/BMW-X2.jpeg");
                image6.Id = 6;

                listImageViewModels.Add(image1);
                listImageViewModels.Add(image2);
                listImageViewModels.Add(image3);
                listImageViewModels.Add(image4);
                listImageViewModels.Add(image5);
                listImageViewModels.Add(image6);

                return listImageViewModels;
            }
        }
    }
}


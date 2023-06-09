﻿using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models;
using DotNetEnglishP5.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace DotNetEnglishP5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMakeService _makeService;
        private readonly IModelService _modelService;
        public HomeController(ICarService carService, IMakeService makeService, IModelService modelService)
        {
            _carService = carService;
            _makeService = makeService;
            _modelService = modelService;
        }
        // GET: /
        public async Task<IActionResult> Index(string? make, string? model, int? page)
        {
            // Get cars, makes and models
            IEnumerable<CarViewModel> cars = await _carService.GetAllCarsAsync();
            var listMakes = await _makeService.GetAllMakesAsync();
            var listModels = await _modelService.GetAllModelsAsync();

            // Pass list of makes and models into ViewBag to use it on razor page
            ViewBag.Makes = listMakes;
            ViewBag.Models = listModels;

            // Filter by make
            if (!String.IsNullOrEmpty(make))
            {
                cars = cars.Where(s => s.Make.Contains(make));
                ViewBag.makeFilter = listMakes.Any(m => m.Name == make) ? make : null;
            }

            // Filter by model
            if (!String.IsNullOrEmpty(model))
            {
                cars = cars.Where(s => s.Model.Contains(model));
                ViewBag.modelFilter = listModels.Any(m => m.Name == model) ? model : null;
            }
            
            // Setup pagination
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(cars.ToPagedList(pageNumber, pageSize));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
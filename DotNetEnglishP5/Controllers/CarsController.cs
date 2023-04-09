using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models.Services;
using DotNetEnglishP5.Models.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using DotNetEnglishP5.Models;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace DotNetEnglishP5.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMakeService _makeService;
        private readonly IModelService _modelService;
        private readonly IImageService _imageService;

        public CarsController(ICarService carService, IMakeService makeService, IModelService modelService, IImageService imageService)
        {
            _carService = carService;
            _makeService = makeService;
            _modelService = modelService;
            _imageService = imageService;
        }
        // GET: Cars
        [Authorize]
        public async Task<IActionResult> Index(int? page)
        {
            var listCars = await _carService.GetAllCarsAsync();

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return listCars != null ?
                          View(listCars.ToPagedList(pageNumber, pageSize)) :
                          Problem("Entity set 'ApplicationDbContext.Car'  is null.");
        }
        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        // GET: Cars/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewData["Model"] = await _carService.GetAllCarsAsync();
            ViewBag.Makes = await _makeService.GetAllMakesAsync();
            ViewBag.Models = await _modelService.GetAllModelsAsync();

            return View();
        }
        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VIN,Make,Model,Trim,Year,PurchaseDate,PurchasePrice,Repairs,RepairCost,LotDate,SellingPrice,SaleDate,ImagesInput")] CarViewModel car)
        {
            if (ModelState.IsValid)
            {
                await _carService.SaveCarAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        // GET: Cars/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carService.GetCarByIdAsync((int)id);
            if (car == null)
            {
                return NotFound();
            }

            ViewBag.Makes = await _makeService.GetAllMakesAsync();
            ViewBag.Models = await _modelService.GetAllModelsAsync();

            return View(car);
        }
        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VIN,Make,Model,Trim,Year,PurchaseDate,PurchasePrice,Repairs,RepairCost,LotDate,SellingPrice,SaleDate,ImagesInput")] CarViewModel car, [Bind("ImagesChanged")] bool imagesChanged)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update car
                    await _carService.UpdateCarAsync(car);

                    // If images changed, update them
                    if (imagesChanged)
                    {
                        var listImagesToDelete = await _imageService.GetImagesByCarAsync(car);
                        foreach (var image in listImagesToDelete)
                        {
                            await _imageService.DeleteImageAsync(image.Id);
                        }

                        if (car.ImagesInput != null && car.ImagesInput.Count() > 0)
                        {
                            await _imageService.SaveImagesAsync(car.ImagesInput, id);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var carUpdated = _carService.GetCarByIdAsync((int)id);
            if (carUpdated == null)
            {
                return NotFound();
            }

            return View(carUpdated);
        }
        // GET: Cars/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int[]? id)
        {
            if (id == null || id.Length <= 0)
            {
                return NotFound();
            }

            var cars = await _carService.GetAllCarsAsync();
            var carNotFound = false;
            var carsToDelete = new List<CarViewModel>();

            foreach(var idToDelete in id)
            {
                var carToDelete = cars.FirstOrDefault(c => c.Id == idToDelete);
                if (carToDelete == null)
                {
                    carNotFound = true;
                    break;
                }
                carsToDelete.Add(carToDelete);
            }

            if (carNotFound)
            {
                return NotFound();
            }

            return View(carsToDelete);
        }
        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int[] id)
        {
            foreach (int idToDelete in id)
            {
                var carToDelete = await _carService.GetCarByIdAsync(idToDelete);
                if (carToDelete != null)
                {
                    await _carService.DeleteCarAsync(idToDelete);
                }
            }
            
            return RedirectToAction(nameof(Index));
        }
        private async Task<bool> CarExists(int id)
        {
            var listCars = await _carService.GetCarByIdAsync(id);
            return (listCars != null);
        }
    }
}

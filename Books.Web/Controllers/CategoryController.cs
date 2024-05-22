﻿using Books.DataAccess.Data;
using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        //Define a GET Action method for retrieving all records
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _categoryRepo.GetAll();
            return View(categories);
        }

        //Define the Create GET Action method
        public IActionResult Create()
        {
            return View();
        }
        //Define the Create POST Action method
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Seems you have entered incorrect input");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //Define the Edit GET Action method
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = _categoryRepo.Get(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        //Define the Edit POST Action method
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                _categoryRepo.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //Define the Delete GET Action method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = _categoryRepo.Get(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        //Define the Delete POST Action method
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category category = _categoryRepo.Get(x =>x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(category);
            _categoryRepo.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}

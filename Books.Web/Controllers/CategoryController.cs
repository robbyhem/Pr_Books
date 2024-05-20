using Books.Web.Data;
using Books.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Define a GET Action method for retrieving all records
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories;
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
                _context.Categories.Add(category);
                _context.SaveChanges();
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

            Category category = _context.Categories.First(x => x.Id == id);
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
                _context.Categories.Update(category);
                _context.SaveChanges();
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

            Category category = _context.Categories.First(x => x.Id == id);
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
            Category category = _context.Categories.First(x =>x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Books.Models.ViewModels;
using Books.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Books.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }

        //Define a GET Action method for retrieving all records
        public IActionResult Index()
        {
            IEnumerable<Book> books = _unitOfWork.Book.GetAll(include: "Category");
            return View(books);
        }

        //Define the Create GET Action method
        public IActionResult Upsert(int? id)
        {
            BookVM bookVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Book = new Book()
            };

            if (id == null || id == 0)
            {
                //Create new record
                return View(bookVM);
            }
            else
            {
                //Update existing record
                bookVM.Book = _unitOfWork.Book.Get(x => x.Id == id);
                return View(bookVM);
            }
        }
        //Define the Create POST Action method
        [HttpPost]
        public IActionResult Upsert(BookVM bookVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoothPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRoothPath, @"images\book");

                    if (!string.IsNullOrEmpty(bookVM.Book.ImageUrl))
                    {
                        //removing old images 
                        var oldImagePath = Path.Combine(wwwRoothPath, bookVM.Book.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    //adding or updating images
                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    bookVM.Book.ImageUrl = @"\images\book\" + fileName;
                }

                if (bookVM.Book.Id == 0)
                {
                    _unitOfWork.Book.Add(bookVM.Book);
                    _unitOfWork.Save();
                    TempData["success"] = "Book Added Successfully";
                }
                else
                {
                    _unitOfWork.Book.Update(bookVM.Book);
                    _unitOfWork.Save();
                    TempData["success"] = "Book Updated Successfully";
                }

                return RedirectToAction("Index");
            }
            else
            {
                bookVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                return View(bookVM);
            }
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Book> books = _unitOfWork.Book.GetAll(include: "Category");
            return Json(new { data = books });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var bookToDelete = _unitOfWork.Book.Get(x => x.Id == id);
            if (bookToDelete == null)
            {
                return Json(new { success = false, message = "Book not found" });
            }
            
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, bookToDelete.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Book.Remove(bookToDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete was successful" });
        }

        #endregion
    }
}

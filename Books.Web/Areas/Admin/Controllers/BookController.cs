using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Define a GET Action method for retrieving all records
        public IActionResult Index()
        {
            IEnumerable<Book> books = _unitOfWork.Book.GetAll();
            return View(books);
        }

        //Define the Create GET Action method
        public IActionResult Create()
        {
            return View();
        }
        //Define the Create POST Action method
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Book.Add(book);
                _unitOfWork.Save();
                TempData["success"] = "Book Added Successfully";
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

            Book book = _unitOfWork.Book.Get(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        //Define the Edit POST Action method
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Book.Update(book);
                _unitOfWork.Save();
                TempData["success"] = "Book Updated Successfully";
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

            Book book = _unitOfWork.Book.Get(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        //Define the Delete POST Action method
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Book book = _unitOfWork.Book.Get(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            _unitOfWork.Book.Remove(book);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}

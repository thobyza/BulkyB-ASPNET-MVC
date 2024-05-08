using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class BookController : Controller
    {

        private readonly ApplicationDBContext _db;

        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }

        // ============== Index View ===================
        public IActionResult Index()
        {
            IEnumerable<Book> objBookList = _db.Books;
            return View(objBookList);
        }

        // ============== Create View ===================
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {
         if (ModelState.IsValid)
            {
                _db.Books.Add(obj);
                _db.SaveChanges();

                TempData["success"] = "New Book has been added";

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // ============== Edit View ===================

    }
}

using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        // tell the ApplicationDBContext to communicate with the database
        // so we can retrieve data from there
        private readonly ApplicationDBContext _db;

        public CategoryController(ApplicationDBContext db)
        {
            // this will implement/populate the data (from database )so we can access it
            // and do CRUD operations eventually
            _db = db;
        }

        public IActionResult Index()
        {
            // fetch all the category data, covert it to a List, and assign it to the variable
            //var objCategoryList = _db.Categories.ToList();

            // lets change from var to IEnumerable
            IEnumerable<Category> objCategoryList = _db.Categories;

            return View(objCategoryList);
        }

        // GET
        public IActionResult Create()
        {

            return View();  
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken] // key must be valid, to help and prevent cross site forgery attack
        public IActionResult Create(Category obj)
        {
            /* handle validation for submitted data
             * eg: if name input is empty => in model, name cant be empty
             */

            if (ModelState.IsValid) // this is a method from .net core if the model is valid or not
            {
                _db.Categories.Add(obj);
                _db.SaveChanges(); // post to the Db and save changes
                return RedirectToAction("Index"); // direct to Index() action
            }
            return View(obj);
        }
    }
}

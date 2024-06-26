﻿using BulkyBookWeb.Data;
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

        // ============== Create View ===================
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
            // check if name input = displayOrder input
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // adding custom error to the Name property, for the argument string ("Name") match it inside the model
                ModelState.AddModelError("Name", "The display order can't match the name input");
            }

            /* handle validation for submitted data
             * eg: if name input is empty => in model, name cant be empty
             */
            if (ModelState.IsValid) // this is a method from .net core if the model is valid or not
            {
                _db.Categories.Add(obj);
                _db.SaveChanges(); // post to the Db and save changes

                // showing alert based on TempData, storing a message string with the key of "success"
                TempData["success"] = "Category created successfully"; 

                return RedirectToAction("Index"); // direct to Index() action
            }
            return View(obj);
        }

        // ============== Edit View ===================
        
        // GET
        // retrieve/GET data based on id
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // [!] ways to assign the retrieved data (based on ID) to a variable

            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order can't match the name input");
            }

            if (ModelState.IsValid) 
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully"; 
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // ============== Delete View ===================
        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // DELETE
        //[HttpPost, ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id) 
            // why name is different here Delete"Post" ? bcs we cant have the same 'Name && Parameter' for action method
            // Delete(int? id) is already exist above
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category deleted successfully"; 
            return RedirectToAction("Index");
        }

    }
}

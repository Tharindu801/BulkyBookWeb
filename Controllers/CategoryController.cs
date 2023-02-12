using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategories = _db.categories.OrderBy(x => x.Displayorder).ToList();
            return View(objCategories);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.Displayorder.ToString())
            {
                ModelState.AddModelError("name", "The Name and Display Order cannot to be same. !.");
            }

            if (ModelState.IsValid) { 
            
                _db.categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Created Successfuly ! ";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var category = _db.categories.Find(id);

            if(category == null)
            {
                return NotFound();
            }


            return View(category);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.Displayorder.ToString())
            {
                ModelState.AddModelError("name", "The Name and Display Order cannot to be same. !.");
            }

            if (ModelState.IsValid)
            {

                _db.categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Updated Successfuly ! ";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _db.categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }


            return View(category);
        }

        //Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.categories.Find(id);

            //var obj = _db.categories.First(c=> c.Id==id);

            if (obj == null)
            {
                return NotFound();
            }


            _db.categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category Deleted Successfuly ! ";
            return RedirectToAction("Index");
        }
    }
}

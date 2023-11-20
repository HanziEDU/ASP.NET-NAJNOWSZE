using Labolatorium3___app.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Labolatorium3___app.Controllers
{
    public class ProductController : Controller
    {   
        static Dictionary<int, Product> _product = new Dictionary<int, Product>();
        static int id = 1;

        public IActionResult Index()
        {
            return View(_product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                model.Id = id++;
                _product.Add(model.Id, model);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_product[id]);
        }

        [HttpPost]
        public IActionResult Update(Product model)
        {
            if (ModelState.IsValid)
            {
                _product[model.Id] = model;
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_product[id]);
        }

        [HttpPost]
        public IActionResult Delete(Product model)
        {
            _product.Remove(model.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(_product[id]);
        }
    }
}

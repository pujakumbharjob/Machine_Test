using Machine_Test.Model;
using Machine_Test.Persister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Machine_Test.UI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            List<Product> products = ProductList.GetPIVOTList();

            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Product product = ProductPersister.Get();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductPersister productPersister = ProductPersister.GetPersister();
                productPersister.Insert(product);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Int32 ProductId)
        {

            Product product = ProductPersister.GetProduct(ProductId);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductPersister productPersister = ProductPersister.GetPersister();
                productPersister.Update(product);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Int32 ProductId)
        {
            ProductPersister productPersister = ProductPersister.GetPersister();
            Product product = ProductPersister.GetProduct(ProductId);
            productPersister.Delete(product);
            return RedirectToAction("Index");
        }
     

    }
}
using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.DataAcces.InMemory;
using Shop.DataAcces.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.UserUI.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        //ProductCategoryRepository context;
        IRepository<ProductCategory> context;
        public ProductCategoryController()
        {
            context = new SQlRepository<ProductCategory>(new MyContext());
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductCategory c = new ProductCategory();
            return View(c);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory product)
        {
            if (ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

        }
        public ActionResult Edit(int id)
        {
            try
            {
                ProductCategory c = context.FindById(id);
                if (c == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(c);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory cat, int id)
        {

            try
            {
                ProductCategory prodToEdit = context.FindById(id);
                if (prodToEdit == null)
                {
                    return HttpNotFound();
                }
                else
                {

                    if (ModelState.IsValid)
                    {
                        return View(cat);
                    }
                    else
                    {
                        //context.Update(product);
                        prodToEdit.Category = cat.Category;
                       
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                ProductCategory p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                ProductCategory c = context.FindById(id);
                if (c == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    context.Delete(id);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
    }
}

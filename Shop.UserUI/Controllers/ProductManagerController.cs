﻿using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.DataAcces.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.UserUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository contextCategory;
        public ProductManagerController()
        {
            context = new ProductRepository();
            contextCategory = new ProductCategoryRepository();
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = contextCategory.Collection();
           // Product p = new Product();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
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
                Product p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
                    viewModel.Product = p;
                    viewModel.ProductCategories = contextCategory.Collection();
                    return View(viewModel);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, int id)
        {

            try
            {
                Product prodToEdit = context.FindById(id);
                if (prodToEdit == null)
                {
                    return HttpNotFound();
                }
                else
                {

                    if (ModelState.IsValid)
                    {
                        return View(product);
                    }
                    else
                    {
                        //context.Update(product);
                        prodToEdit.Name = product.Name;
                        prodToEdit.Description = product.Description;
                        prodToEdit.Price = product.Price;
                        prodToEdit.Image = product.Image;
                        prodToEdit.Category = product.Category;
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
                Product p = context.FindById(id);
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
                Product p = context.FindById(id);
                if (p == null)
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
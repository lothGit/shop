using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.DataAcces.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.UserUI.Controllers
{
    public class AchatController : Controller
    {
        IRepository<Product> context;
        List<Product> lstProd = new List<Product>();
        decimal total = 0;


        public AchatController()
        {
            context = new SQLRepository<Product>(new MyContext());

        }
        public ActionResult Ajouter(int id)
        {
            Product p = context.FindById(id);
            if (Session["Products"] == null)
            {
                lstProd.Add(p);
                Session["Products"] = lstProd;
                Session["nbProd"] = 1;
            }
            else
            {
                lstProd = (List<Product>)Session["Products"];
                lstProd.Add(p);
                Session["Products"] = lstProd;

                //Total
                foreach (var item in lstProd)
                {
                    total += item.Price;
                }

                Session["total"] = total;
                Session["nbProd"] = lstProd.Count;

            }
            return RedirectToAction("Index", "home");

        }

        public ActionResult Panier()
        {
            lstProd = (List<Product>)Session["Products"];
            return View(lstProd);
        }
    }
}
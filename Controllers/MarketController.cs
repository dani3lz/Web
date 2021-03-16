using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using BusinessLogic;
using BusinessObject;

namespace eUseControl.Web.Controllers
{
    public class MarketController : Controller
    {
          // GET: Market
          public ActionResult Books()
          {
               var result = new UserBL().Connect("Category");
               UserData u = new UserData();
               u.Category = new List<string> { } ;
               foreach (var r in result)
               {
                    u.Category.Add(r.GetValue("name").ToString());
               }
               u.Books = new List<string> { "Cartea #1", "Cartea #2", "Cartea #3", "Cartea #4", "Cartea #5", "Cartea #6", "Cartea #7" };
               return View(u);
          }
    }
}
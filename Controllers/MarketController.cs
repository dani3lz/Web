using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class MarketController : Controller
    {
          // GET: Market
          public ActionResult Main()
          {
               UserData u = new UserData();
               u.Category = new List<string> { "TOP", "Drama", "Fantastic", "Biografie", "RO", "RU", "EN", "Audio" };
               u.Books = new List<string> { "Cartea #1", "Cartea #2", "Cartea #3", "Cartea #4", "Cartea #5", "Cartea #6", "Cartea #7" };
               return View(u);
          }
    }
}
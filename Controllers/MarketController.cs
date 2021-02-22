using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace eUseControl.Web.Controllers
{
    public class MarketController : Controller
    {
          // GET: Market
          public ActionResult Main()
          {
               var client = new MongoClient("mongodb://localhost:27017");
               var db = client.GetDatabase("BOOKShop");
               var books = db.GetCollection<BsonDocument>("Books");

               var category = db.GetCollection<BsonDocument>("Category");
               var result = category.Find(new BsonDocument()).ToList();


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
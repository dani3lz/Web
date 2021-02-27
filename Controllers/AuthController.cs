using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Web.Models;
using MongoDB.Driver;
using MongoDB.Bson;


namespace eUseControl.Web.Controllers
{
     public class AuthController : Controller
     {
         

          // GET: Auth
          public ActionResult Login()
          {
               if(Session["Username"] != null)
               {
                    return RedirectToAction("Books", "Market");
               }
               return View();
          }
          public ActionResult Register()
          {
               if (Session["Username"] != null)
               {
                    return RedirectToAction("Books", "Market");
               }
               return View();
          }

          [HttpPost]
          public ActionResult Login(UserLogin log)
          {
               var client = new MongoClient("mongodb://localhost:27017");
               var db = client.GetDatabase("BOOKShop");
               var users = db.GetCollection<BsonDocument>("Users");
               var res = users.Find(new BsonDocument()).ToList();
               var Entered = false;
               foreach (var r in res)
               {
                    if (r.GetValue("username").ToString() == log.Username && r.GetValue("password").ToString() == log.Password)
                    {

                         Entered = true;
                         break;
                    }
               }

               if (Entered)
               {
                    Session["Username"] = log.Username;
                    return RedirectToAction("Books", "Market");
               }
               else
               {
                    ViewBag.Notification = "Username-ul or Parola sunt incorecte!";
                    return View();
               }
          }

          [HttpPost]
          public ActionResult Register(UserRegistration reg)
          {
               var client = new MongoClient("mongodb://localhost:27017");
               var db = client.GetDatabase("BOOKShop");
               var users = db.GetCollection<BsonDocument>("Users");
               var result = users.Find(new BsonDocument()).ToList();

               foreach(var r in result)
               {
                    if (reg.Username == r.GetValue("username"))
                    {
                         ViewBag.Notification = "Username-ul deja exista!";
                         return View();
                    }
                    if(reg.Email == r.GetValue("email"))
                    {
                         ViewBag.Notification = "Email-ul deja exista!";
                         return View();
                    }
               }

               var document = new BsonDocument
               {
                    {"username", reg.Username },
                    {"email", reg.Email },
                    {"password", reg.Password },
                    {"first", "" },
                    {"last", "" },
                    {"adress", "" },
                    {"phone", "" },
               };

               users.InsertOneAsync(document);

               return RedirectToAction("Login", "Auth");
          }
     }
}
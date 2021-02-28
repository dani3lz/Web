using eUseControl.Web.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
     public class UserController : Controller
     {
          // GET: User
          [HttpGet]
          public ActionResult Profile()
          {
               if (Session["Username"] != null)
               {
                    return View();
               }
               if (Request.UrlReferrer != null)
               {
                    return Redirect(Request.UrlReferrer.PathAndQuery);
               }
               else
               {
                    return RedirectToAction("Index", "Home");
               }
          }

          [HttpPost]
          public ActionResult Profile(UserProfil changes, string button)
          {
               if (Session["Username"] != null)
               {
                    changes.Username = Session["Username"].ToString();

                    var client = new MongoClient("mongodb://localhost:27017");
                    var db = client.GetDatabase("BOOKShop");
                    var users = db.GetCollection<BsonDocument>("Users");

                    var filter = Builders<BsonDocument>.Filter.Eq("username", changes.Username);

                    if (button == "general")
                    {
                         if (changes.First == null || changes.Last == null || changes.Telefonul == null || changes.Adresa == null)
                         {

                         }
                         else
                         {
                              var update1 = Builders<BsonDocument>.Update.Set("first", changes.First);
                              var update2 = Builders<BsonDocument>.Update.Set("last", changes.Last);
                              var update3 = Builders<BsonDocument>.Update.Set("phone", changes.Telefonul);
                              var update4 = Builders<BsonDocument>.Update.Set("adress", changes.Adresa);

                              users.UpdateOne(filter, update1);
                              users.UpdateOne(filter, update2);
                              users.UpdateOne(filter, update3);
                              users.UpdateOne(filter, update4);
                              ViewBag.GeneralSucces = "Modificarile au fost salvate!";
                         }
                         return View();
                    }

                    if (button == "securitymail")
                    {
                         var result = users.Find(new BsonDocument { { "username", changes.Username } }).ToList();
                         foreach(var r in result)
                         {
                              if(changes.Email == r.GetValue("email").ToString() && changes.PasswordCurent == r.GetValue("password").ToString())
                              {
                                   var update = Builders<BsonDocument>.Update.Set("email", changes.ReEmail);

                                   users.UpdateOne(filter, update);
                              }
                              else
                              {
                                   if (changes.Email != r.GetValue("email").ToString())
                                   {
                                        ViewBag.Notification = "Email-ul curent nu coincide";
                                   }
                                   else
                                   {
                                        ViewBag.Notification = "Parola curenta nu coincide";
                                   }
                              }
                         }
                         return View();
                    }

                    if (button == "securitypass")
                    {
                         var result = users.Find(new BsonDocument { { "username", changes.Username } }).ToList();
                         foreach (var r in result)
                         {
                              if (changes.PasswordCurent == r.GetValue("password").ToString() && changes.Password == changes.RePassword)
                              {
                                   var update = Builders<BsonDocument>.Update.Set("password", changes.Password);

                                   users.UpdateOne(filter, update);
                              }
                              else
                              {
                                   if (changes.PasswordCurent != r.GetValue("password").ToString())
                                   {
                                        ViewBag.Notification2 = "Parola curenta nu coincide";
                                   }
                                   else
                                   {
                                        ViewBag.Notification2 = "Parola noua nu coincide";
                                   }
                              }
                         }

                         return View();
                    }
               }
               return View("Index", "Home");
          }
     }

}
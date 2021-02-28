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

          public UserLogin Checkcookie()
          {
               UserLogin log = null;
               string username = string.Empty;
               string pass = string.Empty;

               try
               {
                    if (Request.Cookies["username"].Value != null)
                    {
                         username = Request.Cookies["username"].Value;
                    }
                    if (Request.Cookies["pass"].Value != null)
                    {
                         pass = Request.Cookies["pass"].Value;
                    }
                    if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(pass))
                    {
                         log = new UserLogin { Username = username, Password = pass, Remember = true };
                    }
               }
               catch (Exception ex) { }

               return log;
          }

          // GET: Auth
          [HttpGet]
          public ActionResult Login()
          {
               
               if (Session["Username"] != null)
               {
                    return RedirectToAction("Books", "Market");
               }
               UserLogin log = Checkcookie();
               if (log != null)
               {
                    Login(log);
                    if (Request.UrlReferrer != null)
                    {
                         return Redirect(Request.UrlReferrer.PathAndQuery);
                    }
                    else
                    {
                         return RedirectToAction("Index", "Home");
                    }
               }
               return View();
          }

          public ActionResult Register()
          {
               if (Session["Username"] != null)
               {
                    if (Request.UrlReferrer != null)
                    {
                         return Redirect(Request.UrlReferrer.PathAndQuery);
                    }
                    else
                    {
                         return RedirectToAction("Index", "Home");
                    }
               }
               return View();
          }

          public ActionResult Logout()
          {
               if (Session["Username"] != null)
               {
                    Session.Abandon();
               }
               if (Response.Cookies["username"].Value != null || Response.Cookies["pass"].Value != null)
               {
                    Response.Cookies.Clear();
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
                    if (log.Remember)
                    {
                         HttpCookie ckusername = new HttpCookie("username");
                         ckusername.Expires = DateTime.Now.AddSeconds(3600);
                         ckusername.Value = log.Username;
                         Response.Cookies.Add(ckusername);
                         HttpCookie ckpass = new HttpCookie("pass");
                         ckpass.Expires = DateTime.Now.AddSeconds(3600);
                         ckpass.Value = log.Password;
                         Response.Cookies.Add(ckpass);
                    }
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

               if (reg.Password != reg.RePassword)
               {
                    return View();
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
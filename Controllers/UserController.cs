using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using BusinessObject;

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
               if (Request.UrlReferrer != null && Request.UrlReferrer.PathAndQuery != "/User/Profile")
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
                    (string Error, int nr) = new UserBL().ChangeProfile(changes, button, "Users");

                    // Errors
                    if(nr == 0)
                    {
                         return View();
                    }
                    else
                    {
                         if(nr == 1)
                         {
                              ViewBag.Notification = Error;
                              return View();
                         }
                         else
                         {
                              if(nr == 2)
                              {
                                   ViewBag.Notification2 = Error;
                                   return View();
                              }
                         }
                    }
               }
               return View("Index", "Home");
          }
     }

}
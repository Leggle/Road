using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Road.Models;

namespace Road.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string id, string password)
        {
            using (Road.Models.DB.RoadsEntities dbContext = new Models.DB.RoadsEntities())
            {
                var user = dbContext.tblUsers.Where(t => t.UserID.Equals(id) && t.Password.Equals(password));

                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(id))
                        ModelState.AddModelError("", "The login id or password cannot be empty.");
                    else
                    {
                        if (user.Any())
                        {
                            Session["user"] = new UserLogin() { UserID = id };
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Response.Write("<script language=javascript>alert('The login id or password is incorrect.');</script>");
                            ModelState.AddModelError("", "The login id or password provided is incorrect.");
                        }
                    }
                }

                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login","Login");
        }
    }
}
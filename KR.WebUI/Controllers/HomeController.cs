using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KR.WebUI.Infrastructure;
using KR.Domain.HelperClasses;

namespace KR.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.Name = System.Web.HttpContext.Current.Session["Name"];
        }

        [KRAuth]
        public ViewResult Index()
        {
            return View("Index");
        }

        
        public ViewResult Login(int id = 0)
        {
            ViewBag.id = id;
            return View("Login");
        }

        [KRAuth]
        public void LogOut()
        {
            Session.Clear();
            Response.Redirect("/Home/Login");
        }

        [HttpPost]
        public void Process(string userName, string passWord)
        {
            if (SessionHandler.authenticate(userName, passWord))
            {
                Response.Redirect("/Home/Index");
                Response.End();
            }
            else
            {
                if (HttpContext.Session["uid"] != null)
                {
                    if (SessionHandler.is_user_locked((int)HttpContext.Session["uid"]))
                    {
                        Response.Redirect("/Home/Login/2");
                    }
                    else
                    {
                        Response.Redirect("/Home/Login/1");
                    }
                }
                else
                {
                    Response.Redirect("/Home/Login/1");
                }


                Response.End();
            }
        }

    }
}

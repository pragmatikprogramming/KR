using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KR.Domain.HelperClasses;
using KR.WebUI.Infrastructure;
using KR.Domain.Abstract;
using KR.Domain.Models;
using KR.Domain.Entities;
using KR.WebUI.Infrastructure;

namespace KR.WebUI.Controllers
{
    
    public class UserController : Controller
    {
        private IUserRepository UserRepository;

        public UserController(IUserRepository userRepo)
        {
            UserRepository = userRepo;
            ViewBag.Name = System.Web.HttpContext.Current.Session["Name"];
        }

        [KRAuth]
        public ActionResult Index()
        {
            return View("UsersManage");
        }

        [KRAuth]
        [HttpGet]
        public ActionResult Add()
        {
            User m_User = new User();
            return View("UserAdd", m_User);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult Add(User m_User)
        {
            if (ModelState.IsValid)
            {
                if (UserRepository.Create(m_User.UserName, m_User.FirstName, m_User.LastName, m_User.Email, m_User.PassWord))
                {
                    List<User> CMSUsers = UserRepository.RetrieveAll();

                    return View("Manage", CMSUsers);
                }
                else
                {
                    ViewBag.errorMessage = "The Username you selected is already in use";

                    return View("UserAdd", m_User);
                }
            }
            else
            {
                return View("UserAdd", m_User);
            }
        }

        [KRAuth]
        public ActionResult Manage()
        {
            List<User> CMSUsers = UserRepository.RetrieveAll();

            return View(CMSUsers);
        }

        [KRAuth]
        public ActionResult Delete()
        {
            int uid;
            int.TryParse((string)Url.RequestContext.RouteData.Values["id"], out uid);
            UserRepository.Delete(uid);
            List<User> CMSUsers = UserRepository.RetrieveAll();
            return View("Manage", CMSUsers);
        }

        [KRAuth]
        public ActionResult Edit(int id)
        {
            int uid;
            int.TryParse((string)Url.RequestContext.RouteData.Values["id"], out uid);

            User m_User = UserRepository.RetrieveOne(uid);

            return View("Edit", m_User);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult Edit(User m_User, string oldUserName)
        {
            if (ModelState.IsValid)
            {
                UserRepository.Update(m_User, oldUserName);

                List<User> CMSUsers = UserRepository.RetrieveAll();
                return View("Manage", CMSUsers);
            }
            else
            {
                return View("Edit", m_User);
            }
        }
    }
}

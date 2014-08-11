using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KR.Domain.Abstract;
using KR.Domain.Entities;

namespace KR.WebUI.Controllers
{
    public class StaffController : Controller
    {
        IStaffRepository StaffRepository;
        ICompanyRepository CompanyRepository;

        public StaffController(IStaffRepository StaffRepo, ICompanyRepository CompanyRepo)
        {
            StaffRepository = StaffRepo;
            CompanyRepository = CompanyRepo;
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddStaff()
        {
            Staff m_Staff = new Staff();
            ViewBag.Companies = CompanyRepository.RetrieveAll();

            return View("AddStaff", m_Staff);
        }

        [HttpPost]
        public ActionResult AddStaff(Staff m_Staff)
        {
            if (ModelState.IsValid)
            {
                StaffRepository.AddStaff(m_Staff);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Companies = CompanyRepository.RetrieveAll();
                return View("AddStaff", m_Staff);
            }
        }

        [HttpGet]
        public ActionResult EditStaff(int id)
        {
            Staff m_Staff = StaffRepository.RetrieveOne(id);
            ViewBag.Companies = CompanyRepository.RetrieveAll();

            return View("EditStaff", m_Staff);
        }

        [HttpPost]
        public ActionResult EditStaff(Staff m_Staff)
        {
            if (ModelState.IsValid)
            {
                StaffRepository.EditStaff(m_Staff);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Companies = CompanyRepository.RetrieveAll();
                return View("EditStaff", m_Staff);
            }
        }

    }
}

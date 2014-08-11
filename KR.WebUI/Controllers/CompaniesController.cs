using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KR.Domain.Abstract;
using KR.Domain.Entities;

namespace KR.WebUI.Controllers
{
    public class CompaniesController : Controller
    {
        ICompanyRepository CompanyRepository;

        public CompaniesController(ICompanyRepository CompanyRepo)
        {
            CompanyRepository = CompanyRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddCompany()
        {
            Companies m_Company = new Companies();
            ViewBag.CompanyTypes = CompanyRepository.GetCompanyTypes();
            return View("AddCompany", m_Company);
        }

        [HttpPost]
        public ActionResult AddCompany(Companies m_Company)
        {
            if (ModelState.IsValid)
            {
                CompanyRepository.AddCompany(m_Company);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CompanyTypes = CompanyRepository.GetCompanyTypes();
                return View("AddCompany", m_Company);
            }
        }

        [HttpGet]
        public ActionResult EditCompany(int id)
        {
            Companies m_Company = CompanyRepository.RetrieveOne(id);
            ViewBag.CompanyTypes = CompanyRepository.GetCompanyTypes();
            return View("EditCompany", m_Company);
        }

        [HttpPost]
        public ActionResult EditCompany(Companies m_Company)
        {
            if(ModelState.IsValid)
            {
                CompanyRepository.UpdateCompany(m_Company);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CompanyTypes = CompanyRepository.GetCompanyTypes();
                return View("EditCompany", m_Company);
            }
        }

        [HttpGet]
        public ActionResult SearchCompany(int pageNum, string filter)
        {
            List<Companies> m_Companies = CompanyRepository.Pagination(pageNum, filter);
            int numCompanies = CompanyRepository.GetNumCompanies(filter);
            int numPages = numCompanies / 50;
            
            if (numPages % 50 > 0)
            {
                numPages++;
            }
            if (pageNum > 16)
            {
                ViewBag.PaginationStart = pageNum - 15;
                ViewBag.PaginationEnd = pageNum + 15;
            }
            else
            {
                ViewBag.PaginationStart = 1;
                ViewBag.PaginationEnd = 31;
            }
            if (ViewBag.PaginationEnd > numPages)
            {
                ViewBag.PaginationEnd = numPages;
            }

            ViewBag.NumPages = numPages;
            ViewBag.PageNumber = pageNum;
            ViewBag.Filter = filter;

            return View("SearchCompany", m_Companies);
        }

        [HttpPost]
        public ActionResult FilterCompanies(int pageNum, string filter)
        {
            List<Companies> m_Companies = CompanyRepository.Pagination(pageNum, filter);

            int numCompanies = CompanyRepository.GetNumCompanies(filter);
            int numPages = numCompanies / 50;

            if (numPages % 50 > 0)
            {
                numPages++;
            }
            if (pageNum > 16)
            {
                ViewBag.PaginationStart = pageNum - 15;
                ViewBag.PaginationEnd = pageNum + 15;
            }
            else
            {
                ViewBag.PaginationStart = 1;
                ViewBag.PaginationEnd = 31;
            }
            if (ViewBag.PaginationEnd > numPages)
            {
                ViewBag.PaginationEnd = numPages;
            }

            ViewBag.NumPages = numPages;
            ViewBag.PageNumber = pageNum;
            ViewBag.Filter = filter;

            return View("FilterCompanies", m_Companies);
        }
    }
}

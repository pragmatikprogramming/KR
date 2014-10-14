using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KR.Domain.Abstract;
using KR.Domain.Entities;
using KR.WebUI.Infrastructure;

namespace KR.WebUI.Controllers
{
    public class CompaniesController : Controller
    {
        ICompanyRepository CompanyRepository;
        IStaffRepository StaffRepository;
        ICandidateRepository CandidateRepository;
        IJobOrderRepository JobOrderRepository;
        INoteRepository NoteRepository;

        public CompaniesController(ICompanyRepository CompanyRepo, IStaffRepository StaffRepo, ICandidateRepository CandidateRepo, IJobOrderRepository JobOrderRepo, INoteRepository NoteRepo)
        {
            CompanyRepository = CompanyRepo;
            StaffRepository = StaffRepo;
            CandidateRepository = CandidateRepo;
            JobOrderRepository = JobOrderRepo;
            NoteRepository = NoteRepo;
            ViewBag.Name = System.Web.HttpContext.Current.Session["Name"];
        }

        [KRAuth]
        public ActionResult Index()
        {
            return View();
        }

        [KRAuth]
        [HttpGet]
        public ActionResult AddCompany()
        {
            Companies m_Company = new Companies();
            ViewBag.CompanyTypes = CompanyRepository.GetCompanyTypes();
            return View("AddCompany", m_Company);
        }

        [KRAuth]
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

        [KRAuth]
        [HttpGet]
        public ActionResult EditCompany(int id)
        {
            Companies m_Company = CompanyRepository.RetrieveOne(id);
            ViewBag.CompanyTypes = CompanyRepository.GetCompanyTypes();
            return View("EditCompany", m_Company);
        }

        [KRAuth]
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

        [KRAuth]
        [HttpGet]
        public ActionResult SearchCompany(int pageNum, int mode, string filter)
        {
            List<Companies> m_Companies = CompanyRepository.Pagination(pageNum, filter, mode);
            int numCompanies = CompanyRepository.GetNumCompanies(filter, mode);
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
            if (mode == 0)
            {
                ViewBag.Filter = filter;
            }
            else
            {
                ViewBag.DescriptionFilter = filter;
            }
            ViewBag.Mode = mode;

            return View("SearchCompany", m_Companies);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult DescriptionFilter(int pageNum, int mode, string filter)
        {
            List<Companies> m_Companies = CompanyRepository.Pagination(pageNum, filter, 1);

            int numCompanies = CompanyRepository.GetNumCompanies(filter, 1);
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
            ViewBag.DescriptionFilter = filter;

            return View("DescriptionFilter", m_Companies);
        }
        
        [KRAuth]
        [HttpPost]
        public ActionResult FilterCompanies(int pageNum, int mode, string filter)
        {
            List<Companies> m_Companies = CompanyRepository.Pagination(pageNum, filter, 0);

            int numCompanies = CompanyRepository.GetNumCompanies(filter, 0);
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

        [KRAuth]
        [HttpGet]
        public ActionResult DisplayCompany(int id)
        {
            Companies m_Company = CompanyRepository.RetrieveOne(id);
            ViewBag.Staff = StaffRepository.GetStaffByCompanyId(id);
            ViewBag.Candidates = CandidateRepository.GetCandidatesByCompanyId(id);
            ViewBag.Jobs = JobOrderRepository.GetJobsByCompanyId(id);
            ViewBag.Notes = NoteRepository.RetrieveAll(id, "Company");

            return View("DisplayCompany", m_Company);
        }
    }
}

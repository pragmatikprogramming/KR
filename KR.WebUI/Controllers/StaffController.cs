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
        INoteRepository NoteRepository;
        ICandidateRepository CandidateRepository;

        public StaffController(IStaffRepository StaffRepo, ICompanyRepository CompanyRepo, INoteRepository NoteRepo, ICandidateRepository CandidateRepo)
        {
            StaffRepository = StaffRepo;
            CompanyRepository = CompanyRepo;
            NoteRepository = NoteRepo;
            CandidateRepository = CandidateRepo;
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddStaff(int id = 0)
        {
            Staff m_Staff = new Staff();
            ViewBag.Companies = CompanyRepository.RetrieveAll();
            ViewBag.CompanyId = id;

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

        [HttpGet]
        public ActionResult DisplayStaff(int id)
        {
            Staff m_Staff = StaffRepository.RetrieveOne(id);
            ViewBag.Company = CompanyRepository.RetrieveOne(m_Staff.CompanyId);
            ViewBag.Notes = NoteRepository.RetrieveAll(id, "Staff");

            return View("DisplayStaff", m_Staff);
        }

        [HttpGet]
        public ActionResult MakeCand(int id)
        {
            Staff m_Staff = StaffRepository.RetrieveOne(id);
            Candidate m_Candidate = new Candidate();
            m_Candidate.FirstName = m_Staff.FirstName;
            m_Candidate.LastName = m_Staff.LastName;
            m_Candidate.CompanyId = m_Staff.CompanyId;
            m_Candidate.Email = m_Staff.Email;
            m_Candidate.Date = m_Staff.DateEntered;

            CandidateRepository.AddCandidate(m_Candidate, null);
            StaffRepository.DeleteStaff(id);

            return Redirect("/Companies/DisplayCompany/" + m_Staff.CompanyId);
        }

        [HttpGet]

        public ActionResult DeleteStaff( int id, int pageNum, int mode, string filter)
        {
            StaffRepository.DeleteStaff(id);

            return Redirect("/Staff/SearchStaff/" + pageNum + "/" + mode + "/" + filter);
        }

        [HttpGet]
        public ActionResult SearchStaff(int pageNum, int mode, string filter)
        {
            List<Staff> m_Staff = StaffRepository.Pagination(pageNum, filter, mode);
            int numStaff = StaffRepository.GetNumStaff(filter, mode);
            int numPages = numStaff / 50;

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

            return View("SearchStaff", m_Staff);
        }

        [HttpPost]
        public ActionResult DescriptionFilter(int pageNum, int mode, string filter)
        {
            List<Staff> m_Staff = StaffRepository.Pagination(pageNum, filter, mode);
            int numStaff = StaffRepository.GetNumStaff(filter, mode);
            int numPages = numStaff / 50;

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

            return View("DescriptionFilter", m_Staff);
        }

        [HttpPost]
        public ActionResult FilterStaff(int pageNum, int mode, string filter)
        {
            List<Staff> m_Staff = StaffRepository.Pagination(pageNum, filter, mode);
            int numStaff = StaffRepository.GetNumStaff(filter, mode);
            int numPages = numStaff / 50;

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

            return View("FilterStaff", m_Staff);
        }

    }
}

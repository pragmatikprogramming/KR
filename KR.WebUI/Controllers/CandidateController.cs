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
    public class CandidateController : Controller
    {
        ICandidateRepository CandidateRepository;
        ICompanyRepository CompanyRepository;
        IOfferRepository OfferRepository;
        IJobOrderRepository JobOrderRepository;
        IPlacementsRepository PlacementRepository;

        public CandidateController(ICandidateRepository CandidateRepo, ICompanyRepository CompanyRepo, IOfferRepository OfferRepo, IJobOrderRepository JobOrderRepo, IPlacementsRepository PlacementRepo)
        {
            CandidateRepository = CandidateRepo;
            CompanyRepository = CompanyRepo;
            OfferRepository = OfferRepo;
            JobOrderRepository = JobOrderRepo;
            PlacementRepository = PlacementRepo;
            ViewBag.Name = System.Web.HttpContext.Current.Session["Name"];
        }

        [KRAuth]
        public ActionResult Index()
        {
            return View();
        }

        [KRAuth]
        [HttpGet]
        public ActionResult AddCandidate(int id = 0)
        {
            Candidate m_Cand = new Candidate();
            ViewBag.Companies = CompanyRepository.RetrieveAll();
            ViewBag.CompanyId = id;
            return View("AddCandidate", m_Cand);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult AddCandidate(Candidate m_Cand, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                int m_Id = CandidateRepository.AddCandidate(m_Cand, fileUpload);
                return RedirectToAction("/DisplayCandidate/" + m_Id);
            }
            else
            {
                ViewBag.Companies = CompanyRepository.RetrieveAll();
                return View("AddCandidate", m_Cand);
            }
        }

        [KRAuth]
        [HttpGet]
        public ActionResult EditCandidate(int id)
        {
            Candidate m_Cand = CandidateRepository.RetrieveOne(id);
            ViewBag.Companies = CompanyRepository.RetrieveAll();
            return View("EditCandidate", m_Cand);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult EditCandidate(Candidate m_Cand)
        {
            if (ModelState.IsValid)
            {
                CandidateRepository.UpdateCandidate(m_Cand);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Companies = CompanyRepository.RetrieveAll();
                return View("EditCandidate", m_Cand);
            }

        }

        [KRAuth]
        [HttpGet]
        public ActionResult DeleteCandidate(int id)
        {
            CandidateRepository.DeleteCandidate(id);

            return Redirect(System.Web.HttpContext.Current.Request.UrlReferrer.ToString());
        }

        [KRAuth]
        [HttpGet]
        public ActionResult SearchCandidate(int pageNum, int mode, string filter)
        {
            List<Candidate> m_Candidates = CandidateRepository.Pagination(pageNum, mode, filter);
            int numCandidates = CandidateRepository.GetNumCandidates(filter, mode);

            int numPages = numCandidates / 50;

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

            return View("SearchCandidate", m_Candidates);
        }

        [KRAuth]
        public ActionResult ResumeFilter(int pageNum, int mode, string filter)
        {
            List<Candidate> m_Candidates = CandidateRepository.Pagination(pageNum, mode, filter);
            int numCandidates = CandidateRepository.GetNumCandidates(filter, mode);
            int numPages = numCandidates / 50;

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

            return View("ResumeFilter", m_Candidates);
        }

        [KRAuth]
        public ActionResult NameFilter(int pageNum, int mode, string filter)
        {
            List<Candidate> m_Candidates = CandidateRepository.Pagination(pageNum, mode, filter);
            int numCandidates = CandidateRepository.GetNumCandidates(filter, mode);

            int numPages = numCandidates / 50;

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

            return View("NameFilter", m_Candidates);
        }

        [KRAuth]
        [HttpGet]
        public ActionResult SendResume(int id)
        {
            ViewBag.JobOrders = JobOrderRepository.RetrieveAll();
            Candidate m_Cand = CandidateRepository.RetrieveOne(id);

            return View("SendResume", m_Cand);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult SendResume(int Id, int JobOrderId, string EmailText)
        {
            CandidateRepository.SendResume(Id, JobOrderId, EmailText);

            return RedirectToAction("/DisplayCandidate/" + Id);
        }

        [KRAuth]
        public ActionResult DisplayCandidate(int id)
        {
            Candidate m_Cand = CandidateRepository.RetrieveOne(id);
            ViewBag.Company = CompanyRepository.RetrieveOne(m_Cand.CompanyId);
            ViewBag.Resumes = CandidateRepository.GetResumesSent(id);
            ViewBag.Interviews = CandidateRepository.GetInterviews(id);
            ViewBag.Notes = CandidateRepository.GetNotes(id);
            ViewBag.Offers = OfferRepository.RetrieveAllCand(id);

            return View("DisplayCandidate", m_Cand);
        }

        [KRAuth]
        [HttpGet]
        public ActionResult UploadResume(int id)
        {
            ViewBag.CandId = id;

            return View("UploadResume");
        }

        [KRAuth]
        [HttpPost]
        public ActionResult UploadResume(int id, HttpPostedFileBase candResume)
        {
            if (candResume != null && candResume.ContentLength > 0)
            {
                string m_FileName = candResume.FileName;
                string[] splitFile = m_FileName.Split('.');
                string fileType = splitFile[1];

                if (fileType != "doc" && fileType != "docx")
                {
                    ViewBag.CandId = id;
                    return View("UploadResume");
                }
                else
                {
                    //var content = new byte[candResume.ContentLength];
                    //candResume.InputStream.Read(content, 0, candResume.ContentLength);

                    CandidateRepository.UploadResume(id, CandidateRepository.FileToBinary(candResume), fileType, CandidateRepository.FileToText(candResume, fileType));

                    return RedirectToAction("/DisplayCandidate/" + id);
                }    
            }
            else
            {
                ViewBag.CandId = id;

                return View("UploadResume");
            }
        }

        [KRAuth]
        [HttpGet]
        public FileResult ViewResume(int id)
        {
            byte[] m_Barray = new byte[0];
            Candidate m_Cand = CandidateRepository.RetrieveOne(id);
            FileContentResult m_Resume;

            if (m_Cand.FileType == "docx")
            {
                m_Resume = new FileContentResult(CandidateRepository.ViewResume(id), "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }
            else if (m_Cand.FileType == "doc")
            {
                m_Resume = new FileContentResult(CandidateRepository.ViewResume(id), "application/msword");
            }
            else if (m_Cand.FileType == "pdf")
            {
                m_Resume = new FileContentResult(CandidateRepository.ViewResume(id), "application/pdf");
            }
            else
            {
                m_Resume = new FileContentResult(m_Barray, "application/msword");
            }

            m_Resume.FileDownloadName = m_Cand.LastName + ", " + m_Cand.FirstName + "." + m_Cand.FileType;
            return m_Resume;
            //return new FileContentResult(CandidateRepository.ViewResume(id), "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }

        [KRAuth]
        [HttpGet]
        public ActionResult AddPlacement(int id)
        {
            List<Offer> m_Offers = OfferRepository.RetrieveAllCand(id);
            ViewBag.CandidateId = id;
            ViewBag.Placement = new Placement();
            return View("AddPlacement", m_Offers);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult AddPlacement(Placement m_Placement)
        {
            if (!ModelState.IsValid)
            {
                m_Placement.PaidDate = DateTime.MaxValue;
                ModelState.Remove("PaidDate");
            }

            if (ModelState.IsValid)
            {
                PlacementRepository.AddPlacement(m_Placement);
                return Redirect("/Candidate/DisplayCandidate/" + m_Placement.CandidateId);
            }
            else
            {
                List<Offer> m_Offers = OfferRepository.RetrieveAllCand(m_Placement.CandidateId);
                ViewBag.Id = m_Placement.CandidateId;
                ViewBag.Placement = m_Placement;
                return View("AddPlacement", m_Offers);
            }
        }
    }
}

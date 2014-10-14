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
    public class JobOrderController : Controller
    {
        IJobOrderRepository JobOrderRepository;
        ICompanyRepository CompanyRepository;
        IOfferRepository OfferRepository;
        INoteRepository NoteRepository;
        ICandidateRepository CandidateRepository;
        IPlacementsRepository PlacementRepository;

        public JobOrderController(IJobOrderRepository JobOrderRepo, ICompanyRepository CompanyRepo, IOfferRepository OfferRepo, INoteRepository NoteRepo, ICandidateRepository CandidateRepo, IPlacementsRepository PlacementRepo)
        {
            JobOrderRepository = JobOrderRepo;
            CompanyRepository = CompanyRepo;
            OfferRepository = OfferRepo;
            NoteRepository = NoteRepo;
            CandidateRepository = CandidateRepo;
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
        public ActionResult AddJobOrder()
        {
            ViewBag.CompanyId = new SelectList(CompanyRepository.RetrieveAll(), "Id", "Name");
            JobOrder m_JobOrder = new JobOrder();

            return View("AddJobOrder", m_JobOrder);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult AddJobOrder(JobOrder m_JobOrder, string ContactSelect)
        {
            string[] m_info = ContactSelect.Split(':');
            m_JobOrder.ContactId = int.Parse(m_info[0]);
            m_JobOrder.ContactType = m_info[1];
            
            if (ModelState.IsValid)
            {
                JobOrderRepository.AddJobOrder(m_JobOrder);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CompanyId = new SelectList(CompanyRepository.RetrieveAll(), "Id", "Name", m_JobOrder.CompanyId);
                ViewBag.Contacts = CompanyRepository.GetContacts(m_JobOrder.CompanyId);
                return View("AddJobOrder", m_JobOrder);
            }
        }

        [KRAuth]
        [HttpGet]
        public ActionResult EditJobOrder(int id)
        {
            JobOrder m_JobOrder = JobOrderRepository.RetrieveOne(id);
            ViewBag.CompanyId = new SelectList(CompanyRepository.RetrieveAll(), "Id", "Name", m_JobOrder.CompanyId);
            ViewBag.Contacts = CompanyRepository.GetContacts(m_JobOrder.CompanyId);

            return View("EditJobOrder", m_JobOrder);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult EditJobOrder(JobOrder m_JobOrder, string ContactSelect)
        {
            string[] m_info = ContactSelect.Split(':');
            m_JobOrder.ContactId = int.Parse(m_info[0]);
            m_JobOrder.ContactType = m_info[1];

            if (ModelState.IsValid)
            {
                JobOrderRepository.UpdateJobOrder(m_JobOrder);
                return Redirect("/Companies/DisplayCompany/" + m_JobOrder.CompanyId);
            }
            else
            {
                ViewBag.CompanyId = new SelectList(CompanyRepository.RetrieveAll(), "Id", "Name", m_JobOrder.CompanyId);
                ViewBag.Contacts = CompanyRepository.GetContacts(m_JobOrder.CompanyId);
                return View("EditJobOrder", m_JobOrder);
            }
            
        }

        [KRAuth]
        [HttpPost]
        public ActionResult updateContactSelect(int id)
        {
            ViewBag.Contacts = CompanyRepository.GetContacts(id);
            return View("upDateContactSelect");
        }

        [KRAuth]
        [HttpGet]
        public ActionResult DisplayJobOrder(int id)
        {
            JobOrder m_JobOrder = JobOrderRepository.RetrieveOne(id);
            ViewBag.Company = CompanyRepository.RetrieveOne(m_JobOrder.CompanyId);
            ViewBag.ResumesSent = JobOrderRepository.ResumesSent(id);
            ViewBag.Notes = JobOrderRepository.GetNotes(id);
            ViewBag.Interviews = JobOrderRepository.GetInterviews(id);
            ViewBag.Offers = OfferRepository.RetrieveAllJob(id);

            return View("DisplayJobOrder", m_JobOrder);
        }

        [KRAuth]
        [HttpGet]
        public ActionResult DeleteNote(int id)
        {
            Note m_Note = NoteRepository.RetrieveNote(id);
            NoteRepository.DeleteNote(id);

            return RedirectToAction("/DisplayJobOrder/" + m_Note.RelatedTypeId);
        }

        [KRAuth]
        [HttpGet]
        public ActionResult SendResume(int id)
        {
            ViewBag.JobId = id;
            ViewBag.Candidates = CandidateRepository.RetrieveAll();
            JobOrder m_Job = JobOrderRepository.RetrieveOne(id);

            return View("SendResume", m_Job);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult SendResume(Resume m_Resume)
        {
            m_Resume.DateSent = DateTime.Now;

            JobOrderRepository.SendResume(m_Resume);

            return RedirectToAction("/DisplayJobOrder/" + m_Resume.JobId);
        }


        [KRAuth]
        [HttpGet]
        public ActionResult Placements()
        {
            List<JobOrder> m_Jobs = JobOrderRepository.GetPlacements();
            
            return View("Placements", m_Jobs);
        }

        [KRAuth]
        [HttpGet]
        public ActionResult AddPlacement(int id)
        {
            List<Offer> m_Offers = OfferRepository.RetrieveAllJob(id);
            ViewBag.JobId = id;
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
                return Redirect("/JobOrder/DisplayJobOrder/" + m_Placement.JobId);
            }
            else
            {
                List<Offer> m_Offers = OfferRepository.RetrieveAllJob(m_Placement.JobId);
                ViewBag.JobId = m_Placement.JobId;
                return View("AddPlacement", m_Offers);
            }
        }
    }
}

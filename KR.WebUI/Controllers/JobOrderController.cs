using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KR.Domain.Abstract;
using KR.Domain.Entities;

namespace KR.WebUI.Controllers
{
    public class JobOrderController : Controller
    {
        IJobOrderRepository JobOrderRepository;
        ICompanyRepository CompanyRepository;
        IOfferRepository OfferRepository;
        INoteRepository NoteRepository;
        ICandidateRepository CandidateRepository;

        public JobOrderController(IJobOrderRepository JobOrderRepo, ICompanyRepository CompanyRepo, IOfferRepository OfferRepo, INoteRepository NoteRepo, ICandidateRepository CandidateRepo)
        {
            JobOrderRepository = JobOrderRepo;
            CompanyRepository = CompanyRepo;
            OfferRepository = OfferRepo;
            NoteRepository = NoteRepo;
            CandidateRepository = CandidateRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddJobOrder()
        {
            ViewBag.CompanyId = new SelectList(CompanyRepository.RetrieveAll(), "Id", "Name");
            JobOrder m_JobOrder = new JobOrder();

            return View("AddJobOrder", m_JobOrder);
        }

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

        [HttpGet]
        public ActionResult EditJobOrder(int id)
        {
            JobOrder m_JobOrder = JobOrderRepository.RetrieveOne(id);
            ViewBag.CompanyId = new SelectList(CompanyRepository.RetrieveAll(), "Id", "Name", m_JobOrder.CompanyId);
            ViewBag.Contacts = CompanyRepository.GetContacts(m_JobOrder.CompanyId);

            return View("EditJobOrder", m_JobOrder);
        }

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

        [HttpPost]
        public ActionResult updateContactSelect(int id)
        {
            ViewBag.Contacts = CompanyRepository.GetContacts(id);
            return View("upDateContactSelect");
        }

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

        [HttpGet]
        public ActionResult DeleteNote(int id)
        {
            Note m_Note = NoteRepository.RetrieveNote(id);
            NoteRepository.DeleteNote(id);

            return RedirectToAction("/DisplayJobOrder/" + m_Note.RelatedTypeId);
        }

        [HttpGet]
        public ActionResult SendResume(int id)
        {
            ViewBag.JobId = id;
            ViewBag.Candidates = CandidateRepository.RetrieveAll();
            JobOrder m_Job = JobOrderRepository.RetrieveOne(id);

            return View("SendResume", m_Job);
        }

        [HttpPost]
        public ActionResult SendResume(Resume m_Resume)
        {
            m_Resume.DateSent = DateTime.Now;

            JobOrderRepository.SendResume(m_Resume);

            return RedirectToAction("/DisplayJobOrder/" + m_Resume.JobId);
        }

    }
}

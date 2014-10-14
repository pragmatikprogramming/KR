using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KR.Domain.Abstract;
using KR.Domain.Entities;
using KR.WebUI.Infrastructure;

namespace KR.Controllers
{
    public class InterviewController : Controller
    {
        IInterviewRepository InterviewRepository;
        ICandidateRepository CandidateRepository;
        IJobOrderRepository JobOrderRepository;

        public InterviewController(IInterviewRepository InterviewRepo, ICandidateRepository CandidateRepo, IJobOrderRepository JobOrderRepo)
        {
            InterviewRepository = InterviewRepo;
            CandidateRepository = CandidateRepo;
            JobOrderRepository = JobOrderRepo;
            ViewBag.Name = System.Web.HttpContext.Current.Session["Name"];
        }

        [KRAuth]
        [HttpGet]
        public ActionResult AddInterview(int id)
        {
            //List<Resume> m_Resumes = CandidateRepository.GetResumesSent(id);
            ViewBag.Resumes = CandidateRepository.GetResumesSent(id);
            ViewBag.CandId = id;

            return View("AddInterview");
        }

        [KRAuth]
        [HttpPost]
        public ActionResult AddInterview(Interview m_Interview)
        {
            if (ModelState.IsValid)
            {
                InterviewRepository.AddInterview(m_Interview);

                return Redirect("/Candidate/DisplayCandidate/" + m_Interview.CandidateId);
            }
            else
            {
                List<Resume> m_Resumes = CandidateRepository.GetResumesSent(m_Interview.CandidateId);
                ViewBag.Resumes = m_Resumes;
                ViewBag.CandId = m_Interview.CandidateId;

                return View("AddInterview");
            }
 
        }

        [KRAuth]
        [HttpGet]
        public ActionResult AddInterviewJO(int id)
        {

            ViewBag.Resumes = JobOrderRepository.ResumesSent(id);
            ViewBag.JobId = id;

            return View("AddInterviewJO");
        }

        [KRAuth]
        [HttpPost]
        public ActionResult AddInterviewJO(Interview m_Interview)
        {
            if (ModelState.IsValid)
            {
                InterviewRepository.AddInterview(m_Interview);

                return Redirect("/JobOrder/DisplayJobOrder/" + m_Interview.JobId);
            }
            else
            {
                ViewBag.Resumes = JobOrderRepository.ResumesSent(m_Interview.JobId);
                ViewBag.JobId = m_Interview.JobId;

                return View("AddInterviewJO");
            }

        }

        [KRAuth]
        [HttpGet]
        public ActionResult EditInterview(int id)
        {
            Interview m_Interview = InterviewRepository.RetrieveOne(id);
            ViewBag.Resumes = CandidateRepository.GetResumesSent(m_Interview.CandidateId);

            return View("EditInterview", m_Interview);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult EditInterview(Interview m_Interview)
        {
            if (ModelState.IsValid)
            {
                InterviewRepository.EditInterview(m_Interview);

                return Redirect("/Candidate/DisplayCandidate/" + m_Interview.CandidateId);
            }
            else
            {
                Interview o_Interview = InterviewRepository.RetrieveOne(m_Interview.Id);              
                ViewBag.Resumes = CandidateRepository.GetResumesSent(m_Interview.CandidateId);

                return View("EditInterview", o_Interview);
            }
        }

        [KRAuth]
        [HttpGet]
        public ActionResult DeleteInterview(int id)
        {
            Interview m_Interview = InterviewRepository.RetrieveOne(id);
            InterviewRepository.DeleteInterview(id);
            return Redirect("/Candidate/DisplayCandidate/" + m_Interview.CandidateId);
        }

    }
}

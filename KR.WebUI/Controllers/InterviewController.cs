using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KR.Domain.Abstract;
using KR.Domain.Entities;

namespace KR.Controllers
{
    public class InterviewController : Controller
    {
        IInterviewRepository InterviewRepository;
        ICandidateRepository CandidateRepository;

        public InterviewController(IInterviewRepository InterviewRepo, ICandidateRepository CandidateRepo, IJobOrderRepository JobOrderRepo)
        {
            InterviewRepository = InterviewRepo;
            CandidateRepository = CandidateRepo;
        }

        [HttpGet]
        public ActionResult AddInterview(int id)
        {
            //List<Resume> m_Resumes = CandidateRepository.GetResumesSent(id);
            ViewBag.Resumes = CandidateRepository.GetResumesSent(id);
            ViewBag.CandId = id;

            return View("AddInterview");
        }

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

        [HttpGet]
        public ActionResult EditInterview(int id)
        {
            Interview m_Interview = InterviewRepository.RetrieveOne(id);
            ViewBag.Resumes = CandidateRepository.GetResumesSent(m_Interview.CandidateId);

            return View("EditInterview", m_Interview);
        }

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

        [HttpGet]
        public ActionResult DeleteInterview(int id)
        {
            Interview m_Interview = InterviewRepository.RetrieveOne(id);
            InterviewRepository.DeleteInterview(id);
            return Redirect("/Candidate/DisplayCandidate/" + m_Interview.CandidateId);
        }

    }
}

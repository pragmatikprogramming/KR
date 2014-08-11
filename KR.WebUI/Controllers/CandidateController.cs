using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KR.Domain.Abstract;
using KR.Domain.Entities;

namespace KR.WebUI.Controllers
{
    public class CandidateController : Controller
    {
        ICandidateRepository CandidateRepository;
        ICompanyRepository CompanyRepository;

        public CandidateController(ICandidateRepository CandidateRepo, ICompanyRepository CompanyRepo)
        {
            CandidateRepository = CandidateRepo;
            CompanyRepository = CompanyRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddCandidate()
        {
            Candidate m_Cand = new Candidate();
            ViewBag.Companies = CompanyRepository.RetrieveAll();
            return View("AddCandidate", m_Cand);
        }

        [HttpPost]
        public ActionResult AddCandidate(Candidate m_Cand, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                CandidateRepository.AddCandidate(m_Cand, fileUpload);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Companies = CompanyRepository.RetrieveAll();
                return View("AddCandidate", m_Cand);
            }
        }

        [HttpGet]
        public ActionResult EditCandidate(int id)
        {
            Candidate m_Cand = CandidateRepository.RetrieveOne(id);
            ViewBag.Companies = CompanyRepository.RetrieveAll();
            return View("EditCandidate", m_Cand);
        }

        [HttpPost]
        public ActionResult EditCandidate(Candidate m_Cand, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                CandidateRepository.UpdateCandidate(m_Cand, fileUpload);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Companies = CompanyRepository.RetrieveAll();
                return View("EditCandidate", m_Cand);
            }

        }

    }
}

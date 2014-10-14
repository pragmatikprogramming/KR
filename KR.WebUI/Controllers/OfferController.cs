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
    public class OfferController : Controller
    {
        IOfferRepository OfferRepository;
        IInterviewRepository InterviewRepository;

        public OfferController(IOfferRepository OfferRepo, IInterviewRepository InterviewRepo)
        {
            OfferRepository = OfferRepo;
            InterviewRepository = InterviewRepo;
            ViewBag.Name = System.Web.HttpContext.Current.Session["Name"];
        }

        [KRAuth]
        [HttpGet]
        public ActionResult AddOffer(int id)
        {
            ViewBag.CandId = id;
            ViewBag.Interviews = InterviewRepository.RetrieveAllCand(id);

            return View("AddOffer");
        }

        [KRAuth]
        [HttpPost]
        public ActionResult AddOffer(Offer m_Offer)
        {
            if (ModelState.IsValid)
            {
                OfferRepository.AddOffer(m_Offer);
                return Redirect("/Candidate/DisplayCandidate/" + m_Offer.CandidateId);
            }
            else
            {
                ViewBag.CandId = m_Offer.CandidateId;
                ViewBag.Interviews = InterviewRepository.RetrieveAllCand(m_Offer.CandidateId);

                return View("AddOffer");
            }
        }

        [KRAuth]
        [HttpGet]
        public ActionResult EditOffer(int id)
        {
            Offer m_Offer = OfferRepository.RetrieveOne(id);
            ViewBag.Id = id;
            ViewBag.Interviews = InterviewRepository.RetrieveAllCand(id);

            return View("EditOffer", m_Offer);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult EditOffer(Offer m_Offer)
        {
            m_Offer.StartDate = DateTime.Parse("01-01-1901");
            ModelState.Remove("StartDate");

            if (ModelState.IsValid)
            {
                OfferRepository.EditOffer(m_Offer);
                return Redirect("/Candidate/DisplayCandidate/" + m_Offer.CandidateId);
            }
            else
            {
                ViewBag.Id = m_Offer.Id;
                ViewBag.Interviews = InterviewRepository.RetrieveAllCand(m_Offer.CandidateId);
                return View("EditOffer", m_Offer);
            }

        }

        [KRAuth]
        [HttpGet]
        public ActionResult DeleteOffer(int id)
        {
            Offer m_Offer = OfferRepository.RetrieveOne(id);
            OfferRepository.DeleteOffer(id);
            return Redirect("/Candidate/DisplayCandidate/" + m_Offer.CandidateId);
        }

    }
}

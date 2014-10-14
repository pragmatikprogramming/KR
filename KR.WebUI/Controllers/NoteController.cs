using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KR.Domain.Abstract;
using KR.Domain.Models;
using KR.Domain.Entities;
using KR.WebUI.Infrastructure;

namespace KR.Controllers
{
    public class NoteController : Controller
    {
        INoteRepository NoteRepository;

        public NoteController(INoteRepository NoteRepo)
        {
            NoteRepository = NoteRepo;
        }

        [KRAuth]
        [HttpGet]
        public ActionResult AddNote(int id, string noteType)
        {
            Note m_Note = new Note();
            ViewBag.RelatedTypeId = id;
            ViewBag.NoteType = noteType;
            return View("AddNote", m_Note);
            ViewBag.Name = System.Web.HttpContext.Current.Session["Name"];
        }

        [KRAuth]
        [HttpPost]
        public ActionResult AddNote(Note m_Note)
        {
            if (ModelState.IsValid)
            {
                NoteRepository.AddNote(m_Note);

                if(m_Note.NoteType == "Company")
                {
                    return Redirect("/Companies/DisplayCompany/" + m_Note.RelatedTypeId);
                }
                else if(m_Note.NoteType == "Job")
                {
                    return Redirect("/JobOrder/DisplayJobOrder/" + m_Note.RelatedTypeId);
                }
                else if(m_Note.NoteType == "Candidate")
                {
                    return Redirect("/Candidate/DisplayCandidate/" + m_Note.RelatedTypeId);
                }
                else if (m_Note.NoteType == "Staff")
                {
                    return Redirect("/Staff/SearchStaff");
                }
                else
                {
                    return Redirect("/Home");
                }
            }
            else
            {
                ViewBag.RelatedTypeId = m_Note.RelatedTypeId;
                ViewBag.NoteType = m_Note.NoteType;
                return View("AddNote", m_Note);
            }
        }

        [KRAuth]
        [HttpGet]
        public ActionResult EditNote(int id)
        {
            Note m_Note = NoteRepository.RetrieveNote(id);

            return View("EditNote", m_Note);
        }

        [KRAuth]
        [HttpPost]
        public ActionResult EditNote(Note m_Note)
        {
            if (ModelState.IsValid)
            {
                NoteRepository.UpdateNote(m_Note);

                if (m_Note.NoteType == "Company")
                {
                    return Redirect("/Companies/DisplayCompany/" + m_Note.RelatedTypeId);
                }
                else if (m_Note.NoteType == "Job")
                {
                    return Redirect("/JobOrder/DisplayJobOrder/" + m_Note.RelatedTypeId);
                }
                else if (m_Note.NoteType == "Candidate")
                {
                    return Redirect("/Candidate/DisplayCandidate/" + m_Note.RelatedTypeId);
                }
                else if (m_Note.NoteType == "Staff")
                {
                    return Redirect("/Staff/SearchStaff");
                }
                else
                {
                    return Redirect("/Home");
                }
            }
            else
            {
                return View("EditNote", m_Note);
            }
        }
    }
}

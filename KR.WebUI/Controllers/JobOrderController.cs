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

        public JobOrderController(IJobOrderRepository JobOrderRepo, ICompanyRepository CompanyRepo)
        {
            JobOrderRepository = JobOrderRepo;
            CompanyRepository = CompanyRepo;
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

        [HttpPost]
        public ActionResult updateContactSelect(int id)
        {
            ViewBag.Contacts = CompanyRepository.GetContacts(id);
            return View("upDateContactSelect");
        }

    }
}

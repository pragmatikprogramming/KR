using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KR.Domain.Abstract;
using KR.Domain.DataAccess;
using KR.Domain.Entities;

namespace KR.Domain.Models
{
    public class OfferRepository : IOfferRepository
    {
        public void AddOffer(Offer m_Offer)
        {
            DBOffer.AddOffer(m_Offer);
        }

        public void EditOffer(Offer m_Offer)
        {
            DBOffer.EditOffer(m_Offer);
        }

        public Offer RetrieveOne(int id)
        {
            Offer m_Offer = DBOffer.RetrieveOne(id);
            return m_Offer;
        }

        public List<Offer> RetrieveAllCand(int id)
        {
            List<Offer> m_Offers = DBOffer.RetrieveAllCand(id);
            return m_Offers;
        }

        public List<Offer> RetrieveAllJob(int id)
        {
            List<Offer> m_Offers = DBOffer.RetrieveAllJob(id);
            return m_Offers;
        }

        public void DeleteOffer(int id)
        {
            DBOffer.DeleteOffer(id);
        }
    }
}
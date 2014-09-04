using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR.Domain.Entities;

namespace KR.Domain.Abstract
{
    public interface IOfferRepository
    {
        void AddOffer(Offer m_Offer);
        void EditOffer(Offer m_Offer);
        Offer RetrieveOne(int id);
        List<Offer> RetrieveAllCand(int id);
        List<Offer> RetrieveAllJob(int id);
        void DeleteOffer(int id);
    }
}

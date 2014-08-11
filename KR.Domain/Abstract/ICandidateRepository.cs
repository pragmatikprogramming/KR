using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using KR.Domain.Entities;

namespace KR.Domain.Abstract
{
    public interface ICandidateRepository
    {
        void AddCandidate(Candidate m_Cand, HttpPostedFileBase fileUpload);
        void UpdateCandidate(Candidate m_Cand, HttpPostedFileBase fuleUpload);
        void DeleteCandidate(int id);
        Candidate RetrieveOne(int id);
        List<Candidate> RetrieveAll();
    }
}

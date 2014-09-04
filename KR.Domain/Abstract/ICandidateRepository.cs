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
        int AddCandidate(Candidate m_Cand, HttpPostedFileBase fileUpload);
        void UpdateCandidate(Candidate m_Cand);
        void DeleteCandidate(int id);
        Candidate RetrieveOne(int id);
        List<Candidate> RetrieveAll();
        List<Candidate> Pagination(int pageNum, int mode, string filter);
        int GetNumCandidates(string filter, int mode);
        List<Candidate> GetCandidatesByCompanyId(int id);
        List<Resume> GetResumesSent(int id);
        List<Interview> GetInterviews(int id);
        List<Note> GetNotes(int id);
        void UploadResume(int id, byte[] resume, string fileType, string fileText);
        string FileToText(HttpPostedFileBase fileUpload, string fileType);
        byte[] FileToBinary(HttpPostedFileBase fileUpload);
        byte[] ViewResume(int id);
        void SendResume(int id, int JobOrderId, string EmailText);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using KR.Domain.Abstract;
using KR.Domain.Entities;
using KR.Domain.DataAccess;
using Bytescout.Document;

namespace KR.Domain.Models
{
    public class CandidateRepository : ICandidateRepository
    {
        public int AddCandidate(Candidate m_Cand, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                string m_FileName = fileUpload.FileName;
                string[] splitFile = m_FileName.Split('.');
                string fileType = splitFile[1];

                m_Cand.Resume = FileToText(fileUpload, fileType);
                m_Cand.ResumeBinary = FileToBinary(fileUpload);
                m_Cand.FileType = fileType;
            }
            else
            {
                m_Cand.Resume = "";
                m_Cand.ResumeBinary = new byte[0];
                m_Cand.FileType = "";
            }

          
            int m_Id = DBCandidate.AddCandidate(m_Cand);

            return m_Id;
        }

        public void UpdateCandidate(Candidate m_Cand)
        {
            DBCandidate.UpdateCandidate(m_Cand);
        }

        public void DeleteCandidate(int id)
        {
            DBCandidate.DeleteCandidate(id);
        }

        public Candidate RetrieveOne(int id)
        {
            Candidate m_Cand = DBCandidate.RetrieveOne(id);
            return m_Cand;
        }

        public List<Candidate> RetrieveAll()
        {
            List<Candidate> m_Cands = DBCandidate.RetrieveAll();
            return m_Cands;
        }

        public List<Candidate> Pagination(int pageNum, int mode, string filter)
        {
            List<Candidate> m_Candidates = DBCandidate.Pagination(pageNum, mode, filter);
            return m_Candidates;
        }

        public int GetNumCandidates(string filter, int mode)
        {
            int num_Candidates = DBCandidate.GetNumCandidates(filter, mode);
            return num_Candidates;
        }

        public List<Candidate> GetCandidatesByCompanyId(int id)
        {
            List<Candidate> m_Candidates = DBCandidate.GetCandidatesByCompanyId(id);
            return m_Candidates;
        }

        public List<Resume> GetResumesSent(int id)
        {
            List<Resume> m_ResumesSent = DBCandidate.GetResumesSent(id);
            return m_ResumesSent;
        }

        public List<Interview> GetInterviews(int id)
        {
            List<Interview> m_Interviews = DBCandidate.GetInterviews(id);
            return m_Interviews;
        }

        public List<Note> GetNotes(int id)
        {
            List<Note> m_Notes = DBCandidate.GetNotes(id);
            return m_Notes;
        }

        public string FileToText(HttpPostedFileBase fileUpload, string fileType)
        {
            string m_Text = string.Empty;

            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                string path = HttpContext.Current.Server.MapPath("/");
                fileUpload.SaveAs(path + "\\" + fileUpload.FileName);

                if (fileType == "doc" || fileType == "docx")
                {
                    Document m_Doc = new Document();
                    m_Doc.Open(path + "\\" + fileUpload.FileName);

                    for (int i = 0; i < m_Doc.ParagraphCount; i++)
                    {
                        Paragraph p = m_Doc.GetParagraph(i);
                        if (p != null)
                        {
                            m_Text += p.Text;
                        }
                    }
                }
                else if (fileType == "pdf")
                {
                }

                File.Delete(path + "\\" + fileUpload.FileName);
            }

            return m_Text;

        }

        public byte[] FileToBinary(HttpPostedFileBase fileUpload)
        {
            var content = new byte[fileUpload.ContentLength];
            fileUpload.InputStream.Read(content, 0, fileUpload.ContentLength);

            return content;
        }


        public void UploadResume(int id, byte[] resume, string fileType, string fileText)
        {
            DBCandidate.UploadResume(id, resume, fileType, fileText);
        }

        public byte[] ViewResume(int id)
        {
            byte[] m_Resume = DBCandidate.ViewResume(id);

            return m_Resume;
        }

        public void SendResume(int id, int JobOrderId, string EmailText)
        {
            DBCandidate.SendResume(id, JobOrderId);
        }
    }
}
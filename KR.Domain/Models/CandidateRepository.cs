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
        public void AddCandidate(Candidate m_Cand, HttpPostedFileBase fileUpload)
        {
            
            BinaryReader b_reader = new BinaryReader(fileUpload.InputStream);
            m_Cand.ResumeBinary = b_reader.ReadBytes(fileUpload.ContentLength);

            string path = HttpContext.Current.Server.MapPath("/uploads");
            fileUpload.SaveAs(path + "\\" + fileUpload.FileName);
            

            Document m_Doc = new Document();
            m_Doc.Open(path + "\\" + fileUpload.FileName);

            string m_Text = string.Empty;

            for (int i = 0; i < m_Doc.ParagraphCount; i++)
            {
                Paragraph p = m_Doc.GetParagraph(i);
                if (p != null)
                {
                    m_Text += p.Text;
                }
            }

            m_Cand.Resume = m_Text;
            
            DBCandidate.AddCandidate(m_Cand);

            File.Delete(path + "\\" + fileUpload.FileName);
        }

        public void UpdateCandidate(Candidate m_Cand, HttpPostedFileBase fileUpload)
        {
            DBCandidate.UpdateCandidate(m_Cand);
        }

        public void DeleteCandidate(int id)
        {

        }

        public Candidate RetrieveOne(int id)
        {
            Candidate m_Cand = DBCandidate.RetrieveOne(id);
            return m_Cand;
        }

        public List<Candidate> RetrieveAll()
        {
            List<Candidate> m_Cands = new List<Candidate>();
            return m_Cands;
        }
    }
}
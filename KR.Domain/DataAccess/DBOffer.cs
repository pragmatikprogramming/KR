using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using KR.Domain.HelperClasses;
using KR.Domain.Entities;

namespace KR.Domain.DataAccess
{
    public class DBOffer
    {
        public static void AddOffer(Offer m_Offer)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_Offers(offerDate, jobId, offer, accepted, startDate, candidateId) VALUES(@offerDate, @jobId, @offerText, @accepted, @startDate, @candidateId)";
            SqlCommand insOffer = new SqlCommand(queryString, conn);
            insOffer.Parameters.AddWithValue("offerDate", m_Offer.OfferDate);
            insOffer.Parameters.AddWithValue("jobId", m_Offer.JobId);
            insOffer.Parameters.AddWithValue("offerText", m_Offer.OfferText);
            insOffer.Parameters.AddWithValue("accepted", m_Offer.Accepted);
            insOffer.Parameters.AddWithValue("startDate", m_Offer.StartDate);
            insOffer.Parameters.AddWithValue("candidateId", m_Offer.CandidateId);

            insOffer.ExecuteNonQuery();

            conn.Close();
        }

        public static void EditOffer(Offer m_Offer)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "UPDATE CRM_Offers SET offerDate = @offerDate, jobId = @jobId, offer = @offerText, accepted = @accepted, startDate = @startDate, candidateId = @candidateId WHERE id = @id";
            SqlCommand updOffer = new SqlCommand(queryString, conn);
            updOffer.Parameters.AddWithValue("offerDate", m_Offer.OfferDate);
            updOffer.Parameters.AddWithValue("jobId", m_Offer.JobId);
            updOffer.Parameters.AddWithValue("offerText", m_Offer.OfferText);
            updOffer.Parameters.AddWithValue("accepted", m_Offer.Accepted);
            updOffer.Parameters.AddWithValue("startDate", m_Offer.StartDate);
            updOffer.Parameters.AddWithValue("candidateId", m_Offer.CandidateId);
            updOffer.Parameters.AddWithValue("id", m_Offer.Id);

            updOffer.ExecuteNonQuery();

            conn.Close();
        }

        public static Offer RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Offers WHERE id = @id";
            SqlCommand getOffer = new SqlCommand(queryString, conn);
            getOffer.Parameters.AddWithValue("id", id);
            SqlDataReader offerReader = getOffer.ExecuteReader();

            Offer m_Offer = new Offer();

            if (offerReader.Read())
            {
                m_Offer.Id = offerReader.GetInt32(0);
                m_Offer.OfferDate = offerReader.GetDateTime(1);
                m_Offer.JobId = offerReader.GetInt32(2);
                m_Offer.OfferText = offerReader.GetString(3);
                m_Offer.Accepted = offerReader.GetByte(4);
                m_Offer.StartDate = offerReader.GetDateTime(5);
                m_Offer.CandidateId = offerReader.GetInt32(6);

                Candidate m_Cand = DBCandidate.RetrieveOne(m_Offer.CandidateId);
                m_Offer.CandidateName = m_Cand.LastName + ", " + m_Cand.FirstName;

                JobOrder m_Job = DBJobOrder.RetrieveOne(m_Offer.JobId);
                m_Offer.JobName = m_Job.PositionTitle;
            }

            conn.Close();

            return m_Offer;
        }

        public static List<Offer> RetrieveAllCand(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Offers WHERE candidateId = @id ORDER BY offerDate DESC";
            SqlCommand getOffers = new SqlCommand(queryString, conn);
            getOffers.Parameters.AddWithValue("id", id);
            SqlDataReader offerReader = getOffers.ExecuteReader();

            List<Offer> m_Offers = new List<Offer>();

            while (offerReader.Read())
            {
                Offer m_Offer = new Offer();

                m_Offer.Id = offerReader.GetInt32(0);
                m_Offer.OfferDate = offerReader.GetDateTime(1);
                m_Offer.JobId = offerReader.GetInt32(2);
                m_Offer.OfferText = offerReader.GetString(3);
                m_Offer.Accepted = offerReader.GetByte(4);
                m_Offer.StartDate = offerReader.GetDateTime(5);
                m_Offer.CandidateId = offerReader.GetInt32(6);

                Candidate m_Cand = DBCandidate.RetrieveOne(m_Offer.CandidateId);
                m_Offer.CandidateName = m_Cand.LastName + ", " + m_Cand.FirstName;

                JobOrder m_Job = DBJobOrder.RetrieveOne(m_Offer.JobId);
                m_Offer.JobName = m_Job.PositionTitle;


                m_Offers.Add(m_Offer);
            }


            conn.Close();

            return m_Offers;
        }

        public static List<Offer> RetrieveAllJob(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Offers WHERE jobId = @id ORDER BY offerDate DESC";
            SqlCommand getOffers = new SqlCommand(queryString, conn);
            getOffers.Parameters.AddWithValue("id", id);
            SqlDataReader offerReader = getOffers.ExecuteReader();

            List<Offer> m_Offers = new List<Offer>();

            while (offerReader.Read())
            {
                Offer m_Offer = new Offer();

                m_Offer.Id = offerReader.GetInt32(0);
                m_Offer.OfferDate = offerReader.GetDateTime(1);
                m_Offer.JobId = offerReader.GetInt32(2);
                m_Offer.OfferText = offerReader.GetString(3);
                m_Offer.Accepted = offerReader.GetByte(4);
                m_Offer.StartDate = offerReader.GetDateTime(5);
                m_Offer.CandidateId = offerReader.GetInt32(6);

                Candidate m_Cand = DBCandidate.RetrieveOne(m_Offer.CandidateId);
                m_Offer.CandidateName = m_Cand.LastName + ", " + m_Cand.FirstName;

                JobOrder m_Job = DBJobOrder.RetrieveOne(m_Offer.JobId);
                m_Offer.JobName = m_Job.PositionTitle;


                m_Offers.Add(m_Offer);
            }


            conn.Close();

            return m_Offers;
        }

        public static void DeleteOffer(int id)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "DELETE FROM CRM_Offers WHERE id = @id";
            SqlCommand delOffer = new SqlCommand(queryString, conn);
            delOffer.Parameters.AddWithValue("id", id);
            delOffer.ExecuteNonQuery();

            conn.Close();
        }
    }
}
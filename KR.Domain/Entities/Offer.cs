using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KR.Domain.Entities
{
    public class Offer
    {
        private int id;
        private DateTime offerDate;
        private int jobId;
        private string jobName;
        private string offerText;
        private int accepted;
        private DateTime startDate;
        private int candidateId;
        private string candidateName;


        public int Id
        {
            get 
            { 
                return id; 
            }
            set 
            { 
                id = value; 
            }
        }
        public DateTime OfferDate
        {
            get 
            { 
                return offerDate; 
            }
            set 
            { 
                offerDate = value; 
            }
        }
        public int JobId
        {
            get 
            { 
                return jobId; 
            }
            set 
            { 
                jobId = value; 
            }
        }
        public string JobName
        {
            get 
            { 
                return jobName; 
            }
            set 
            { 
                jobName = value; 
            }
        }
        public string OfferText
        {
            get 
            { 
                return offerText; 
            }
            set 
            { 
                offerText = value; 
            }
        }
        public int Accepted
        {
            get 
            { 
                return accepted; 
            }
            set 
            { 
                accepted = value; 
            }
        }
        public DateTime StartDate
        {
            get 
            { 
                return startDate; 
            }
            set 
            { 
                startDate = value; 
            }
        }
        public int CandidateId
        {
            get 
            { 
                return candidateId; 
            }
            set 
            { 
                candidateId = value; 
            }
        }

        public string CandidateName
        {
            get 
            { 
                return candidateName; 
            }
            set 
            { 
                candidateName = value; 
            }
        }
    }
}
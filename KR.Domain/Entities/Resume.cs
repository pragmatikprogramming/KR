using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KR.Domain.Entities
{
    public class Resume
    {
        private int id;
        private int candidateId;
        private string candidateName;
        private int jobId;
        private string jobName;
        private DateTime dateSent;

        

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

        public DateTime DateSent
        {
            get 
            { 
                return dateSent; 
            }
            set 
            { 
                dateSent = value; 
            }
        }
    }
}
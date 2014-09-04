using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KR.Domain.Entities
{
    public class Interview
    {
        private int id;        
        private DateTime interviewDate;     
        private int jobId;
        private string jobName;
        private int candidateId;       
        private string candidateName;
        private string interviewType;


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

        [Required(ErrorMessage = "Please enter a date")]
        public DateTime InterviewDate
        {
            get 
            { 
                return interviewDate; 
            }
            set 
            { 
                interviewDate = value; 
            }
        }

        [Required(ErrorMessage = "Please select a job")]
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

        [Required(ErrorMessage = "Something went terribly wrong please report this to your administrator")]
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

        [Required(ErrorMessage = "Please select an interview Type")]
        public string InterviewType
        {
            get 
            { 
                return interviewType; 
            }
            set 
            { 
                interviewType = value; 
            }
        }
    }
}
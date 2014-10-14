using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KR.Domain.Entities
{
    public class Placement
    {
        private int id;
        private int jobId;
        private int candidateId;
        private DateTime startDate;
        private DateTime invoiceDate;
        private DateTime paidDate;       
        private int salary;



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

        [Required(ErrorMessage = "Please select an Offer to add a placement to")]
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

        [Required(ErrorMessage = "Please select a Candidate to add a placement to")]
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

        [Required(ErrorMessage = "Please select a Start Date")]
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

        public DateTime InvoiceDate
        {
            get 
            { 
                return invoiceDate; 
            }
            set 
            { 
                invoiceDate = value; 
            }
        }

        public DateTime PaidDate
        {
            get 
            { 
                return paidDate; 
            }
            set 
            { 
                paidDate = value; 
            }
        }

        [Required(ErrorMessage = "Please enter a salary for the placement")]
        public int Salary
        {
            get 
            { 
                return salary; 
            }
            set 
            { 
                salary = value; 
            }
        }
    }
}
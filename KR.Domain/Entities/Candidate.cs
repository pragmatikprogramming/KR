using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KR.Domain.Entities
{
    public class Candidate
    {
        private int id;
        private DateTime date;
        private string lastName;
        private string firstName;
        private string email;
        private string resume;
        private int noncompete;
        private int optOut;
        private DateTime resumeLastUpdate;
        private byte[] resumeBinary;
        private int companyId;
        private string fileType;



        

        

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

        public DateTime Date
        {
            get 
            { 
                return date; 
            }
            set 
            { 
                date = value; 
            }
        }

        public string LastName
        {
            get 
            { 
                return lastName; 
            }
            set 
            { 
                lastName = value; 
            }
        }

        public string FirstName
        {
            get 
            { 
                return firstName; 
            }
            set 
            { 
                firstName = value; 
            }
        }

        public string Email
        {
            get 
            { 
                return email; 
            }
            set 
            { 
                email = value; 
            }
        }

        public string Resume
        {
            get 
            { 
                return resume; 
            }
            set 
            { 
                resume = value; 
            }
        }

        public int Noncompete
        {
            get 
            { 
                return noncompete; 
            }
            set 
            { 
                noncompete = value; 
            }
        }

        public int OptOut
        {
            get 
            { 
                return optOut; 
            }
            set 
            { 
                optOut = value; 
            }
        }

        public DateTime ResumeLastUpdate
        {
            get 
            { 
                return resumeLastUpdate; 
            }
            set 
            { 
                resumeLastUpdate = value; 
            }
        }

        public Byte[] ResumeBinary
        {
            get 
            { 
                return resumeBinary; 
            }
            set 
            { 
                resumeBinary = value; 
            }
        }
        public int CompanyId
        {
            get 
            { 
                return companyId; 
            }
            set 
            { 
                companyId = value; 
            }
        }

        public string FileType
        {
            get 
            { 
                return fileType; 
            }
            set 
            { 
                fileType = value; 
            }
        }
    }
}
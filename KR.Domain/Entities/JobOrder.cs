using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KR.Domain.Entities
{
    public class JobOrder
    {
        private int id;
        private DateTime dateAquired;
        private int companyId;
        private string positionTitle;
        private int salary;
        private string education;
        private int recruitmentFee;
        private DateTime datePlaced;
        private int displayOnWebsite;
        private string keyWords;
        private string requirements;
        private string duties;
        private int contactId;
        private string contactType;

        

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

        public DateTime DateAquired
        {
            get 
            { 
                return dateAquired; 
            }
            set 
            { 
                dateAquired = value; 
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

        public string PositionTitle
        {
            get 
            { 
                return positionTitle; 
            }
            set 
            { 
                positionTitle = value; 
            }
        }

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

        public string Education
        {
            get 
            { 
                return education; 
            }
            set 
            { 
                education = value; 
            }
        }

        public int RecruitmentFee
        {
            get 
            { 
                return recruitmentFee; 
            }
            set 
            { 
                recruitmentFee = value; 
            }
        }

        public DateTime DatePlaced
        {
            get 
            { 
                return datePlaced; 
            }
            set 
            { 
                datePlaced = value; 
            }
        }

        public int DisplayOnWebsite
        {
            get 
            { 
                return displayOnWebsite; 
            }
            set 
            { 
                displayOnWebsite = value; 
            }
        }

        public string KeyWords
        {
            get 
            { 
                return keyWords; 
            }
            set 
            { 
                keyWords = value; 
            }
        }

        public string Requirements
        {
            get 
            { 
                return requirements; 
            }
            set 
            { 
                requirements = value; 
            }
        }

        public string Duties
        {
            get 
            { 
                return duties; 
            }
            set 
            { 
                duties = value; 
            }
        }

        public int ContactId
        {
            get 
            { 
                return contactId;
            }
            set 
            { 
                contactId = value; 
            }
        }

        public string ContactType
        {
            get 
            { 
                return contactType; 
            }
            set 
            { 
                contactType = value; 
            }
        }

    }
}
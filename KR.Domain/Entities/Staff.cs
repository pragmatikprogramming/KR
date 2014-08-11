using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KR.Domain.Entities
{
    public class Staff
    {
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private int companyId;
        private string title;
        private int optOut;
        private DateTime dateEntered;


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

        public string Title
        {
            get 
            { 
                return title; 
            }
            set 
            { 
                title = value; 
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

        public DateTime DateEntered
        {
            get 
            { 
                return dateEntered; 
            }
            set 
            { 
                dateEntered = value; 
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KR.Domain.Entities
{
    public class Companies
    {
        private int id;
        private string name;
        private int type;
        private string address;
        private string city;
        private string state;
        private string country;
        private string phone;
        private string phoneSecondary;
        private int contact;
        private string website;
        private string description;
        private DateTime date;
        private int crawl;
        private string zip;

        
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

        public string Name
        {
            get 
            { 
                return name; 
            }
            set 
            { 
                name = value; 
            }
        }

        public int Type
        {
            get 
            { 
                return type; 
            }
            set 
            { 
                type = value; 
            }
        }

        public string Address
        {
            get 
            { 
                return address; 
            }
            set 
            { 
                address = value; 
            }
        }

        public string City
        {
            get 
            { 
                return city; 
            }
            set 
            { 
                city = value; 
            }
        }

        public string State
        {
            get 
            { 
                return state; 
            }
            set 
            { 
                state = value; 
            }
        }

        public string Country
        {
            get 
            { 
                return country; 
            }
            set 
            { 
                country = value; 
            }
        }

        public string Phone
        {
            get 
            { 
                return phone; 
            }
            set 
            { 
                phone = value; 
            }
        }

        public string PhoneSecondary
        {
            get 
            { 
                return phoneSecondary; 
            }
            set 
            { 
                phoneSecondary = value; 
            }
        }

        public int Contact
        {
            get 
            { 
                return contact; 
            }
            set 
            { 
                contact = value; 
            }
        }

        public string Website
        {
            get 
            { 
                return website; 
            }
            set 
            { 
                website = value; 
            }
        }

        public string Description
        {
            get 
            { 
                return description; 
            }
            set 
            { 
                description = value; 
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

        public int Crawl
        {
            get 
            { 
                return crawl; 
            }
            set 
            { 
                crawl = value; 
            }
        }

        public string Zip
        {
            get 
            { 
                return zip; 
            }
            set 
            { 
                zip = value; 
            }
        }
    }
}
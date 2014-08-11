using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KR.Domain.Entities
{
    public class CompanyType
    {
        private int id;
        private string type;

        public string Type
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
    }
}
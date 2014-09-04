using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KR.Domain.Entities
{
    public class Note
    {
        private int id;
        private DateTime noteDate;
        private string noteText;
        private string noteType;
        private int relatedTypeId;


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
        public DateTime NoteDate
        {
            get 
            { 
                return noteDate; 
            }
            set 
            { 
                noteDate = value; 
            }
        }

        [Required(ErrorMessage = "Please enter note text")]
        public string NoteText
        {
            get 
            { 
                return noteText; 
            }
            set 
            { 
                noteText = value; 
            }
        }

        public string NoteType
        {
            get 
            { 
                return noteType; 
            }
            set 
            { 
                noteType = value; 
            }
        }

        public int RelatedTypeId
        {
            get 
            { 
                return relatedTypeId; 
            }
            set 
            { 
                relatedTypeId = value; 
            }
        }
    }
}
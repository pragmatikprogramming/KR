using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KR.Domain.Abstract;
using KR.Domain.DataAccess;
using KR.Domain.Entities;

namespace KR.Domain.Models
{
    public class NoteRepository : INoteRepository
    {
        public void AddNote(Note m_Note)
        {
            DBNote.AddNote(m_Note);
        }

        public Note RetrieveNote(int id)
        {
            Note m_Note = DBNote.RetrieveNote(id);

            return m_Note;
        }

        public List<Note> RetrieveAll(int typeRelatedId, string noteType)
        {
            List<Note> m_Notes = DBNote.RetrieveAll(typeRelatedId, noteType);

            return m_Notes;
        }

        public void UpdateNote(Note m_Note)
        {
            DBNote.UpdateNote(m_Note);
        }

        public void DeleteNote(int id)
        {
            DBNote.DeleteNote(id);
        }
    }
}
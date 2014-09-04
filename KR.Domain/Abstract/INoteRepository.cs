using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR.Domain.Entities;

namespace KR.Domain.Abstract
{
    public interface INoteRepository
    {
        void AddNote(Note m_Note);
        Note RetrieveNote(int id);
        List<Note> RetrieveAll(int typeRelatedId, string noteType);
        void UpdateNote(Note m_Note);
        void DeleteNote(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using KR.Domain.HelperClasses;
using KR.Domain.Entities;

namespace KR.Domain.DataAccess
{
    public class DBNote
    {
        public static void AddNote(Note m_Note)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_Notes(noteDate, noteText, noteType, typeRelatedId) VALUES(@noteDate, @noteText, @noteType, @typeRelatedId)";
            SqlCommand insNote = new SqlCommand(queryString, conn);
            insNote.Parameters.Add("noteDate", m_Note.NoteDate);
            insNote.Parameters.Add("noteText", m_Note.NoteText);
            insNote.Parameters.Add("noteType", m_Note.NoteType);
            insNote.Parameters.Add("typeRelatedId", m_Note.RelatedTypeId);

            insNote.ExecuteNonQuery();

            conn.Close();
        }

        public static Note RetrieveNote(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Notes WHERE id = @id";
            SqlCommand getNote = new SqlCommand(queryString, conn);
            getNote.Parameters.AddWithValue("id", id);

            SqlDataReader noteReader = getNote.ExecuteReader();

            Note m_Note = new Note();

            if (noteReader.Read())
            {
                m_Note.Id = noteReader.GetInt32(0);
                m_Note.NoteDate = noteReader.GetDateTime(1);
                m_Note.NoteText = noteReader.GetString(2);
                m_Note.NoteType = noteReader.GetString(3);
                m_Note.RelatedTypeId = noteReader.GetInt32(4);
            }

            conn.Close();

            return m_Note;
        }

        public static List<Note> RetrieveAll(int typeRelatedId, string noteType)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Notes WHERE typeRelatedId = @typeRelatedId AND noteType = @noteType";
            SqlCommand getNote = new SqlCommand(queryString, conn);
            getNote.Parameters.AddWithValue("typeRelatedId", typeRelatedId);
            getNote.Parameters.AddWithValue("noteType", noteType);

            SqlDataReader noteReader = getNote.ExecuteReader();

            List<Note> m_Notes = new List<Note>();

            while (noteReader.Read())
            {
                Note m_Note = new Note();

                m_Note.Id = noteReader.GetInt32(0);
                m_Note.NoteDate = noteReader.GetDateTime(1);
                m_Note.NoteText = noteReader.GetString(2);
                m_Note.NoteType = noteReader.GetString(3);
                m_Note.RelatedTypeId = noteReader.GetInt32(4);

                m_Notes.Add(m_Note);
            }

            conn.Close();

            return m_Notes;
        }

        public static void UpdateNote(Note m_Note)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "UPDATE CRM_Notes SET noteDate = @noteDate, noteText= @noteText, noteType = @noteType, typeRelatedId = @typeRelatedId WHERE id = @id";
            SqlCommand updNote = new SqlCommand(queryString, conn);
            updNote.Parameters.AddWithValue("noteDate", m_Note.NoteDate);
            updNote.Parameters.AddWithValue("noteText", m_Note.NoteText);
            updNote.Parameters.AddWithValue("noteType", m_Note.NoteType);
            updNote.Parameters.AddWithValue("typeRelatedId", m_Note.RelatedTypeId);
            updNote.Parameters.AddWithValue("id", m_Note.Id);

            updNote.ExecuteNonQuery();

            conn.Close();
        }

        public static void DeleteNote(int id)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "DELETE FROM CRM_Notes WHERE id = @id";
            SqlCommand delNote = new SqlCommand(queryString, conn);
            delNote.Parameters.AddWithValue("id", id);
            delNote.ExecuteNonQuery();

            conn.Close();
        }
    }
}
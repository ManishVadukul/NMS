using System;
using System.Threading.Tasks;
using NMS.Models;
using NMS.ViewModels;

namespace NMS.Core.IRepositories
{
    public interface INoteRepository
    {
        Task<List<Note>> GetAllNotes();        
        Task<Note> GetNoteById(int id);
        Task<Note> GetNotesById(int id);
        Task<Note> CreateNote(Note note);
        Task<Note> DeleteNote(int id);

        Task<Note> UpdateNote(Note note);

        Task<NotesVM> InsertProduct(NotesVM notesVM);
        Task<NotesVM> UpdateProduct(NotesVM notesVM);
        Task<IEnumerable<Category>> DDLNotesCategories();
    }
}
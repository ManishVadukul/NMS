using Microsoft.EntityFrameworkCore;
using NMS.Data;
using NMS.Models;
using NMS.ViewModels;

namespace NMS.Repository
{
    public class NoteRepository
    {
        private readonly ApplicationDbContext _db;

        public NoteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Note>> GetNotes()
        {
            return await _db.Notes.Include(i => i.Category).ToListAsync();
        }

        public async Task<IEnumerable<Category>> DDLNotesCategories()
        {
            var result = await (from c in _db.Categories
                                select c).ToListAsync();
            return result;
        }

        public async Task<List<Note>> GetAllNotes()
        {
            var result = await (from n in _db.Notes
                                join c in _db.Categories on n.CategoryId equals c.Id
                                select new Note
                                {
                                    Description = n.Description,
                                    CategoryId = n.CategoryId,
                                    CreatedDateTime=n.CreatedDateTime,
                                    CategoryName = c.Title
                                }).ToListAsync();
            return (result);
        }

        public async Task<Note> GetNoteById(int? id)
        {
            return await _db.Notes.Include(i=>i.Category).Where(c => c.Id == id).FirstOrDefaultAsync();
        }


        public async Task<Note> GetNotesById(int? id)
        {
            return await _db.Notes.FindAsync(id);
        }

        public async Task<Note> CreateNote(Note notes)
        {
            _db.Notes.Add(notes);
            await _db.SaveChangesAsync();
            return (notes);
        }


        public async Task<Note> UpdateNote(Note notes)
        {
            var objFromDb = await _db.Notes.FirstOrDefaultAsync(c => c.Id == notes.Id);
            if (objFromDb != null)
            {
                objFromDb.Description = notes.Description;
                objFromDb.CreatedDateTime = DateTime.Now;                
                 await _db.SaveChangesAsync();
                return (notes);
            }
            return (notes);
        }

        public async Task<Note> DeleteNote(int id)
        {
            
            var objFromDb = await _db.Notes.FindAsync(id);

            _db.Notes.Remove(objFromDb);
            await _db.SaveChangesAsync();
            return (objFromDb);


        }




        public async Task<NotesVM> InsertProduct(NotesVM notesVM)
        {
            
            _db.Notes.Add(notesVM.Note);
            await _db.SaveChangesAsync();
            return (notesVM);
        }
        public async Task<NotesVM> UpdateProduct(NotesVM notesVM)
        {
            var objFromDb = await _db.Notes.FirstOrDefaultAsync(c => c.Id == notesVM.Note.Id);
            if (objFromDb != null)
            {
                objFromDb.Description = notesVM.Note.Description;
                objFromDb.CreatedDateTime = notesVM.Note.CreatedDateTime;                
                objFromDb.CategoryId = notesVM.Note.Id;                
                await _db.SaveChangesAsync();
                return (notesVM);
            }
            return (notesVM);
        }

    }
}

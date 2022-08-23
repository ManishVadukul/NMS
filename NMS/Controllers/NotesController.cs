using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NMS.Core.IRepositories;
using NMS.Models;
//using NMS.Repository;
using NMS.ViewModels;

namespace NMS.Controllers
{
    public class NotesController : Controller
    {
        private readonly INoteRepository _notesRepo;
        protected readonly ILogger<NotesController> _logger;

        public NotesController(INoteRepository notesRepo, ILogger<NotesController> logger)
        {
            _notesRepo = notesRepo;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var allObj = await _notesRepo.GetAllNotes();
            return View(allObj);
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/notes", "GET");
            return Ok(await _notesRepo.GetAllNotes());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetNoteById(int id)
        {

            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/notes/{id}", "GET");

            var note = _notesRepo.GetNoteById(id);

            if (note == null)
            {
                return NotFound();
            }            
            return Ok(await note);
        }

        [HttpGet]
        [Route("api/[controller]/{id}/html")]
        public async Task<IActionResult> GetHtmlNoteById(int id)
        {

            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/notes/{id}", "GET");

            var note = _notesRepo.GetNoteById(id);

            if (note == null)
            {
                return NotFound();
            }            
            return Ok(await note);
        }


        //GET: Add or Edit
        //[NoDirectAccess]
        public async Task<IActionResult> Upsert(int? id)
        {
            var notesVM = new NotesVM()
            {
                Note = new Note(),
                CategoryList = new SelectList(await _notesRepo.DDLNotesCategories(), "Id", "Title")
            };

            if (id == null)
            {
                //this is for create
                return View(notesVM);
            }
            //this is for edit            
            notesVM.Note = await _notesRepo.GetNotesById(id.GetValueOrDefault());
            if (notesVM.Note == null)
            {
                return NotFound();
            }
            return View(notesVM);
        }

        //POST: Add or Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NotesVM notesVM)
        {
            //Set Dropdownlist before & after update 
            notesVM.CategoryList = new SelectList(await _notesRepo.DDLNotesCategories(), "CategoryId", "Name");
            if (ModelState.IsValid)
            {
                //Insert
                if (notesVM.Note.Id == 0)
                {
                    notesVM.Note.CreatedDateTime = DateTime.Now;
                    await _notesRepo.InsertProduct(notesVM);
                }
                //Update
                else
                {
                    await _notesRepo.UpdateProduct(notesVM);
                }
                // return json becuase submiting form using AJAX post method
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_viewAll", await _notesRepo.GetAllNotes()) });
            }
            //return View(Note);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Upsert", notesVM) });
        }


        // POST api/<NotesController>
        [Route("api/[controller]/")]
        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] Note note)
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/notes/", "POST");                        
            await _notesRepo.CreateNote(note);
            return Ok();
        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] Note updatedNote)
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/notes/{id}", "PUT");
            var existingNote = await _notesRepo.GetNotesById(id);
            if (existingNote == null)
            {
                return NotFound();
            }
            await _notesRepo.UpdateNote(updatedNote);
            return Ok();
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/notes/{id}", "DELETE");
            var objFromDb = _notesRepo.GetNotesById(id); //EF
            if (objFromDb == null)
            {
                return NotFound();
            }
            await _notesRepo.DeleteNote(id);
            //_logger.LogTrace("Added new  entity with Id {id}", id);
            return Ok();
        }
    }
}
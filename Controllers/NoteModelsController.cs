using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesWebApi;

namespace NotesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteModelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NoteModelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/NoteModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteModel>>> GetNotes()
        {
          if (_context.Notes == null)
          {
              return NotFound();
          }
            return await _context.Notes.ToListAsync();
        }

        // GET: api/NoteModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteModel>> GetNoteModel(Guid id)
        {
          if (_context.Notes == null)
          {
              return NotFound();
          }
            var noteModel = await _context.Notes.FindAsync(id);

            if (noteModel == null)
            {
                return NotFound();
            }

            return noteModel;
        }

        // PUT: api/NoteModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteModel(int id, NoteModel noteModel)
        {
            if (id != noteModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(noteModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NoteModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NoteModel>> PostNoteModel(NoteModel noteModel)
        {
          if (_context.Notes == null)
          {
              return Problem("Entity set 'AppDbContext.Notes'  is null.");
          }
            _context.Notes.Add(noteModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoteModel", new { id = noteModel.Id }, noteModel);
        }

        // DELETE: api/NoteModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteModel(Guid id)
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }
            var noteModel = await _context.Notes.FindAsync(id);
            if (noteModel == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(noteModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteModelExists(int id)
        {
            return (_context.Notes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

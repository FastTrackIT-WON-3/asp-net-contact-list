using ContactList.Data;
using ContactList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/contacts-list")]
    [ApiController]
    public class ContactListApiController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ContactListApiController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContactListEntry>))]
        public Task<List<ContactListEntry>> GetAll()
        {
            return _context.ContactListEntry.ToListAsync();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactListEntry))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            ContactListEntry entry = await _context.ContactListEntry
                .FirstOrDefaultAsync(ce => ce.Id == id);

            if (entry is null)
            {
                return NotFound();
            }

            return Ok(entry);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactListEntry entry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entry);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = entry.Id },
                    entry);
            }

            return BadRequest();
        }
    }
}

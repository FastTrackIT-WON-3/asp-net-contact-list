using ContactList.Core.Services;
using ContactList.Extensions;
using ContactList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IContactListService _contactListService;

        public ContactListApiController(IContactListService contactListService)
        {
            _contactListService = contactListService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContactListEntryViewModel>))]
        public async Task<List<ContactListEntryViewModel>> GetAll()
        {
            var contactListEntries = await _contactListService.GetAllAsync();

            var viewModels = contactListEntries.Select(c => c.ToViewModel())
                                               .ToList();

            return viewModels;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactListEntryViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _contactListService.GetByIdAsync(id);

            if (entry is null)
            {
                return NotFound();
            }

            var viewModel = entry.ToViewModel();
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactListEntryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newEntry = await _contactListService.CreateAsync(viewModel.ToContactListEntry());

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = newEntry.Id },
                    newEntry.ToViewModel());
            }

            return BadRequest();
        }
    }
}

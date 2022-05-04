using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactList.Models;
using ContactList.Filters;
using ContactList.Core.Services;
using ContactList.Extensions;
using ContactList.Core.Exceptions;

namespace ContactList.Controllers
{
    public class ContactListController : Controller
    {
        private readonly IContactListService _contactListService;

        public ContactListController(IContactListService contactListService)
        {
            _contactListService = contactListService;
        }

        // GET: ContactList
        [ServiceFilter(typeof(ExecutionMonitorFilter))]
        public async Task<IActionResult> Index()
        {
            var contactListEntries = await _contactListService.GetAllAsync();

            var viewModels = contactListEntries.Select(c => c.ToViewModel())
                                               .ToList();

            return View(viewModels);
        }

        // GET: ContactList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactListEntry = await _contactListService.GetByIdAsync(id.Value);
            if (contactListEntry == null)
            {
                return NotFound();
            }

            var viewModel = contactListEntry.ToViewModel();
            return View(viewModel);
        }

        // GET: ContactList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,ContactType,Name,DateOfBirth,Address,PhoneNumber,Email")] ContactListEntryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _contactListService.CreateAsync(viewModel.ToContactListEntry());
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: ContactList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactListEntry = await _contactListService.GetByIdAsync(id.Value);
            if (contactListEntry == null)
            {
                return NotFound();
            }

            var viewModel = contactListEntry.ToViewModel();
            return View(viewModel);
        }

        // POST: ContactList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,ContactType,Name,DateOfBirth,Address,PhoneNumber,Email")] ContactListEntryViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _contactListService.UpdateAsync(id, viewModel.ToContactListEntry());
                }
                catch (EntryDoesNotExistException)
                {
                    return NotFound();
                }
                catch (EntryUpdateErrorException)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: ContactList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactListEntry = await _contactListService.GetByIdAsync(id.Value);
            if (contactListEntry == null)
            {
                return NotFound();
            }

            var viewModel = contactListEntry.ToViewModel();
            return View(viewModel);
        }

        // POST: ContactList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contactListService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

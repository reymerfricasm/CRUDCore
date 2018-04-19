using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDCore.Models;
using System.Data.SqlClient;

namespace CRUDCore.Controllers
{
    public class CitizensController : Controller
    {
        private readonly PRGContext _context;

        public CitizensController(PRGContext context)
        {
            _context = context;
        }

        // GET: Citizens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Citizen.ToListAsync());
        }

        [Route("Citizen/{id}")]
        // GET: Citizens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen
                .SingleOrDefaultAsync(m => m.CitizenId == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }
        // GET: Citizens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citizens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitizenId,CitizenIdentification,FirstName,SecondName,LastName")] Citizen citizen)
        {
            if(CitizenExists(citizen.CitizenIdentification))
            {
                ViewBag.Message = $"The '{citizen.CitizenIdentification.Value}' is duplicate in the database.";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(citizen);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(citizen);
        }

        // GET: Citizens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen.SingleOrDefaultAsync(m => m.CitizenId == id);
            if (citizen == null)
            {
                return NotFound();
            }
            return View(citizen);
        }

        // POST: Citizens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitizenId,CitizenIdentification,FirstName,SecondName,LastName")] Citizen citizen)
        {
            if (id != citizen.CitizenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citizen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitizenExists(citizen.CitizenId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(citizen);
        }

        // GET: Citizens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen
                .SingleOrDefaultAsync(m => m.CitizenId == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // POST: Citizens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citizen = await _context.Citizen.SingleOrDefaultAsync(m => m.CitizenId == id);
            _context.Citizen.Remove(citizen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Find(string searchString)
        {

            if (searchString == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen
                .Where(m => m.CitizenIdentification == Convert.ToInt64(searchString)).ToListAsync();

            if(citizen.Count == 0)
            {
                ViewBag.Content = "No hay registros que coincidan.";
            }

            if (citizen == null)
            {
                return NotFound();
            }
            return View("Index", citizen);
        }

        private bool CitizenExists(long? id)
        {
            return _context.Citizen.Any(e => e.CitizenIdentification == id);
        }
    }
}

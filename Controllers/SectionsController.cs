﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanProject.Data;
using KanbanProject.Models;

namespace KanbanProject.Controllers
{
    public class SectionsController : Controller
    {
        private readonly KanbanProjectContext _context;

        public SectionsController(KanbanProjectContext context)
        {
            _context = context;
        }
        public IActionResult ProjectView()
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            return View(_context);
        }

        

        // GET: Sections
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            return _context.Section != null ? 
                          View(await _context.Section.ToListAsync()) :
                          Problem("Entity set 'KanbanProjectContext.Section'  is null.");
        }

        // GET: Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (id == null || _context.Section == null)
            {
                return NotFound();
            }

            var section = await _context.Section
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // GET: Sections/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Section section)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (ModelState.IsValid)
            {
                _context.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProjectView");
            }
            return View(section);
        }



        // GET: Sections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (id == null || _context.Section == null)
            {
                return NotFound();
            }

            var section = await _context.Section.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Section section)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (id != section.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.Id))
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
            return View(section);
        }

        //GET: Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (id == null || _context.Section == null)
            {
                return NotFound();
            }

            var section = await _context.Section
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (_context.Section == null)
            {
                return Problem("Entity set 'KanbanProjectContext.Section'  is null.");
            }
            var section = await _context.Section.FindAsync(id);
            if (section != null)
            {
                foreach(var assignment in _context.Assignment)
                {
                    if (assignment.SectionId == id) 
                    { 
                    _context.Assignment.Remove(assignment);
                    }
                }
                _context.Section.Remove(section);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("ProjectView");
        }

        private bool SectionExists(int id)
        {
          return (_context.Section?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

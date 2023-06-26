using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanProject.Data;
using KanbanProject.Models;
using System.Security.Cryptography;
using System.Text;

namespace KanbanProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly KanbanProjectContext _context;

        public UsersController(KanbanProjectContext context)
        {
            
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'KanbanProjectContext.User'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            //if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            ViewData["Exist"] = false;
            
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {                
            
            if  (ModelState.IsValid)
            {
                user.Password = CalculateMD5(user.Password);
                if (!_context.User.Any(x => (x.Login == user.Login && x.Password == user.Password)))
                {
                    
                    _context.Add(user);
                    

                    HttpContext.Session.SetInt32("IsLogged", 1);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ProjectView","Sections");
                }
                return View();

            }
            ViewData["exist"] = true;
            return View();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Password")] User user)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            if (_context.User == null)
            {
                return Problem("Entity set 'KanbanProjectContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string CalculateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // Konwersja na postać szesnastkową
                }

                return sb.ToString();
            }
        }
    }
}

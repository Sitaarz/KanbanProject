using KanbanProject.Data;
using KanbanProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KanbanProject.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly KanbanProjectContext _context;

        
        public HomeController(KanbanProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            ViewBag.IsValid = true;
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind("Id,Login,Password")] User user)
        {
            if(HttpContext.Session.GetInt32("IsLogged") != 1)
            {
                if (ModelState.IsValid)
                {
                    if (_context.User.Any(x => (x.Login == user.Login && x.Password == user.Password)))
                    {
                        HttpContext.Session.SetInt32("IsLogged", 1);
                        return RedirectToAction("Index");
                    }
                    
                }
                ViewBag.IsValid = false;
                return View(user);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetInt32("IsLogged", 0);
            return RedirectToAction("Index");
        }

        

    }
}
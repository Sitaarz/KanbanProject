using KanbanProject.Data;
using KanbanProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

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
            user.Password = CalculateMD5(user.Password);
            if(HttpContext.Session.GetInt32("IsLogged") != 1)
            {
                if (ModelState.IsValid)
                {
                    if (_context.User.Any(x => (x.Login == user.Login && x.Password == user.Password)))
                    {
                        HttpContext.Session.SetInt32("IsLogged", 1);
                        return RedirectToAction("ProjectView", "Sections");
                    }
                    
                }
                ViewBag.IsValid = false;
                return View(user);
            }
            return RedirectToAction("ProjectView", "Sections"); ;
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("IsLogged") == 0) return RedirectToAction("Login", "Home");
            HttpContext.Session.SetInt32("IsLogged", 0);
            return RedirectToAction("Index");
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
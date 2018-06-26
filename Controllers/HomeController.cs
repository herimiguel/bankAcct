using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BankAccts.Models;
using System.Linq;

namespace BankAccts.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;
        public HomeController(Context context){
            _context = context;
        } 
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("login")]
        public IActionResult login(){
            return View();
        }


        [HttpPost]
        [Route("register")]
        public IActionResult register(RegisterViewModel regAuth){
            if(ModelState.IsValid){
                User currentUser = _context.users.SingleOrDefault(users => users.Email == regAuth.Email);
                if(currentUser != null){
                    ModelState.AddModelError("Email", "That email aldready exists");
                    return View("Index", regAuth);
                }
                User NewUser = new User{
                    FirstName = regAuth.FirstName,
                    LastName = regAuth.LastName,
                    Email = regAuth.Email,
                    Password = regAuth.Password,
                    Balance =0.0
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                currentUser = _context.users.SingleOrDefault(user => user.Email == NewUser.Email);
                HttpContext.Session.SetInt32("currentUser", (int)currentUser.Id);
                return Redirect("/account");
            }else{
                return View("index", regAuth);
            }
        }

        [HttpPost]
        [Route("processLogin")]
        public IActionResult processLogin(LoginViewModel loginAuth){
            if(ModelState.IsValid){
            User currentUser = _context.users.SingleOrDefault( user => user.Email == loginAuth.Email);
            if(currentUser != null){
                HttpContext.Session.SetInt32("currentUser", (int)currentUser.Id);
                return Redirect("account");
            }else{
                ModelState.AddModelError("Email", "Does not match our records");
                return View("login", loginAuth);
            }
            }else{
                return View("login", loginAuth);

            }   
        }

    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BankAccts.Models;
using System.Linq;

namespace BankAccts.Controllers{
    public class AccountController : Controller{
        private Context _context;
        public AccountController(Context context){
            _context = context;
        }
    [HttpGet]
    [Route("account")]
    public IActionResult Index(){
        if(HttpContext.Session.GetInt32("currentUser")== null){
            return Redirect("login");
        }else{
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            User getCurrentUser = _context.users.SingleOrDefault(user => user.Id == currentUserId);
            ViewBag.CurrentUser = getCurrentUser;
            List<Transaction> transactions = _context.transactions.Where(transaction => transaction.users_id == currentUserId).OrderByDescending(transaction => transaction.XDate).ToList();
            ViewBag.Transactions = transactions;

            return View();
        }        
    }
    [HttpGet]
    [Route("logout")]
    public IActionResult logout(){
        HttpContext.Session.Clear();
        return Redirect("/login");
    }
    [HttpPost]
    [Route("addTrans")]
    public IActionResult addTrans(BankViewModel transaction){
        if(ModelState.IsValid){
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            User getCurrentUser = _context.users.SingleOrDefault(user => user.Id == currentUserId);
            ViewBag.CurrentUser = getCurrentUser;
            DateTime date = DateTime.Now;
            if(transaction.Amount >=1){
                getCurrentUser.Balance += transaction.Amount;
                Transaction newTrans = new Transaction {
                    Amount = transaction.Amount,
                    XDate = date,
                    users_id = (int)currentUserId
                };
                _context.transactions.Add(newTrans);
                _context.SaveChanges();
                return Redirect("/account");
            }else{
                if(getCurrentUser.Balance < Math.Abs(transaction.Amount)){
                    ModelState.AddModelError("Amount", "Insufficient funds");
                    List<Transaction> transactions = _context.transactions.Where(trans => trans.users_id == currentUserId).OrderByDescending(trans => trans.XDate).ToList();
                    ViewBag.Transactions = transactions;
                    return View("Index", transaction);
                }else{
                    getCurrentUser.Balance += transaction.Amount;
                    Transaction newTrans = new Transaction{
                        Amount = transaction.Amount,
                        XDate = date,
                        users_id = (int)currentUserId
                    };
                    _context.transactions.Add(newTrans);
                    _context.SaveChanges();
                }
            }
            return Redirect("/account");
        }else{
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            User getCurrentUser = _context.users.SingleOrDefault(user => user.Id == currentUserId);
            ViewBag.CurrentUser = getCurrentUser;
            return View("Index", transaction);
        }
        }
    }
}
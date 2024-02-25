using AccountDeletion.Models;
using AccountDeletion.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AccountDeletion.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public HomeController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<IActionResult> Index(string appid, string message)
        {
            var app = await _accountRepository.IsIdExist(appid);

            if (app == null)
            {
                return NotFound();
            }
            var appConfig = await _accountRepository.GetAppConfig(app.Id);
            ViewBag.SuccessMessage = message;
            return View(appConfig);
        }

        public async Task<IActionResult> SubmitForm(string appid, string data)
        {
          
            await _accountRepository.SaveUserDetails(appid, data);
  
            // get app id
            var appInfo = await _accountRepository.GetAppById(appid);
            var successMessage = "Your account deletion request received successfully";
            return RedirectToAction("Index", new { appid = appInfo.AppId, message = successMessage });
        }

        [HttpPost]
        public IActionResult LogIn(string username, string password)
        {
            //var successMessage = "";


            int ans = _accountRepository.LoginUser(username, password);
            if (ans<0)
            {
                //successMessage = "LoginFaild";
                return RedirectToAction("Login", "Home");
            }
            return RedirectToAction("Index", "Dashboard");

        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    //url:http://localhost:5155/?appid=123456789
}

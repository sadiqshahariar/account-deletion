using AccountDeletion.Models;
using AccountDeletion.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace AccountDeletion.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public DashboardController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult app_config()
        {
            List<AppConfig> appconfig= _accountRepository.GetData();

            return View(appconfig);
        }
        public IActionResult AddConfing()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfing(int id)
        {
            //int ans = id;
            bool deleteconfig = _accountRepository.DeleteConfingData(id);
            return RedirectToAction("app_config", "Dashboard");
        }
        
        public IActionResult UpdateConfing(int id)
        {
            return View(id);
        }
        [HttpPost]
        public async Task<IActionResult> AddConfingData(string appid, string bannercolor, string formcolor, string logo, string inputlevel, string phonenumber, string buttoncolor)
        {
            //var successMessage = "this from not add fully all data";
            try
            {

                int ans = await _accountRepository.SaveConfing(appid, bannercolor, formcolor, logo, inputlevel, phonenumber, buttoncolor);
                // successMessage = "User Details Add Successfully.";

            }
            catch (Exception ex) { }

            return RedirectToAction("app_config","Dashboard");

        }

        public async Task<IActionResult> UpdateConfingData(int id, string bannercolor, string formcolor, string logo, string inputlevel, string phonenumber, string buttoncolor)
        {

            string successMessage;
            try
            {
                // Find the user by userId
                bool isExistid = await _accountRepository.Updateconfingdata(id, bannercolor, formcolor, logo, inputlevel, phonenumber, buttoncolor);


                if (isExistid)
                {
                    successMessage = "User details updated successfully.";
                }
            }
            catch(Exception ex) { }

            return RedirectToAction("app_config", "Dashboard");
        }
    }
}

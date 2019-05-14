using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Lab4.Models;
using Lab4.ViewModels;
using Lab4.Services;
using Lab4.Filters;


namespace Lab4.Controllers
{
    [CatchExceptionFilter]
    public class HomeController : Controller
    {
        //private IMemoryCache _cache;
        public HomeController(IMemoryCache memoryCache) {
            //_cache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            HomeViewModel homeViewModel = TakeLast.GetHomeViewModel();
            return View(homeViewModel);
        }

        public IActionResult ToError()
        {
            return View("~/Views/Home/About.cshtml");
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult About2()
        {

            HomeViewModel homeViewModel = TakeLast.GetHomeViewModel();
            return View("~/Views/Home/About.cshtml", homeViewModel);
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Car()
        {
            if (HttpContext.Session.Keys.Contains("model"))
            {
                ViewBag.Model = HttpContext.Session.GetString("model");
            }
            if (HttpContext.Session.Keys.Contains("power"))
            {
                ViewBag.Power = HttpContext.Session.GetString("power");
            }
            if (HttpContext.Session.Keys.Contains("colour"))
            {
                ViewBag.Colour = HttpContext.Session.GetString("colour");
            }
            if (HttpContext.Session.Keys.Contains("stateNumber"))
            {
                ViewBag.StateNumber = HttpContext.Session.GetString("stateNumber");
            }
            if (HttpContext.Session.Keys.Contains("yearOfIssue"))
            {
                ViewBag.YearOfIssue = HttpContext.Session.GetString("yearOfIssue");
            }
            if (HttpContext.Session.Keys.Contains("bodyNumber"))
            {
                ViewBag.BodyNumber = HttpContext.Session.GetString("bodyNumber");
            }
            if (HttpContext.Session.Keys.Contains("engineNumber"))
            {
                ViewBag.EngineNumber = HttpContext.Session.GetString("engineNumber");
            }
            return View("~/Views/Car/Index.cshtml");
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Order()
        {
            return View("~/Views/Order/Index.cshtml");
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Owner()
        {
            return View("~/Views/Owner/Index.cshtml");
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Worker()
        {
            return View("~/Views/Worker/Index.cshtml");
        }

        [HttpPost]
        public string Car(string Model, string StateNumber,
            string Colour, int Power)
        {
            HttpContext.Session.SetString("model", Model);
            HttpContext.Session.SetString("stateNumber", StateNumber);
            HttpContext.Session.SetString("colour", Colour);
            HttpContext.Session.SetString("power", Power.ToString());
            return "Автомобиль " + Model + " с гос. номером " + StateNumber + "\n" +
                "добавлен в базу. Цвет: " + Colour + ", мощность: " + Power;
        }

        [HttpPost]
        public string Worker(string fioworker, decimal salary,
            DateTime dateOfEmployment, DateTime dateOfDismissal)
        {
            return "Рабочий " + fioworker + " с зарплатой " + salary
               + " успешно зарегистрирован. Дата принятия " + dateOfEmployment;
        }
       
       

        [HttpPost]
        public string Order(int OrderID, string DateReceipt)
        {
            return "Заказ " + OrderID + " на дату " + DateReceipt + " добавлен";
        }
       
       [HttpPost]
        public string Owner(string fioowner, string adress, int phone)
        {
            return "Владелец: " + fioowner + ". Адресс: " + adress + ". Телефон: " + phone;
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
    }
}

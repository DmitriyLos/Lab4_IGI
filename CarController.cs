using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using Lab4.ViewModels;
using Lab4.Filters;
using Newtonsoft.Json;

namespace Lab4.Controllers
{
    [CatchExceptionFilter]
    public class CarController : Controller
    {
        private int pageSize = 15;
        private STOContext db;
        private Car _car = new Car
        {
            Model = "",
            Colour = "",
            StateNumber = "",
        };

        public CarController(STOContext CarContext)
        {
            db = CarContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder)
        {
            Car sessionCar = HttpContext.Session.GetObject<Car>("Car");
            string sessionSortState = HttpContext.Session.GetString("SortStateCar");
            int? page = HttpContext.Session.GetInt32("CarPage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("CarPage", 0);
            }

            if (sessionCar != null)
            {
                _car = sessionCar;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["ModelSort"] = sortOrder == SortState.ModelDesc ? SortState.ModelAsc : SortState.ModelDesc;
            ViewData["ColourSort"] = sortOrder == SortState.ColourDesc ? SortState.ColourAsc : SortState.ColourDesc;

            HttpContext.Session.SetString("SortState", sortOrder.ToString());
            IQueryable<Car> Cars = Sort(db.Cars, sortOrder,
                _car.Model, _car.Colour,
                _car.StateNumber , (int)page);

            CarsViewModel CarsView = new CarsViewModel
            {
                CarViewModel = _car,
                PageViewModel = Cars,
                PageNumber = (int)page
            };

            return View(CarsView);
        }

        [HttpPost]
        public IActionResult Index(Car car)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateCar");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("CarPage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("CarPage", 0);
            }

            IQueryable<Car> cars = Sort(db.Cars, sortOrder,
                car.Model, car.Colour,
                car.StateNumber, (int)page);
            HttpContext.Session.SetObject("Car", car);

            CarsViewModel carsView = new CarsViewModel
            {
                CarViewModel = car,
                PageViewModel = cars,
                PageNumber = (int)page
            };

            return View(carsView);
        }

        private IQueryable<Car> Sort(IQueryable<Car> cars,
            SortState sortOrder, string model, string colour, string statenumber, int page)
        {
            switch (sortOrder)
            {
                case SortState.ModelAsc:
                    cars = cars.OrderBy(s => s.Model);
                    break;
                case SortState.ModelDesc:
                    cars = cars.OrderByDescending(s => s.Model);
                    break;
                case SortState.ColourAsc:
                    cars = cars.OrderBy(s => s.Colour);
                    break;
                case SortState.ColourDesc:
                    cars = cars.OrderByDescending(s => s.Colour);
                    break;
            }
            cars = cars.Include(o => o.Model).Include(o => o.Colour)
                .Include(o => o.StateNumber).Where(o => o.Model.Contains(model ?? ""))
                .Where(o => o.Colour.Contains(colour ?? ""))
                .Where(o => o.StateNumber.Contains(statenumber ?? ""))
                .Skip(page * pageSize).Take(pageSize);
            return cars;
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<Car> cars = CarContext.GetPage(0, pageSize);
            return View(cars);
        }

        [HttpPost]
        public string Add(string model, string colour, string statenumber)
        {
            return "Автомобиль " + model + " с гос. номером " + statenumber + "цвет " + colour
                + " успешно зарегистрирован";
        }
    }
}
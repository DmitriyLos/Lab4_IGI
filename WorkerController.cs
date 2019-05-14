using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Lab4.Data;
using Lab4.Models;
using Lab4.ViewModels;
using Lab4.Filters;
using Newtonsoft.Json;

namespace Lab4.Controllers
{
    [CatchExceptionFilter]
    public class WorkerController : Controller
    {
        private int pageSize = 3;
        private STOContext db;
        private Worker _worker = new Worker
        {
            FioWorker = "",
            Salary = 0
        };

        public WorkerController(STOContext WorkerContext) {
            db = WorkerContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder = SortState.No, int index = 0)
        {
            Worker sessionWorker = HttpContext.Session.GetObject<Worker>("Worker");
            string sessionSortState = HttpContext.Session.GetString("SortState");
            int? page = HttpContext.Session.GetInt32("Page");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("Page", 0);
            }
            else
            {
                if (!(page < 1 && index < 0))
                    page += index;
                HttpContext.Session.SetInt32("Page", (int)page);
            }

            if (sessionWorker != null)
            {
                _worker = sessionWorker;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["SurnameSort"] = sortOrder == SortState.FioWorkerDesc ? SortState.FioWorkerAsc : SortState.FioWorkerDesc;
            ViewData["SalarySort"] = sortOrder == SortState.SalaryDesc ? SortState.SalaryAsc : SortState.SalaryDesc;

            HttpContext.Session.SetString("SortState", sortOrder.ToString());
            IQueryable<Worker> workers = Sort(db.Workers, sortOrder,
                _worker.FioWorker, _worker.Salary, (int)page);
            WorkersViewModel workersView = new WorkersViewModel
            {
                WorkerViewModel = _worker,
                PageViewModel = workers,
                PageNumber = (int)page
            };

            return View(workersView);
        }

        [HttpPost]
        public IActionResult Index(Worker worker)
        {
            var sessionSortState = HttpContext.Session.GetString("SortState");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("Page");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("Page", 0);
            }

            IQueryable<Worker> workers = Sort(db.Workers, sortOrder,
                worker.FioWorker, worker.Salary, (int)page);
            HttpContext.Session.SetObject("Worker", worker);

            WorkersViewModel workersView = new WorkersViewModel
            {
                WorkerViewModel = worker,
                PageViewModel = workers,
                PageNumber = (int)page
            };

            return View(workersView);
        }

        private IQueryable<Worker> Sort(IQueryable<Worker> workers,
            SortState sortOrder, string fioworker, decimal salary, int page)
        {
            switch (sortOrder)
            {
                case SortState.FioWorkerAsc:
                    workers = workers.OrderBy(s => s.FioWorker);
                    break;
                case SortState.FioWorkerDesc:
                    workers = workers.OrderByDescending(s => s.FioWorker);
                    break;
                case SortState.SalaryAsc:
                    workers = workers.OrderBy(s => s.Salary);
                    break;
                case SortState.SalaryDesc:
                    workers = workers.OrderByDescending(s => s.Salary);
                    break;
            }
            workers = workers.Where(o => o.FioWorker.Contains(fioworker ?? "")).Skip(page * pageSize).Take(pageSize);
            return workers;
        }

        private void SetSessionEmployee(string sessionWorker)
        {
            _worker.FioWorker = sessionWorker.Split(':')[0];
        }
    }
}
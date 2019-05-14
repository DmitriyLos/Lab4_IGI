using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Lab4.Models;
using Lab4.Data;
using Lab4.ViewModels;
using Lab4.Filters;

namespace Lab4.Controllers
{
    [CatchExceptionFilter]
    public class OwnerController : Controller
    {
        private int pageSize = 15;
        private STOContext db;
        private Owner _owner = new Owner
        {
            FioOwner = ""
        };

        public OwnerController(STOContext OwnerContext)
        {
            db = OwnerContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder)
        {
            Owner sessionOwner = HttpContext.Session.GetObject<Owner>("Owner");
            string sessionSortState = HttpContext.Session.GetString("SortStateClient");
            int? page = HttpContext.Session.GetInt32("OwnerPage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("OwnerPage", 0);
            }

            if (sessionOwner != null)
            {
                _owner = sessionOwner;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["FioOwnerSort"] = sortOrder == SortState.FioOwnerDesc ? SortState.FioOwnerAsc : SortState.FioOwnerDesc;
            HttpContext.Session.SetString("SortStateOwner", sortOrder.ToString());

            IQueryable<Owner> owners = Sort(db.Owners, sortOrder,
                _owner.FioOwner, (int)page);
            OwnersViewModel employeesView = new OwnersViewModel
            {
                OwnerViewModel = _owner,
                PageViewModel = owners,
                PageNumber = (int)page
            };

            return View(employeesView);
        }

        [HttpPost]
        public IActionResult Index(Owner owner)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateOwner");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("OwnerPage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("OwnerPage", 0);
            }

            IQueryable<Owner> owners = Sort(db.Owners, sortOrder,
                owner.FioOwner, (int)page);
            //new Employee { Surname = surname, Post = post }
            HttpContext.Session.SetObject("Owner", owner);

            OwnersViewModel employeesView = new OwnersViewModel
            {
                OwnerViewModel = owner,
                PageViewModel = owners,
                PageNumber = (int)page
            };

            return View(employeesView);
        }

        private IQueryable<Owner> Sort(IQueryable<Owner> owners,
            SortState sortOrder, string fioowner, int page)
        {
            switch (sortOrder)
            {
                case SortState.FioOwnerAsc:
                    owners = owners.OrderBy(s => s.FioOwner);
                    break;
                case SortState.FioOwnerDesc:
                    owners = owners.OrderByDescending(s => s.FioOwner);
                    break;
            }
            owners = owners.Where(o => o.FioOwner.Contains(fioowner ?? ""))
                .Skip(page * pageSize).Take(pageSize);
            return owners;
        }
    }
}
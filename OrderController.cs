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
    public class OrderController : Controller
    {
        private int pageSize = 15;
        private STOContext db;
        private Order _order = new Order
        {
            DateReceipt = new DateTime(0),
            DateCompletion = new DateTime(0)
        };

        public OrderController(STOContext OrderContext)
        {
            db = OrderContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder)
        {
            Order sessionOrder = HttpContext.Session.GetObject<Order>("Order");
            string sessionSortState = HttpContext.Session.GetString("SortStateOrder");
            int? page = HttpContext.Session.GetInt32("OrderPage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("OrderPage", 0);
            }

            if (sessionOrder != null)
            {
                _order = sessionOrder;
                //_order = JsonConvert.DeserializeObject<Order>(sessionOrder);
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["NameSort"] = sortOrder == SortState.DateReceiptDesc ? SortState.DateReceiptAsc : SortState.DateReceiptDesc;
            ViewData["PriceSort"] = sortOrder == SortState.DateCompletionDesc ? SortState.DateCompletionAsc : SortState.DateCompletionDesc;

            HttpContext.Session.SetString("SortState", sortOrder.ToString());
            IQueryable<Order> Orders = Sort(db.Orders, sortOrder,
                _order.DateReceipt, _order.DateCompletion, (int)page);
            OrdersViewModel OrdersView = new OrdersViewModel
            {
                OrderViewModel = _order,
                PageViewModel = Orders,
                PageNumber = (int)page
            };

            return View(OrdersView);
        }

        [HttpPost]
        public IActionResult Index(Order order)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateOrder");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("OrderPage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("OrderPage", 0);
            }

            IQueryable<Order> Orders = Sort(db.Orders, sortOrder,
                order.DateReceipt, order.DateCompletion, (int)page);
            //new Order { Surname = surname, Post = post }
            HttpContext.Session.SetObject("Order", order);

            OrdersViewModel OrdersView = new OrdersViewModel
            {
                OrderViewModel = order,
                PageViewModel = Orders,
                PageNumber = (int)page
            };

            return View(OrdersView);
        }

        private IQueryable<Order> Sort(IQueryable<Order> Orders,
            SortState sortOrder, DateTime dateReceipt, DateTime? dateCompletion, int page)
        {
            switch (sortOrder)
            {
                case SortState.DateReceiptAsc:
                    Orders = Orders.OrderBy(s => s.DateReceipt);
                    break;
                case SortState.DateReceiptDesc:
                    Orders = Orders.OrderByDescending(s => s.DateReceipt);
                    break;
                case SortState.DateCompletionAsc:
                    Orders = Orders.OrderBy(s => s.DateCompletion);
                    break;
                case SortState.DateCompletionDesc:
                    Orders = Orders.OrderByDescending(s => s.DateCompletion);
                    break;
            }
            //Orders = Orders.Where(o => o.DateReceipt.Contains(dateReceipt ?? null))
            //    .Skip(page * pageSize).Take(pageSize);
            return Orders;
        }
    }
}
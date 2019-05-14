using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Lab4.ViewModels;
using Lab4.Models;
using Lab4.Data;

namespace Lab4.Services
{
    public class TakeLast
    {
        public static HomeViewModel GetHomeViewModel()
        {
            HomeViewModel homeViewModel = null;
            Thread.Sleep(2000);
            using (STOContext _context = new STOContext())
            {
                List<Worker> workers = _context.Workers.OrderByDescending(p => p.WorkerID).Take(5).ToList();
                List<Car> cars = _context.Cars.OrderByDescending(p => p.CarID).Take(5).ToList();
                List<Order> orders = _context.Orders.OrderByDescending(p => p.OrderID).Take(5).ToList();
                List<Owner> owners = _context.Owners.OrderByDescending(p => p.OwnerID).Take(5).ToList();
                homeViewModel = new HomeViewModel {
                    Owners = owners,
                    Orders = orders,
                    Cars = cars,
                    Workers = workers
                };
            }

            return homeViewModel;
        }
    }
}

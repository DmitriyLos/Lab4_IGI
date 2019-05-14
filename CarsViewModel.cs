using System;
using System.Linq;
using Lab4.Models;

namespace Lab4.ViewModels
{
    public class CarsViewModel
    {
        public Car CarViewModel { get; set; }
        public IQueryable<Car> PageViewModel { get; set; }
        public int PageNumber { get; set; }     
    }

    
}

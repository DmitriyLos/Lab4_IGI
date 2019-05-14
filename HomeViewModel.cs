using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Models;

namespace Lab4.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Worker> Workers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Owner> Owners { get; set; }
    }
}

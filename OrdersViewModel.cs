using System.Linq;
using Lab4.Models;

namespace Lab4.ViewModels
{
    public class OrdersViewModel
    {
        public Order OrderViewModel { get; set; }
        public IQueryable<Order> PageViewModel { get; set; }
        public int PageNumber { get; set; }
    }
}

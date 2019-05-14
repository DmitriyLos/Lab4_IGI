using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int CarID { get; set; }
        public DateTime DateReceipt { get; set; }
        public DateTime? DateCompletion { get; set; }
        public int WorkerID { get; set; }
        public Car Car { get; set; }
        public Worker Worker { get; set; }

        public Order() { }

        public Order(int OrderID, DateTime DateReceipt, DateTime? DateCompletion)
        {
            this.OrderID = OrderID;
            this.DateReceipt = DateReceipt;
            this.DateCompletion = DateCompletion;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Order;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.OrderID == item.OrderID;
        }

        public override int GetHashCode()
        {
            return this.OrderID.GetHashCode();
        }
    }
}

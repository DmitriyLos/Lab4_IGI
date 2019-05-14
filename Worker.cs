using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class Worker
    {
        [Key]
        public int WorkerID { get; set; }
        public string FioWorker { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime? DateOfDismissal { get; set; }
        public decimal Salary { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Worker() { }

        public Worker(int WorkerID, string FioWorker, DateTime DateOfEmployment, DateTime? DateOfDismissal, decimal Salary)
        {
            this.WorkerID = WorkerID;
            this.FioWorker = FioWorker;
            this.DateOfEmployment = DateOfEmployment;
            this.DateOfDismissal = DateOfDismissal;
            this.Salary = Salary;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Worker;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.WorkerID == item.WorkerID;
        }

        public override int GetHashCode()
        {
            return this.WorkerID.GetHashCode();
        }
    }
}

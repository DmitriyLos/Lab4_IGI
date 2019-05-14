using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Models;

namespace Lab4.Data
{
    public class OrderContext
    {
        public static List<Order> GetAll()
        {
            List<Order> all = new List<Order>();
            using (STOContext db = new STOContext())
            {
                all = db.Orders.ToList();
            }

            return all;
        }

        public static void AddOrder(Order orderToAdd)
        {
            using (STOContext db = new STOContext())
            {
                db.Orders.Add(orderToAdd);
                db.SaveChanges();
            }
        }

        public static void UpdataOrder(Order order)
        {
            using (STOContext db = new STOContext())
            {
                if (order != null)
                {
                    db.Orders.Update(order);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteOrder(Order orderToDelete)
        {
            using (STOContext db = new STOContext())
            {
                if (orderToDelete != null)
                {
                    db.Orders.Remove(orderToDelete);
                    db.SaveChanges();
                }
            }
        }

        public static List<Order> FindOrders(DateTime dateReceipt, DateTime? dateCompletion)
        {
            List<Order> orders = new List<Order>();
            using (STOContext db = new STOContext())
            {
                if (dateReceipt != null)
                {
                    orders = db.Orders.Where(k => k.DateReceipt == dateReceipt).ToList();
                }
                if (dateCompletion != null)
                {
                    if (orders.Count != 0)
                    {
                        orders = orders.Where(k => k.DateCompletion == dateCompletion).ToList();
                    }
                    else
                    {
                        orders = db.Orders.Where(k => k.DateCompletion == dateCompletion).ToList();
                    }
                }
            }
            return orders;
        }

        public static Order FindOrderById(int id)
        {
            Order order = null;
            using (STOContext db = new STOContext())
            {
                order = db.Orders.Where(k => k.OrderID == id).ToList().FirstOrDefault();
            }
            return order;
        }
    }
}

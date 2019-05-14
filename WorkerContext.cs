using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Models;

namespace Lab4.Data
{
    public class WorkerContext
    {
        public static List<Worker> GetAll()
        {
            List<Worker> all = new List<Worker>();
            using (STOContext db = new STOContext())
            {
                all = db.Workers.ToList();
            }

            return all;
        }

        public static void AddWorker(Worker workerToAdd)
        {
            using (STOContext db = new STOContext())
            {
                db.Workers.Add(workerToAdd);
                db.SaveChanges();
            }
        }

        public static void UpdataWorker(Worker worker)
        {
            using (STOContext db = new STOContext())
            {
                if (worker != null)
                {
                    db.Workers.Update(worker);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteWorker(Worker workerToDelete)
        {
            using (STOContext db = new STOContext())
            {
                if (workerToDelete != null)
                {
                    db.Workers.Remove(workerToDelete);
                    db.SaveChanges();
                }
            }
        }

        public static List<Worker> FindWorkers(string fioWorker, DateTime dateOfEmployment, DateTime? dateOfDismissal, decimal salary)
        {
            List<Worker> workers = new List<Worker>();
            using (STOContext db = new STOContext())
            {
                if (fioWorker != null && fioWorker != "")
                {
                    workers = db.Workers.Where(k => k.FioWorker == fioWorker).ToList();
                }
                if (dateOfEmployment != null)
                {
                    if (workers.Count != 0)
                    {
                        workers = workers.Where(k => k.DateOfEmployment == dateOfEmployment).ToList();
                    }
                    else
                    {
                        workers = db.Workers.Where(k => k.DateOfEmployment == dateOfEmployment).ToList();
                    }
                }
                if (dateOfDismissal != null)
                {
                    if (workers.Count != 0)
                    {
                        workers = workers.Where(k => k.DateOfDismissal == dateOfDismissal).ToList();
                    }
                    else
                    {
                        workers = db.Workers.Where(k => k.DateOfDismissal == dateOfDismissal).ToList();
                    }
                }
                if (salary != 0)
                {
                    workers = db.Workers.Where(k => k.Salary == salary).ToList();
                }
            }
            return workers;
        }

        public static Worker FindWorkerById(int id)
        {
            Worker worker = null;
            using (STOContext db = new STOContext())
            {
                worker = db.Workers.Where(k => k.WorkerID == id).ToList().FirstOrDefault();
            }
            return worker;
        }
    }
}

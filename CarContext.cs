using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab4.Models;

namespace Lab4.Data
{
    public class CarContext
    {
        public static List<Car> GetAll()
        {
            List<Car> all = new List<Car>();
            using (STOContext db = new STOContext())
            {
                all = db.Cars.ToList();
            }

            return all;
        }

        public static List<Car> GetPage(int pageNumber, int sizeOfPage)
        {
            List<Car> all = new List<Car>();
            using (STOContext db = new STOContext())
            {
                all = db.Cars.Include(t => t.Model)
                    .Include(t => t.Colour).Include(t => t.StateNumber).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }
        public static void AddCar(Car car)
        {
            using (STOContext db = new STOContext())
            {
                db.Cars.Add(car);
                db.SaveChanges();
            }
        }

        public static void UpdataCar(Car car)
        {
            using (STOContext db = new STOContext())
            {
                if (car != null)
                {
                    db.Cars.Update(car);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteCar(Car car)
        {
            using (STOContext db = new STOContext())
            {
                if (car != null)
                {
                    db.Cars.Remove(car);
                    db.SaveChanges();
                }
            }
        }

        public static List<Car> FindCar(string model, int power,
            string colour, string stateNumber, int yearOfIssue, int bodyNumber, int engineNumber)
        {
            List<Car> cars = new List<Car>();
            using (STOContext db = new STOContext())
            {
                if (model != null)
                {
                    cars = db.Cars.Where(k => k.Model == model).ToList();
                }
                if (power != 0)
                {
                    if (cars.Count != 0)
                    {
                        cars = cars.Where(k => k.Power == power).ToList();
                    }
                    else
                    {
                        cars = db.Cars.Where(k => k.Power == power).ToList();
                    }
                }
                if (colour != null )
                {
                    if (cars.Count != 0)
                    {
                        cars = cars.Where(k => k.Colour == colour).ToList();
                    }
                    else
                    {
                        cars = db.Cars.Where(k => k.Colour == colour).ToList();
                    }
                }
                if (stateNumber != null)
                {
                    if (cars.Count != 0)
                    {
                        cars = cars.Where(k => k.StateNumber == stateNumber).ToList();
                    }
                    else
                    {
                        cars = db.Cars.Where(k => k.StateNumber == stateNumber).ToList();
                    }
                }
                if (yearOfIssue != 0)
                {
                    if (cars.Count != 0)
                    {
                        cars = cars.Where(k => k.YearOfIssue == yearOfIssue).ToList();
                    }
                    else
                    {
                        cars = db.Cars.Where(k => k.YearOfIssue == yearOfIssue).ToList();
                    }
                }
                if (bodyNumber != 0)
                {
                    if (cars.Count != 0)
                    {
                        cars = cars.Where(k => k.BodyNumber == bodyNumber).ToList();
                    }
                    else
                    {
                        cars = db.Cars.Where(k => k.BodyNumber == bodyNumber).ToList();
                    }
                }
                if (engineNumber != 0)
                {
                    if (cars.Count != 0)
                    {
                        cars = cars.Where(k => k.EngineNumber == engineNumber).ToList();
                    }
                    else
                    {
                        cars = db.Cars.Where(k => k.EngineNumber == engineNumber).ToList();
                    }
                }
            }
            return cars;
        }

        public static Car FindCarById(int id)
        {
            Car car = null;
            using (STOContext db = new STOContext())
            {
                car = db.Cars.Where(k => k.CarID == id).ToList().FirstOrDefault();
            }
            return car;
        }
    }
}

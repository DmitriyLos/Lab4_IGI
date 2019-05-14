using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Models;

namespace Lab4.Data
{
    public static class DbInitializer
    {
        public static void Initialize(STOContext db)
        {
            db.Database.EnsureCreated();
            int carsNumber = 25;
            int ordersNumber = 50;
            int ownersNumber = 10;
            int workersNumber = 10;

            InitializeOwners(db, ownersNumber);
            InitializeWorkers(db, workersNumber);
            InitializeCars(db, carsNumber, ownersNumber);
            InitializeOrders(db, ordersNumber, carsNumber, workersNumber);
        }

        private static void InitializeOwners(STOContext db, int ownersNumber)
        {


            //проверка, занесены ли данные в Owners
            if (db.Owners.Any())
            {
                return; //бд иницилизирована
            }

            int ownerID;
            int driverLicense;
            string fioOwner;
            string adress;
            int phone;

            Random randomObj = new Random(1);

            for (int OwnerID = 1; OwnerID <= ownersNumber; OwnerID++)
            {
                ownerID = OwnerID;
                driverLicense = randomObj.Next(1, 3000);
                fioOwner = MyRandom.RandomString(15);
                adress = MyRandom.RandomString(15);
                phone = randomObj.Next(1, 1000000);

                db.Owners.Add(new Owner
                {
                    DriverLicense = driverLicense,
                    FioOwner = fioOwner,
                    Adress = adress,
                    Phone = phone
                });
            }

            //сохранение изменений в бд, связанную с объектом контекста
            db.SaveChanges();
        }

        
 
        private static void InitializeWorkers(STOContext db, int workersNumber)
        {


            //проверка, занесены ли данные в Workers
            if (db.Workers.Any())
            {
                return; //бд иницилизирована
            }

            string fioWorker;
            DateTime dateOfEmployment;
            DateTime? dateOfDismissal;
            decimal salary;

            Random randomObj = new Random(1);

            for (int WorkerID = 1; WorkerID <= workersNumber; WorkerID++)
            {
                var date = new DateTime(randomObj.Next(1990, 2018),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 28));
                fioWorker = MyRandom.RandomString(15);
                var twoDate = new DateTime(randomObj.Next(1, 10),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 28));
                if (WorkerID % 5 == 0)
                {
                    dateOfDismissal = date.Add(new TimeSpan(twoDate.Ticks));
                }
                else
                {
                    dateOfDismissal = null;
                }
                dateOfEmployment = date;
                salary = randomObj.Next(100, 1000);

                db.Workers.Add(new Worker
                {
                    FioWorker = fioWorker,
                    DateOfDismissal = dateOfDismissal,
                    DateOfEmployment = dateOfEmployment,
                    Salary = salary,
                });
            }

            //сохранение изменений в бд, связанную с объектом контекста
            db.SaveChanges();
        }

        private static void InitializeCars(STOContext db, int carsNumber, int ownersNumber)
        {


            //проверка, занесены ли данные в Cars
            if (db.Cars.Any())
            {
                return; //бд иницилизирована
            }

            int ownerID;
            string model;
            int power;
            string colour;
            string stateNumber;
            int yearOfIssue;
            int bodyNumber;
            int engineNumber;

            Random randomObj = new Random(1);

            for (int CarID = 1; CarID <= carsNumber; CarID++)
            {
                ownerID = db.Owners.Skip(randomObj.Next(0, db.Owners.Count() - 2)).First().OwnerID;
                model = MyRandom.RandomString(10);
                power = randomObj.Next(10, 100);
                colour = MyRandom.RandomString(10);
                stateNumber = MyRandom.RandomString(9);
                yearOfIssue = randomObj.Next(1, 4);
                bodyNumber = randomObj.Next(1, 10);
                engineNumber = randomObj.Next(1, 10);

                db.Cars.Add(new Car
                {
                    OwnerID = ownerID,
                    Model = model,
                    Power = power,
                    Colour = colour,
                    StateNumber = stateNumber,
                    YearOfIssue = yearOfIssue,
                    BodyNumber = bodyNumber,
                    EngineNumber = engineNumber
                });
            }

            //сохранение изменений в бд, связанную с объектом контекста
            db.SaveChanges();
        }

        private static void InitializeOrders(STOContext db, int ordersNumber, int carsNumber, int workersNumber)
        {


            //проверка, занесены ли данные в Orders
            if (db.Orders.Any())
            {
                return; //бд иницилизирована
            }

            int carID;
            DateTime dateReceipt;
            DateTime? dateCompletion;
            int workerID;

            Random randomObj = new Random(DateTime.Now.Month * 30 + DateTime.Now.Year * 365 + DateTime.Now.Day
                + DateTime.Now.Minute);

            for (int OrderID = 1; OrderID <= ordersNumber; OrderID++)
            {
                var date = new DateTime(randomObj.Next(2000, 2020),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 28));
                carID = db.Cars.Skip(randomObj.Next(0, db.Cars.Count() - 2)).First().CarID;
                dateReceipt = date;
                var twoDate = new DateTime(randomObj.Next(1, 10),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 28));
                if (OrderID % 5 == 0)
                {
                    dateCompletion = date.Add(new TimeSpan(twoDate.Ticks));
                }
                else
                {
                    dateCompletion = null;
                }
                workerID = db.Workers.Skip(randomObj.Next(0, db.Workers.Count() - 2)).First().WorkerID;

                db.Orders.Add(new Order
                {
                    CarID = carID,
                    DateReceipt = dateReceipt,
                    DateCompletion = dateCompletion,
                    WorkerID = workerID
                });
            }

            //сохранение изменений в бд, связанную с объектом контекста
            db.SaveChanges();
        }

        
    }
}

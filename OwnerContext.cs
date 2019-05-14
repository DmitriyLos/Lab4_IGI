using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Models;

namespace Lab4.Data
{
    public class OwnerContext
    {
        public static List<Owner> GetAll()
        {
            List<Owner> all = new List<Owner>();
            using (STOContext db = new STOContext())
            {
                all = db.Owners.ToList();
            }

            return all;
        }

        public static void AddOwner(Owner ownerToAdd)
        {
            using (STOContext db = new STOContext())
            {
                db.Owners.Add(ownerToAdd);
                db.SaveChanges();
            }
        }

        public static void UpdataOwner(Owner owner)
        {
            using (STOContext db = new STOContext())
            {
                if (owner != null)
                {
                    db.Owners.Update(owner);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteOwner(Owner ownerToDelete)
        {
            using (STOContext db = new STOContext())
            {
                if (ownerToDelete != null)
                {
                    db.Owners.Remove(ownerToDelete);
                    db.SaveChanges();
                }
            }
        }

        public static List<Owner> FindOwners(int ownerID, int driverLicense,
            string fioOwner, string adress, int phone)
        {
            List<Owner> owners = new List<Owner>();
            using (STOContext db = new STOContext())
            {
                if (ownerID != 0)
                {
                    owners = db.Owners.Where(k => k.OwnerID == ownerID).ToList();
                }
                if (driverLicense != 0)
                {
                    if (owners.Count != 0)
                    {
                        owners = owners.Where(k => k.DriverLicense == driverLicense).ToList();
                    }
                    else
                    {
                        owners = db.Owners.Where(k => k.DriverLicense == driverLicense).ToList();
                    }
                }
                if (fioOwner != null)
                {
                    if (owners.Count != 0)
                    {
                        owners = owners.Where(k => k.FioOwner == fioOwner).ToList();
                    }
                    else
                    {
                        owners = db.Owners.Where(k => k.FioOwner == fioOwner).ToList();
                    }
                }
                if (adress != null)
                {
                    if (owners.Count != 0)
                    {
                        owners = owners.Where(k => k.Adress == adress).ToList();
                    }
                    else
                    {
                        owners = db.Owners.Where(k => k.Adress == adress).ToList();
                    }
                }
                if (phone != 0)
                {
                    if (owners.Count != 0)
                    {
                        owners = owners.Where(k => k.Phone == phone).ToList();
                    }
                    else
                    {
                        owners = db.Owners.Where(k => k.Phone == phone).ToList();
                    }
                }
            }
            return owners;
        }

        public static Owner FindOwnerById(int id)
        {
            Owner owner = null;
            using (STOContext db = new STOContext())
            {
                owner = db.Owners.Where(k => k.OwnerID == id).ToList().FirstOrDefault();
            }
            return owner;
        }
    }
}

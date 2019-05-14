using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
     public class Owner
    {
        [Key]
        public int OwnerID { get; set; }
        public int DriverLicense { get; set; }
        public string FioOwner { get; set; }
        public string Adress { get; set; }
        public int Phone { get; set; }
        public ICollection<Car> Cars { get; set; }

        public Owner() { }

        public Owner(int OwnerID, int DriverLicense, string FioOwner, string Adress,
            int Phone)
        {
            this.OwnerID = OwnerID;
            this.DriverLicense = DriverLicense;
            this.FioOwner = FioOwner;
            this.Adress = Adress;
            this.Phone = Phone;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Owner;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.OwnerID == item.OwnerID;
        }

        public override int GetHashCode()
        {
            return this.OwnerID.GetHashCode();
        }
    }
}

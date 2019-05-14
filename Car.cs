using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class Car
    {
        [Key]
        public int CarID { get; set; }
        public int OwnerID { get; set; }
        public string Model { get; set; }
        public int Power { get; set; }
        public string Colour { get; set; }
        public string StateNumber { get; set; }
        public int YearOfIssue { get; set; }
        public int BodyNumber { get; set; }
        public int EngineNumber { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Owner Owner { get; set; }
        public Car() { }

        public Car(int CarID, string Model, int Power, string Colour, string StateNumber, int YearOfIssue,
            int BodyNumber, int EngineNumber)
        {
            this.CarID = CarID;
            this.Model = Model;
            this.Power = Power;
            this.Colour = Colour;
            this.StateNumber = StateNumber;
            this.YearOfIssue = YearOfIssue;
            this.BodyNumber = BodyNumber;
            this.EngineNumber = EngineNumber;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Car;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.CarID == item.CarID;
        }

        public override int GetHashCode()
        {
            return this.CarID.GetHashCode();
        }
    }
}

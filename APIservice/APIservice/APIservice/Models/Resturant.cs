using System.Collections.Generic;

namespace APIservice.Models
{
    public class Resturant : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string LogoPath { get; set; }
        public int Rank { get; set; }
        public List<Category> Categories { get; set; }
        public override string ToString()
        {
            return Id+" || "+Name + " || " +Suburb;
        }
    }
}
using System.Collections.Generic;

namespace TacoChallenge.Models
{
    public class FoodResultView
    {
        public string Name { set; get; }
        public string Suburb { set; get; }
        public int Rank { set; get; }
        public List<Category> Categories { set; get; }
        public string LogoPath { set; get; }
        public override string ToString()
        {
            return Name + " || " + Suburb;
        }
    }
}   
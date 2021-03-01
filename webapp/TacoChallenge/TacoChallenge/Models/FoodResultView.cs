using System.Collections.Generic;

namespace TacoChallenge.Models
{
    public class FoodResultView
    {
        public string ResturantName { set; get; }
        public string Suburb { set; get; }
        public int Rank { set; get; }
        public List<Category> Categories { set; get; }
        public List<MenuItem> FoodMenuItems { set; get; }
        public string LogoPath { set; get; }
    }
}   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIservice.Models
{
    public class FoodResultsContainer
    {
        public List<FoodResultView> MealViewList { set; get; }
        public string ParsedMeal { set; get; }
        public string ParsedLocation { set; get; }
    }
}

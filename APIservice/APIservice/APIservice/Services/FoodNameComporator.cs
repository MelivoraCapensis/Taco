using System.Collections;
using System.Collections.Generic;
using System.Linq;
using APIservice.Models;

namespace APIservice.Services
{
    public class FoodNameComporator : IComparer<Resturant>
    {
        private string foodName = "";

        public FoodNameComporator(string _foodName)
        {
            this.foodName = _foodName;
        }
        public int Compare(Resturant x, Resturant y)
        {
            if (x == null && y != null)
                return 1;
            if (x != null && y == null)
                return -1;

            var xFoodsMachCount = (x.Categories
                    .SelectMany(category => category.MenuItems, (category, menuItem) => new {category, menuItem})
                    .Where(t => t.menuItem.Name.Contains(this.foodName))
                    .Select(t => t.menuItem)).Count();

            var yFoodsMachCount = (from category in y.Categories
                from menuItem in category.MenuItems
                where menuItem.Name.Contains(this.foodName)
                select menuItem).Count();
            
            // Descending direction
            if (xFoodsMachCount > yFoodsMachCount)
                return -1;
            if (xFoodsMachCount < yFoodsMachCount)
                return 1;
            if(x.Rank > y.Rank)
                return -1;
            if (x.Rank < y.Rank)
                return 1;
            return 0;
        }
    }

    public class FoodVeiwNameComporator : IComparer<FoodResultView>
    {
        private string foodName = "";

        public FoodVeiwNameComporator(string _foodName)
        {
            this.foodName = _foodName;
        }

        public int Compare(FoodResultView x, FoodResultView y)
        {
            if (x == null && y != null)
                return 1;
            if (x != null && y == null)
                return -1;

            var xFoodsMachCount = x.Categories
                .SelectMany(category => category.MenuItems, (category, menuItem) => new { category, menuItem })
                .Select(t => t.menuItem).Count();

            var yFoodsMachCount = (from category in y.Categories
                from menuItem in category.MenuItems
                select menuItem).Count();

            // Descending direction
            if (xFoodsMachCount > yFoodsMachCount)
                return -1;
            if (xFoodsMachCount < yFoodsMachCount)
                return 1;
            if (x.Rank > y.Rank)
                return -1;
            if (x.Rank < y.Rank)
                return 1;
            return 0;
        }
    }
}
using System.Collections.Generic;

namespace TacoChallenge.Models
{
    public class Category
    {
            public string Name { get; set; }
            public List<MenuItem> MenuItems { get; set; }
    }
}
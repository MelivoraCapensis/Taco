using System.Collections.Generic;

namespace APIservice.Models
{
    public class Category
    {
            public string Name { get; set; }
            public List<MenuItem> MenuItems { get; set; }
    }
}
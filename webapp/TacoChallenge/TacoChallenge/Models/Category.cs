using System.Collections.Generic;
using TacoChallenge.Data;

namespace TacoChallenge.Models
{
    public class Category
    {
            public string Name { get; set; }
            public List<MenuItem> MenuItems { get; set; }
    }
}
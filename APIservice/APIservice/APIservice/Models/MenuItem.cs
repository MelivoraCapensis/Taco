﻿namespace APIservice.Models
{
    public class MenuItem : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
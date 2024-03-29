﻿namespace CaloriesCounterAPI.Models
{
    public class Meal
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public virtual ICollection<ProductAdded>? ProductsAdded { get; set; }
    }
}

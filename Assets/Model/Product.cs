using System;

namespace Model
{
    [Serializable]
    public class Product
    {
        public Int64 Project_id { get; set; }
        public String Title { get; set; }
        public Decimal Price { get; set; }
        public Double Quality { get; set; }
        public Decimal Prime_cost { get; set; }


        public Product(Int64 project_id, String title, Decimal price, Double quality,
                        Decimal prime_cost)
        {
            Project_id = project_id;
            Title = title;
            Price = price;
            Quality = quality;
            Prime_cost = prime_cost;

        }

        public Product() { }
    }
}

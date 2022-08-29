using System;

namespace ShopManagement
{
    class Product
    {
        private string name;
        private string origin;
        private double price;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0 || value > ShopConst.PriceLimit)
                    throw new ArgumentException("Product price cannot be" +
                        $" negative or higher than ${ShopConst.PriceLimit}.");
                price = value;
            }
        }

        public Product(string name, string origin, double price)
        {
            Name = name;
            Origin = origin;
            Price = price;
        }
        public override string ToString()
        {
            return $"{Name} - {Origin} - ${Price:0.00}";
        }
    }

}

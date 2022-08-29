using System;

namespace ShopManagement
{
    class Item
    {
        private Product type;
        private int quantity;
        private DateTime? exportDate;

        public Product Type
        {
            get { return type; }
            private set { type = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value <= 0 || value > ShopConst.QuantityLimit)
                    throw new ArgumentException("The shop doesn't accept" +
                        $" item with negative quantity or " +
                        $"quantity larger than {ShopConst.QuantityLimit}.");
                quantity = value;
            }
        }
        public DateTime? ExportDate
        {
            get { return exportDate; }
            set { exportDate = value; }
        }

        public Item(Product type, int quantity)
        {
            Type = type;
            Quantity = quantity;
        }

        public Item(Product type, int quantity, DateTime exportDate)
            : this(type, quantity)
        {
            ExportDate = exportDate;
        }

        public double Price()
        {
            return Type.Price * Quantity;
        }

        public override string ToString()
        {
            return $"{Type.Name} - {Quantity} - " +
                $"${Price():0.00} - {ExportDate}";
        }
    }

}

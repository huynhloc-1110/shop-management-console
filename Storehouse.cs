using System;
using System.Collections.Generic;

namespace ShopManagement
{
    class Storehouse : ItemContainer
    {
        private string location;

        public Storehouse(string location)
        {
            Location = location;
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public override Item TakeOut(Product product, int quantity)
        {
            Item item = base.TakeOut(product, quantity);
            item.ExportDate = DateTime.Now;
            return item;
        }

        public override string ToString()
        {
            return Location + " " + base.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement
{
    class Cart : ItemContainer
    {
        public Cart() { }

        public double TotalPrice()
        {
            double totalPrice = 0;
            foreach (Item item in itemList)
            {
                totalPrice += item.Price();
            }

            return totalPrice;
        }

        public Bill Purchase()
        {
            if (IsEmpty())
                return null;
            return new Bill(DateTime.Now, this);
        }
        public override string ToString()
        {
            StringBuilder st = new StringBuilder(base.ToString());
            st.Append("Total Price: $");
            st.Append(TotalPrice().ToString("0.00"));
            st.Append("\n");
            return st.ToString();
        }
    }
}

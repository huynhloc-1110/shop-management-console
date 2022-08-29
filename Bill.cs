using System;

namespace ShopManagement
{
    class Bill
    {
        private Cart thisCart;
        private DateTime date;

        public Cart ThisCart
        {
            get { return thisCart; }
            set { thisCart = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public Bill(DateTime date, Cart thisCart)
        {
            Date = date;
            ThisCart = thisCart;
        }

        public override string ToString()
        {
            return Date + " " + thisCart;
        }
    }
}

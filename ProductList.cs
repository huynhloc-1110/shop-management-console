using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement
{
    class ProductList
    {
        private List<Product> productList;

        public ProductList()
        {
            productList = new List<Product>();
        }

        public Product Get(int id)
        {
            IdCheck(id);
            return productList[id];
        }

        public void Add(Product product)
        {
            productList.Add(product);
        }

        public void Edit(int id, string name, string origin, double price)
        {
            IdCheck(id);
            productList[id].Name = name;
            productList[id].Origin = origin;
            productList[id].Price = price;
        }

        public void Delete(Product product)
        {
            for (int i = 0; i < productList.Count; i++)
                if (productList[i] == product)
                {
                    productList.RemoveAt(i);
                    return;
                }
            throw new ArgumentException("Product not found.");
        }

        public void DeleteAt(int id)
        {
            IdCheck(id);
            productList.RemoveAt(id);
        }

        private void IdCheck(int id)
        {
            if (id >= productList.Count || id < 0)
                throw new ArgumentException("Product id not found.");
        }

        public override string ToString()
        {
            StringBuilder st = new StringBuilder();
            st.Append("Product list:\n");

            if (productList.Count == 0)
                st.Append("#0\tEmpty\n");
            else
                for (int i = 0; i < productList.Count; i++)
                {
                    st.Append($"#{i}\t");
                    st.Append(productList[i]);
                    st.Append("\n");
                }
            return st.ToString();
        }
    }

}

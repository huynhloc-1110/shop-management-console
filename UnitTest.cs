using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement
{
    class ProductTest
    {
        static void Main(string[] args)
        {
            Product product = new Product("Clock", "Japan", 10);
            Console.WriteLine(product);

            product.Name = "Clock No.1";
            product.Origin = "Germany";
            product.Price = 5;

            Console.WriteLine(product);

            // product.Price = -1;
            // product.Price = ShopConst.PriceLimit + 1;

            // Product product2 = new Product("Plushie", "Japan", -4);
        }
    }

    class ItemTest
    {
        static void Main(string[] args)
        {
            Product product = new Product("Clock", "Japan", 10);
            Item item = new Item(product, 12);
            Console.WriteLine(item);

            item.Quantity = 20;
            item.ExportDate = DateTime.Now;
            Console.WriteLine(item);

            // item.Quantity = 0;
            // item.Quantity = ShopConst.QuantityLimit + 1;
        }
    }

    class ProductListTest
    {
        static void Main(string[] args)
        {
            var product1 = new Product("Clock", "Japan", 10);
            var product2 = new Product("Laptop", "America", 1000);
            var product3 = new Product("IPhone", "America", 200);

            ProductList pList = new ProductList();
            pList.Add(product1);
            pList.Add(product2);
            pList.Add(product3);

            pList.DeleteAt(2);
            pList.Edit(1, "Laptop", "Italy", 900);

            Console.WriteLine(pList);
            Console.WriteLine();
            Console.WriteLine(pList.Get(0));

            // Console.WriteLine(pList.Get(3));
            // pList.Edit(3, "Error", "Vietnam", 120);
            // pList.Delete(3);
        }
    }

    class CartTest
    {
        static void Main(string[] args)
        {
            Cart cart = new Cart();

            var product1 = new Product("Clock", "Japan", 10);
            var product2 = new Product("Laptop", "America", 1000);
            var product3 = new Product("IPhone", "America", 200);
            var product4 = new Product("FakePhone", "Vietnam", 50);

            var item1 = new Item(product1, 1, DateTime.Now);
            var item2 = new Item(product2, 2);
            var item3 = new Item(product3, 3);

            cart.PutIn(item1);
            cart.PutIn(item2);
            cart.PutIn(item3);
            cart.PutIn(item2);

            Console.WriteLine(cart);
            Console.WriteLine("Take out from cart: " + cart.TakeOut(product1, 1));
            Console.WriteLine("Take out from cart: " + cart.TakeOut(product2, 3));
            Console.WriteLine("Take out from cart: " + cart.TakeOut(product3, 3));
            Console.WriteLine("Take out from cart: " + cart.TakeOut(product2, 1) + "\n");
            Console.WriteLine(cart);

            // cart.PutIn(null);
            // cart.TakeOut(product4, 10);
            // cart.TakeOut(product2, 2);
        }
    }

    class StorehouseTest
    {
        static void Main(string[] args)
        {
            var product1 = new Product("Clock", "Japan", 10);
            var item1 = new Item(product1, 1);

            Storehouse stHouse = new Storehouse("HCM City");
            stHouse.PutIn(item1);

            stHouse.Location = "Ha Noi Capital";
            Console.WriteLine("Location: " + stHouse.Location + "\n");

            Console.WriteLine(stHouse);
            Console.WriteLine("Take out from storehouse: " + stHouse.TakeOut(product1, 1) + "\n");
            Console.WriteLine(stHouse);
        }
    }

    class BillTest
    {
        static void Main(string[] args)
        {
            Cart cart = new Cart();

            var product1 = new Product("Clock", "Japan", 10);
            var product2 = new Product("Laptop", "America", 1000);
            var product3 = new Product("IPhone", "America", 200);

            var item1 = new Item(product1, 1, DateTime.Now);
            var item2 = new Item(product2, 2);
            var item3 = new Item(product3, 3);

            cart.PutIn(item1);
            cart.PutIn(item2);
            cart.PutIn(item3);

            Bill bill = cart.Purchase();
            Console.WriteLine(bill);
        }
    }
}

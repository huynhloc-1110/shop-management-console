using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement
{
    class Shop
    {
        private ProductList productList;
        private Storehouse storehouse;
        private List<Bill> billList;

        public Shop()
        {
            productList = new ProductList();
            storehouse = new Storehouse("HCM City");
            billList = new List<Bill>();
        }

        public void DisplayIntro()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== SHOP MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Sell item");
            Console.WriteLine("2. Import item");
            Console.WriteLine("3. Manage product");
            Console.WriteLine("4 and other. Exit\n");
            Console.ResetColor();
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    DisplayIntro();
                    Write(ConsoleColor.Cyan, "Choose function: ");
                    string option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            SellItem();
                            break;
                        case "2":
                            ImportItem();
                            break;
                        case "3":
                            ManageOption();
                            break;
                        default:
                            Write(ConsoleColor.Red, "Exit the system.");
                            return;
                    }
                }
            }
            catch(OperationCanceledException e)
            {
                WriteLine(ConsoleColor.Red, e.Message);
                Run();
            }
            catch(Exception e)
            {
                WriteLine(ConsoleColor.Red, e.Message);
            }
        }

        #region Sell Item methods
        public void SellItem()
        {
            WriteLine(ConsoleColor.Yellow, "\n== Sell item ==");
            Cart cart = new Cart();
            while (true)
            {
                Console.WriteLine(productList);
                Console.WriteLine(storehouse);
                
                RetrieveItemTo(cart);
                Console.WriteLine(cart);

                Write(ConsoleColor.Cyan, "Press 1 to continue shopping, " +
                    "2 to confirm purchase, or other to cancle the current cart: ");
                string option = Console.ReadLine();
                switch(option)
                {
                    case "1":
                        continue;
                    case "2":
                        Bill bill = cart.Purchase();
                        if (bill == null)
                        {
                            WriteLine(ConsoleColor.Red, "Cannot purchase an empty cart!");
                            return;
                        }
                        billList.Add(bill);
                        WriteLine(ConsoleColor.Green, "Purchase successfully. Here is your bill.");
                        Console.WriteLine(bill);
                        return;
                    default:
                        cart.TransferTo(storehouse);
                        WriteLine(ConsoleColor.Red, "Cart cancelled.");
                        return;
                }
            }
        }

        private void RetrieveItemTo(Cart cart)
        {
            if (storehouse.IsEmpty())
            {
                WriteLine(ConsoleColor.Red, "Store house is empty now! " +
                    "No item left to buy!");
                return;
            }
            try
            {
                Product product = InputProduct();
                int quantity = InputQuantity();

                Item item = storehouse.TakeOut(product, quantity);

                cart.PutIn(item);

                WriteLine(ConsoleColor.Green, "Item successfully added to cart.");
            }
            catch (ArgumentException e)
            {
                WriteLine(ConsoleColor.Red, e.Message);
                WriteLine(ConsoleColor.Cyan, "Please input again.");
                RetrieveItemTo(cart);
            }
            catch (FormatException e)
            {
                WriteLine(ConsoleColor.Red, e.Message);
                WriteLine(ConsoleColor.Cyan, "Please input again.");
                RetrieveItemTo(cart);
            }
        }
        #endregion

        #region Import Item methods
        public void ImportItem()
        {
            try
            {
                WriteLine(ConsoleColor.Yellow, "\n== Import new item ==");
                Console.WriteLine(productList);

                Product product = InputProduct();
                int quantity = InputQuantity();
                Item item = new Item(product, quantity);

                storehouse.PutIn(item);
            }
            catch (ArgumentException e)
            {
                WriteLine(ConsoleColor.Red, e.Message);
                WriteLine(ConsoleColor.Cyan, "Please input again.");
                ImportItem();
            }
            catch (FormatException e)
            {
                WriteLine(ConsoleColor.Red, e.Message);
                WriteLine(ConsoleColor.Cyan, "Please input again.");
                ImportItem();
            }
        }
        #endregion

        #region Manage product list methods
        public void ManageOption()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n== Product list manage ==");
            Console.WriteLine("1. Create new product.");
            Console.WriteLine("2. Edit a product.");
            Console.WriteLine("3. Delete a product");
            Console.WriteLine("4 or other. Back to main menu.\n");

            Write(ConsoleColor.Cyan, "Choose option: ");
            String option = Console.ReadLine();
            switch(option)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    EditProduct();
                    break;
                case "3":
                    DeleteProduct();
                    break;
                default:
                    WriteLine(ConsoleColor.Red, "Quit managing product. Back to main menu.");
                    break;
            }
        }

        private void AddProduct()
        {
            WriteLine(ConsoleColor.Yellow, "\n== Create new product ==");
            Write(ConsoleColor.Cyan, "Input product name (press 'X' to cancle action): ");
            string name = Console.ReadLine();
            CheckExit(name);

            Write(ConsoleColor.Cyan, "Input product origin (press 'X' to cancle action): ");
            string origin = Console.ReadLine();
            CheckExit(origin);

            double price;
            while (true)
            {
                Write(ConsoleColor.Cyan, "Input product price in US dollar" +
                    " (press 'X' to cancle action): ");
                string st = Console.ReadLine();
                CheckExit(st);

                double.TryParse(st, out price);
                if (price <= 0 || price > ShopConst.PriceLimit)
                    WriteLine(ConsoleColor.Red, "Invalid input. Please enter again.");
                else
                    break;
            }

            productList.Add(new Product(name, origin, price));
        }

        private void EditProduct()
        {
            try
            {
                WriteLine(ConsoleColor.Yellow, "\n== Edit a product ==");
                Console.WriteLine(productList);
                Product product = InputProduct();
                Write(ConsoleColor.Cyan, "Input product name (press 'X' to cancle action): ");
                string name = Console.ReadLine();
                CheckExit(name);

                Write(ConsoleColor.Cyan, "Input product origin (press 'X' to cancle action): ");
                string origin = Console.ReadLine();
                CheckExit(origin);

                double price;
                while (true)
                {
                    Write(ConsoleColor.Cyan, "Input product price in US dollar" +
                        " (press 'X' to cancle action): ");
                    string st = Console.ReadLine();
                    CheckExit(st);

                    double.TryParse(st, out price);
                    if (price <= 0 || price > ShopConst.PriceLimit)
                        WriteLine(ConsoleColor.Red, "Invalid input. Please enter again.");
                    else
                        break;
                }

                product.Name = name;
                product.Origin = origin;
                product.Price = price;
            }
            catch (ArgumentException e)
            {
                WriteLine(ConsoleColor.Red, e.Message);
                WriteLine(ConsoleColor.Cyan, "Please input again.");
                EditProduct();
            }
            catch (FormatException e)
            {
                WriteLine(ConsoleColor.Red, e.Message);
                WriteLine(ConsoleColor.Cyan, "Please input again.");
                EditProduct();
            }
        }

        private void DeleteProduct()
        {
            try
            {
                WriteLine(ConsoleColor.Yellow, "\n== Delete a product ==");
                Console.WriteLine(productList);
                 Product product = InputProduct();
                productList.Delete(product);
            }
            catch (ArgumentException e)
            {
                WriteLine(ConsoleColor.Red, e.Message);
                WriteLine(ConsoleColor.Cyan, "Please input again.");
                DeleteProduct();
            }
            catch (FormatException e)
            {
                WriteLine(ConsoleColor.Red, e.Message);
                WriteLine(ConsoleColor.Cyan, "Please input again.");
                DeleteProduct();
            }
        }

        #endregion

        #region Reusable Input Methods
        private Product InputProduct()
        {
            Write(ConsoleColor.Cyan, "Input product id (press 'X' to cancle action): ");
            string st = Console.ReadLine();
            CheckExit(st);

            int productId = int.Parse(st);

            return productList.Get(productId);
        }

        private int InputQuantity()
        {
            Write(ConsoleColor.Cyan, "Input item quantity (press 'X' to cancle action): ");
            string st = Console.ReadLine();
            CheckExit(st);

            int quantity = int.Parse(st);

            return quantity;
        }

        private void CheckExit(string st)
        {
            if (st == "X" || st == "x")
                throw new OperationCanceledException("Action canceled. Back to menu.");
        }
        #endregion

        #region Console Write with Color
        private static void Write(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        private static void WriteLine(ConsoleColor color, string message)
        {
            Write(color, message + "\n");
        }
        #endregion
    }
}

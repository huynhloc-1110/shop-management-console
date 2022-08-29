using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement
{
    abstract class ItemContainer
    {
        protected List<Item> itemList;

        public ItemContainer()
        {
            itemList = new List<Item>();
        }

        public void PutIn(Item newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException("Null item.");

            int itemIndex = FindItemIndex(newItem.Type);
            if (itemIndex >= 0)
            {
                Item item = itemList[itemIndex];
                item.Quantity += newItem.Quantity;
                return;
            }
            itemList.Add(newItem);
        }
        public virtual Item TakeOut(Product product, int quantity)
        {
            int itemIndex = FindItemIndex(product);
            if (itemIndex < 0)
                throw new ArgumentException("Item not found!");

            if (quantity <= 0)
                throw new ArgumentException("Cannot take out a 0 or negative quantity!");

            Item item = itemList[FindItemIndex(product)];
            if (item.Quantity < quantity)
                throw new ArgumentException("Item found but not enough!");
            if (item.Quantity == quantity)
                itemList.RemoveAt(itemIndex);
            else
                item.Quantity -= quantity;
            return new Item(product, quantity);
        }

        public void TransferTo(ItemContainer iContainer)
        {
            if (itemList.Count == 0)
                return;
            foreach (Item item in itemList)
                iContainer.PutIn(item);
        }

        public bool IsEmpty()
        {
            return itemList.Count == 0;
        }

        private int FindItemIndex(Product type)
        {
            for (int i = 0; i < itemList.Count; i++)
                if (itemList[i].Type == type)
                    return i;

            return -1;
        }
        public override string ToString()
        {
            StringBuilder st = new StringBuilder();
            st.Append(this.GetType().Name);
            st.Append(" list:\n");

            if (itemList.Count == 0)
                st.Append("#\tEmpty\n");
            else
                for (int i = 0; i < itemList.Count; i++)
                {
                    st.Append($"#\t");
                    st.Append(itemList[i]);
                    st.Append("\n");
                }
            return st.ToString();
        }
    }
}

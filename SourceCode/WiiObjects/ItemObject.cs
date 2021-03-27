using System.Collections.Generic;
using System.Linq;

namespace WiiObjects
{
    public class ItemObject
    {
        public int Index { get; set; }
        public string ItemValue { get; set; }
    }

    public class ItemCollection
    {
        private List<ItemObject> itemList = null;

        public ItemCollection()
        {
            itemList = new List<ItemObject>();
        }

        public string ListItemString()
        {
            string listItem = string.Empty;
            foreach (var item in itemList)
            {
                listItem += item.ItemValue + ",";
            }

            return listItem.Remove(listItem.Length - 1);
        }

        public List<ItemObject> ItemList()
        {
            return itemList;
        }

        public void AddItem(ItemObject item)
        {
            itemList.Add(item);
        }

        public ItemObject GetItemByIndex(int index)
        {
            ItemObject item = null;
            var query = itemList.Where(r => r.Index == index);
            if (query == null)
                return null;
            item = new ItemObject
            {
                Index = query.First().Index,
                ItemValue = query.First().ItemValue
            };
            return item;
        }
    }
}

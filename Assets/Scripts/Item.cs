// Item.cs
namespace InventorySystem
{
    public class Item
    {
        public string itemName;
        public int itemID;

        public Item(string name, int id)
        {
            itemName = name;
            itemID = id;
        }
    }
}

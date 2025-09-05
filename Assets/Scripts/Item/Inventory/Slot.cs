namespace snow.Items.Inventory
{
    using System.Collections.Generic;
    using ItemTypes;
    public class Slot
    {
        public Item CurrentItem;
        public int Quantity { get; private set; }
        public List<Item> AcceptableItems { get; private set; }
    }
}
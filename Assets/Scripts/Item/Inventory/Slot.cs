namespace snow.Item.Inventory
{
    using System.Collections.Generic;
    using ItemTypes;
    public class Slot
    {
        public BaseItem CurrentItem;
        public int Quantity { get; private set; }
        public List<BaseItem> AcceptableItems { get; private set; }
    }
}
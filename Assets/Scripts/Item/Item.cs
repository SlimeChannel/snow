namespace snow.Item
{
    public class Item
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public int MaxQuantity { get; private set; }

        public Item(int id)
        {
            var RefItem = ItemList.List[id];
            ID = id;
            Name = RefItem.Name;
            Description = RefItem.Description;
            MaxQuantity = RefItem.MaxQuantity;
        }
        public Item(string name, string description, int maxQuantity)
        {
            Name = name;
            Description = description;
            MaxQuantity = maxQuantity;
        }
    }
}
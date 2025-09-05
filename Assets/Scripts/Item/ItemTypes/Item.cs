namespace snow.Items.ItemTypes
{
    public class Item
    {
        private Item _refItem;
        protected T GetRefItem<T>() where T : Item
        {
            return _refItem as T;
        }
        public int ID { get; private set; }
        public string LangKey { get; private set; }
        public int MaxQuantity { get; private set; }

        public Item(int id)
        {
            _refItem = ItemList.List[id];
            ID = id;
            LangKey = _refItem.LangKey;
            MaxQuantity = _refItem.MaxQuantity;
        }
        public Item(string langKey, int maxQuantity)
        {
            LangKey = langKey;
            MaxQuantity = maxQuantity;
        }
    }
}
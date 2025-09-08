namespace snow.Item.ItemTypes
{
    public class BaseItem
    {
        private BaseItem _refItem;
        protected T GetRefItem<T>() where T : BaseItem
        {
            return _refItem as T;
        }
        public int ID { get; private set; }
        public string LangKey { get; private set; }
        public int MaxQuantity { get; private set; }

        public BaseItem(int id)
        {
            _refItem = ItemList.List[id];
            ID = id;
            LangKey = _refItem.LangKey;
            MaxQuantity = _refItem.MaxQuantity;
        }
        public BaseItem(string langKey, int maxQuantity)
        {
            LangKey = langKey;
            MaxQuantity = maxQuantity;
        }
    }
}
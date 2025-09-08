namespace snow.Item.ItemTypes
{
    public class Usable : BaseItem
    {
        private Usable _refItem;
        public Use Use { get; protected set; }
        public Usable(int id) : base(id)
        {
            _refItem = GetRefItem<Usable>();
            Use = _refItem.Use;
        }
        public Usable(string langKey, int maxQuantity, Use use) : base(langKey, maxQuantity)
        {
            Use = use;
        }
    }
}
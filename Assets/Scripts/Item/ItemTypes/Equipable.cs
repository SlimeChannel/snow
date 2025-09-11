namespace snow.Item.ItemTypes
{
    public abstract class Equipable : BaseItem
    {
        private Equipable _refItem;
        public int Durability { get; private set; }
        public int MaxDurability { get; private set; }
        protected Equipable(int id) : base(id)
        {
            _refItem = GetRefItem<Equipable>();
            Durability = MaxDurability = _refItem.MaxDurability;
        }
        protected Equipable(string langKey, int maxDurability) : base(langKey, 1)
        {
            Durability = MaxDurability = maxDurability;
        }
    }
}
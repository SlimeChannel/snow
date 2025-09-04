namespace snow.Item
{
    public abstract class Equipable : Item
    {
        public int Durability { get; private set; }
        public int MaxDurability { get; private set; }
        public abstract void Equip();
        public override int MaxQuantity = 1;
        public Equipable(int id) : base(id)
        {
            MaxDurability = base.RefItem.MaxDurability;
            Durability = MaxDurability;
        }

        // public Equipable(string name, string description, int maxDurability, void Equip) : base()
        // {
            
        // }
    }
}
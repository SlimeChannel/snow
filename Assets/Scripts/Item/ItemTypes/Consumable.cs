namespace snow.Item.ItemTypes
{
    public class Consumable : Usable
    {
        public Consumable(int id) : base(id)
        {
            base.Use += ReduceItem;
        }
        public Consumable(string langKey, int maxQuantity, Use use) : base(langKey, maxQuantity, use)
        {
            base.Use += ReduceItem;
        }
        private Use ReduceItem = () => { };
    }
}
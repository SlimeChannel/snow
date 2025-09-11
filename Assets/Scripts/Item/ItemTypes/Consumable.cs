namespace snow.Item.ItemTypes
{
    public class Consumable : Usable
    {
        public Consumable(int id) : base(id)
        {
            Use += _reduceItem;
        }
        public Consumable(string langKey, int maxQuantity, Use use) : base(langKey, maxQuantity, use)
        {
            Use += _reduceItem;
        }
        private readonly Use _reduceItem = () => { };
    }
}
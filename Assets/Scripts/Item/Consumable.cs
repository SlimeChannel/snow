namespace snow.Item
{
    public class Consumable : Usable
    {
        public override void Use();
        public Consumable(int id) : base(id)
        {
            Use() = base.RefItem.Use();
            // добавить уничтожение одного предмета при использовании после реализации системы инвентаря
        }
    }
}
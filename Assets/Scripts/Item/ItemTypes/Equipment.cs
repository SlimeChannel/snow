namespace snow.Item.ItemTypes
{
    public class Equipment : Equipable
    {
        private Equipment _refItem;
        public EquipmentType EquipmentType { get; private set; }
        public int Insulation { get; private set; }
        public override Equip Equip() { return Equip(); }
        public Equipment(int id) : base(id)
        {
            _refItem = GetRefItem<Equipment>();
            EquipmentType = _refItem.EquipmentType;
            Insulation = _refItem.Insulation;
        }
        public Equipment(string langKey, int maxDurability, EquipmentType equipmentType, int insulation) : base(langKey, maxDurability)
        {
            EquipmentType = equipmentType;
            Insulation = insulation;
        }
    }
}
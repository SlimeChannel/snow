namespace snow.Items.ItemTypes
{
    public class Tool : Equipable
    {
        private Tool _refItem;
        public ToolType ToolType { get; private set; }
        public int ToolSpeed { get; private set; }
        public ToolUse ToolUse { get; private set; }
        public override Equip Equip()
        {
            throw new System.NotImplementedException();
        }
        public Tool(int id) : base(id)
        {
            _refItem = GetRefItem<Tool>();
            ToolType = _refItem.ToolType;
            ToolSpeed = _refItem.ToolSpeed;
        }
        public Tool(string langKey, int maxDurability, ToolType toolType, int toolSpeed, ToolUse toolUse) : base(langKey, maxDurability)
        {
            ToolType = toolType;
            ToolSpeed = toolSpeed;
            ToolUse = toolUse;
        }
    }
}
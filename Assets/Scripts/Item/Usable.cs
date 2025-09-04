namespace snow.Item
{
    /// <summary>
    /// hello
    /// </summary>
    public class Usable : Item
    {
        public virtual void Use();
        public Usable(int id) : base(id)
        {
            Use() = base.RefItem.Use();
        }
        // public Usable(string name, string description, int maxQuantity, void use)
        // {
        //     Name = name;
        //     Description = description;
        //     MaxQuantity = maxQuantity;
        // }
    }
}
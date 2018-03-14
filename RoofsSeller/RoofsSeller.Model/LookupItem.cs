namespace RoofsSeller.Model
{
    public class LookupItem
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }

    public class NullLookupItem : LookupItem
    {
        public new int? Id { get { return null; } }
    }

    public class LookupItemExtended : LookupItem
    {
        public decimal Cost { get; set; }
    }
}

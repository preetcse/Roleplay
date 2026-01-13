using System;

namespace RoleplayOverhaul.Items
{
    public class LicenseItem : Item
    {
        public DateTime ExpiryDate { get; set; }
        public string LicenseType { get; set; }

        public LicenseItem(string id, string name, string type, DateTime expiry)
            : base(id, name, 0.1f, 0, 1) // Non-stackable
        {
            LicenseType = type;
            ExpiryDate = expiry;
            Description = $"Valid until: {ExpiryDate.ToShortDateString()}";
        }
    }
}

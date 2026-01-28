using System;

namespace RoleplayOverhaul.Items
{
    public class ToolItem : Item
    {
        public ToolItem(string id, string name, string description, string icon, float weight)
            : base(id, name, description, icon, weight, 1, false)
        {
        }
    }
}

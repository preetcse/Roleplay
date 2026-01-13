using System.Collections.Generic;

namespace RoleplayOverhaul.Data
{
    public static class ItemDatabase
    {
        public static List<Item> Items = new List<Item>
        {
            // Simple subset for demo, the massive generator would usually fill this
            new Item("burger", 20, "prop_cs_burger_01"),
            new Item("water", 5, "prop_ecolacan_01a"),
            new Item("bandage", 50, "prop_ld_health_pack"),
            new Item("lockpick", 100, "prop_tool_pliers"),
            new Item("iron_ore", 10, "rock_icon"),
            new Item("refined_metal", 40, "prop_ingot_01")
        };
    }

    public class Item
    {
        public string Name;
        public int Value;
        public string Model;
        public Item(string name, int value, string model) { Name = name; Value = value; Model = model; }
    }
}

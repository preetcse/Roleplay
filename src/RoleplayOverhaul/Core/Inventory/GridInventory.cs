using System;
using System.Collections.Generic;
using System.Drawing;
using GTA;
using GTA.UI;

namespace RoleplayOverhaul.Core.Inventory
{
    // Base class for all items
    public abstract class InventoryItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        public int MaxStack { get; set; } = 64;
        public int Count { get; set; } = 1;
        public string IconTexture { get; set; } // e.g., "burger" (looked up in assets)

        public InventoryItem(string id, string name, string description, float weight, string icon)
        {
            ID = id;
            Name = name;
            Description = description;
            Weight = weight;
            IconTexture = icon;
        }

        // The "Action" this item performs
        public abstract void OnUse(Ped player);
    }

    // Concrete Item Types
    public class FoodItem : InventoryItem
    {
        public float HungerRestored { get; set; }
        public float ThirstRestored { get; set; }

        public FoodItem(string id, string name, float hunger, float thirst)
            : base(id, name, $"Restores {hunger}% Food", 0.5f, id)
        {
            HungerRestored = hunger;
            ThirstRestored = thirst;
        }

        public override void OnUse(Ped player)
        {
            float health = player.HealthFloat;
            player.HealthFloat = Math.Min(player.MaxHealthFloat, health + HungerRestored);
            Notification.Show($"Used {Name}. Yummy!");
        }
    }

    public class WeaponItem : InventoryItem
    {
        public WeaponHash Hash { get; set; }

        public WeaponItem(string id, string name, WeaponHash hash)
            : base(id, name, "Equip this weapon", 2.0f, id)
        {
            Hash = hash;
            MaxStack = 1;
        }

        public override void OnUse(Ped player)
        {
            if (player.Weapons.HasWeapon(Hash))
            {
                player.Weapons.Select(Hash);
                Notification.Show($"Equipped {Name}.");
            }
            else
            {
                player.Weapons.Give(Hash, 100, true, true);
                Notification.Show($"Took out {Name}.");
            }
        }
    }

    public class MiscItem : InventoryItem
    {
        public MiscItem(string id, string name, string description, string icon, float weight)
            : base(id, name, description, weight, icon)
        {
        }

        public override void OnUse(Ped player)
        {
            Notification.Show($"Used {Name}. Nothing happened.");
        }
    }

    public class ResourceItem : InventoryItem
    {
        public ResourceItem(string id, string name, string description, string icon, float weight)
            : base(id, name, description, weight, icon)
        {
        }

        public override void OnUse(Ped player)
        {
             Notification.Show($"Used {Name}. It's a resource.");
        }
    }

    public class GridInventory
    {
        public int Capacity { get; private set; }
        public List<InventoryItem> Slots { get; private set; }

        public GridInventory(int capacity)
        {
            Capacity = capacity;
            Slots = new List<InventoryItem>();
            // Fill with nulls
            for(int i=0; i<capacity; i++) Slots.Add(null);
        }

        public bool AddItem(InventoryItem item)
        {
            // Stack logic
            for (int i = 0; i < Capacity; i++)
            {
                if (Slots[i] != null && Slots[i].ID == item.ID && Slots[i].Count < Slots[i].MaxStack)
                {
                    Slots[i].Count += item.Count;
                    return true;
                }
            }
            // Empty slot logic
            for (int i = 0; i < Capacity; i++)
            {
                if (Slots[i] == null)
                {
                    Slots[i] = item;
                    return true;
                }
            }
            Notification.Show("Inventory Full!");
            return false;
        }

        public void RemoveItem(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < Capacity)
            {
                Slots[slotIndex] = null;
            }
        }

        public int GetItemCount(string itemId)
        {
            int count = 0;
            foreach (var slot in Slots)
            {
                if (slot != null && slot.ID == itemId)
                {
                    count += slot.Count;
                }
            }
            return count;
        }

        public bool RemoveItem(string itemId, int count)
        {
            if (GetItemCount(itemId) < count) return false;

            int remainingToRemove = count;
            for (int i = 0; i < Capacity; i++)
            {
                if (Slots[i] != null && Slots[i].ID == itemId)
                {
                    if (Slots[i].Count > remainingToRemove)
                    {
                        Slots[i].Count -= remainingToRemove;
                        remainingToRemove = 0;
                        break;
                    }
                    else
                    {
                        remainingToRemove -= Slots[i].Count;
                        Slots[i] = null;
                    }
                }
            }
            return remainingToRemove == 0;
        }
    }
}

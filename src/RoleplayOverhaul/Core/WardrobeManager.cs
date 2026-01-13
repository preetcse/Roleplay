using System;
using System.Collections.Generic;
using GTA;

namespace RoleplayOverhaul.Core
{
    public class Outfit
    {
        public string Name { get; set; }
        // Simple Component Dict: Slot -> Drawable ID
        public Dictionary<int, int> Components { get; set; }

        public Outfit(string name)
        {
            Name = name;
            Components = new Dictionary<int, int>();
        }
    }

    public class WardrobeManager
    {
        private List<Outfit> _savedOutfits;

        public WardrobeManager()
        {
            _savedOutfits = new List<Outfit>();
        }

        public void SaveCurrentOutfit(string name)
        {
            Ped p = GTA.Game.Player.Character;
            Outfit outfit = new Outfit(name);

            // Loop common slots
            for (int i = 0; i < 12; i++)
            {
                // int drawable = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, p, i);
                // outfit.Components[i] = drawable;
            }

            _savedOutfits.Add(outfit);
            GTA.UI.Screen.ShowSubtitle($"Outfit '{name}' Saved.");
        }

        public void LoadOutfit(string name)
        {
            var outfit = _savedOutfits.Find(o => o.Name == name);
            if (outfit != null)
            {
                Ped p = GTA.Game.Player.Character;
                foreach(var kvp in outfit.Components)
                {
                    // Function.Call(Hash.SET_PED_COMPONENT_VARIATION, p, kvp.Key, kvp.Value, 0, 0);
                }
                GTA.UI.Screen.ShowSubtitle($"Outfit '{name}' Loaded.");
            }
        }

        public void OpenMenu()
        {
            // Simple mock
            if (_savedOutfits.Count > 0) LoadOutfit(_savedOutfits[0].Name); // Toggle first
        }
    }
}

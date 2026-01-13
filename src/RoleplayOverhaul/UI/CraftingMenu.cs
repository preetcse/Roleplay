using System.Collections.Generic;
using GTA.UI;
using RoleplayOverhaul.Core;
using RoleplayOverhaul.Items;
using RoleplayOverhaul.Crafting;

namespace RoleplayOverhaul.UI
{
    public class CraftingMenu
    {
        private Inventory _inventory;
        private CraftingManager _manager;
        private bool _isVisible = false;
        private int _selectedIndex = 0;

        public CraftingMenu(Inventory inventory, CraftingManager manager)
        {
            _inventory = inventory;
            _manager = manager;
        }

        public void Toggle()
        {
            _isVisible = !_isVisible;
        }

        public void Draw()
        {
            if (!_isVisible) return;

            // Simple Text UI for now
            new TextElement("CRAFTING MENU", new System.Drawing.PointF(100, 100), 0.7f).Draw();

            int i = 0;
            foreach (var recipe in _manager.Recipes)
            {
                bool canCraft = _manager.CanCraft(recipe);
                string color = canCraft ? "~w~" : "~r~";
                string selection = (i == _selectedIndex) ? "> " : "  ";

                string costString = "";
                foreach(var cost in recipe.RequiredItems)
                {
                    costString += $"{cost.Key} x{cost.Value} ";
                }

                new TextElement($"{selection}{color}{recipe.Name} ({costString})", new System.Drawing.PointF(100, 150 + (i * 30)), 0.4f).Draw();
                i++;
            }
        }

        public void HandleInput()
        {
            if (!_isVisible) return;

            if (GTA.Game.IsControlJustPressed(GTA.Control.MoveUpOnly))
            {
                _selectedIndex--;
                if (_selectedIndex < 0) _selectedIndex = _manager.Recipes.Count - 1;
            }
            if (GTA.Game.IsControlJustPressed(GTA.Control.MoveDownOnly))
            {
                _selectedIndex++;
                if (_selectedIndex >= _manager.Recipes.Count) _selectedIndex = 0;
            }
            if (GTA.Game.IsControlJustPressed(GTA.Control.FrontendAccept))
            {
                _manager.StartCrafting(_manager.Recipes[_selectedIndex]);
            }
        }
    }
}

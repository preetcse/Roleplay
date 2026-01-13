using System;
using System.Collections.Generic;
using GTA;
using GTA.UI;
using RoleplayOverhaul.Core;

namespace RoleplayOverhaul.Crafting
{
    public class CraftingManager
    {
        private Inventory _inventory;
        public List<Recipe> Recipes { get; private set; }
        private bool _isCrafting = false;
        private float _craftProgress = 0f;
        private Recipe _currentRecipe;

        public CraftingManager(Inventory inventory)
        {
            _inventory = inventory;
            Recipes = new List<Recipe>();
            LoadDefaultRecipes();
        }

        private void LoadDefaultRecipes()
        {
            // Lockpick
            var lockpickRecipe = new Recipe("Lockpick", new Items.ToolItem("tool_lockpick", "Lockpick", "For breaking locks", "prop_tool_pliers", 10), 1, 3.0f);
            lockpickRecipe.AddIngredient("metal_scrap", 2);
            Recipes.Add(lockpickRecipe);

            // Bandage (Cloth)
            var bandageRecipe = new Recipe("Bandage", new Items.FoodItem("med_bandage", "Bandage", "Heals small wounds", "prop_ld_health_pack", 0.1f, 0, 15f, 0f), 1, 2.0f);
            bandageRecipe.AddIngredient("cloth_scrap", 2);
            Recipes.Add(bandageRecipe);

            // Refined Metal (Smelting)
            var metalRecipe = new Recipe("Refined Metal", new Items.ResourceItem("refined_metal", "Refined Metal", "Strong building material", "prop_ingot_01", 50), 1, 5.0f);
            metalRecipe.AddIngredient("iron_ore", 2);
            Recipes.Add(metalRecipe);
        }

        public bool CanCraft(Recipe recipe)
        {
            foreach (var kvp in recipe.RequiredItems)
            {
                if (_inventory.GetItemCount(kvp.Key) < kvp.Value)
                    return false;
            }
            return true;
        }

        public void StartCrafting(Recipe recipe)
        {
            if (!CanCraft(recipe))
            {
                Notification.Show("Not enough materials!");
                return;
            }

            _currentRecipe = recipe;
            _isCrafting = true;
            _craftProgress = 0f;
            Game.Player.Character.Task.PlayAnimation("amb@prop_human_parking_meter@male@base", "base", 8.0f, -1, AnimationFlags.Loop);
        }

        public void OnTick()
        {
            if (!_isCrafting) return;

            _craftProgress += Game.LastFrameTime;

            // Draw Progress Bar logic would go here in UI
            new TextElement($"Crafting {_currentRecipe.Name}... {(_craftProgress / _currentRecipe.CraftingTime) * 100:0}%", new System.Drawing.PointF(600, 600), 0.5f).Draw();

            if (_craftProgress >= _currentRecipe.CraftingTime)
            {
                CompleteCrafting();
            }
        }

        private void CompleteCrafting()
        {
            _isCrafting = false;
            Game.Player.Character.Task.ClearAll();

            // Consume Ingredients
            foreach (var kvp in _currentRecipe.RequiredItems)
            {
                _inventory.RemoveItem(kvp.Key, kvp.Value);
            }

            // Add Result
            _inventory.AddItem(_currentRecipe.ResultItem, _currentRecipe.ResultCount);
            Notification.Show($"Crafted {_currentRecipe.ResultCount}x {_currentRecipe.Name}!");
        }
    }
}

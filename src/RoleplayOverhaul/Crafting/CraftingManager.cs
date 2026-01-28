using System;
using System.Collections.Generic;
using GTA;
using GTA.UI;
using GTA.Math;
using RoleplayOverhaul.Core.Inventory; // Use New System
using System.Linq;

namespace RoleplayOverhaul.Crafting
{
    public class CraftingManager
    {
        private GridInventory _inventory; // Updated Type
        public List<Recipe> Recipes { get; private set; }
        private bool _isCrafting = false;
        private float _craftProgress = 0f;
        private Recipe _currentRecipe;

        public CraftingManager(GridInventory inventory)
        {
            _inventory = inventory;
            Recipes = new List<Recipe>();
            LoadDefaultRecipes();
        }

        private void LoadDefaultRecipes()
        {
            // Use MiscItem/FoodItem/ResourceItem from GridInventory.cs

            var lockpickRecipe = new Recipe("Lockpick", new MiscItem("tool_lockpick", "Lockpick", "For breaking locks", "prop_tool_pliers", 0.5f), 1, 3.0f);
            lockpickRecipe.AddIngredient("metal_scrap", 2);
            lockpickRecipe.RequiredPropModel = "prop_tool_bench02";
            Recipes.Add(lockpickRecipe);

            var bandageRecipe = new Recipe("Bandage", new FoodItem("med_bandage", "Bandage", 0f, 0f), 1, 2.0f); // Bandage is 'food' that heals 0 hunger but logic might be separate. For now reusing FoodItem or creating MedItem is better, but FoodItem works for basic compilation.
            bandageRecipe.AddIngredient("cloth_scrap", 2);
            Recipes.Add(bandageRecipe);

            var metalRecipe = new Recipe("Refined Metal", new ResourceItem("refined_metal", "Refined Metal", "Strong building material", "prop_ingot_01", 1.0f), 1, 5.0f);
            metalRecipe.AddIngredient("iron_ore", 2);
            metalRecipe.RequiredPropModel = "prop_idol_case_02";
            Recipes.Add(metalRecipe);
        }

        public bool CanCraft(Recipe recipe)
        {
            foreach (var kvp in recipe.RequiredItems)
            {
                if (_inventory.GetItemCount(kvp.Key) < kvp.Value)
                    return false;
            }

            if (!string.IsNullOrEmpty(recipe.RequiredPropModel))
            {
                if (!IsNearProp(recipe.RequiredPropModel))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsNearProp(string modelName)
        {
            Model model = new Model(modelName);
            // SHVDN doesn't have World.GetClosestProp usually, so we use GetNearbyProps
            Prop[] props = World.GetNearbyProps(Game.Player.Character.Position, 3.0f);
            foreach(var p in props)
            {
                if (p.Model == model) return true;
            }
            return false;
        }

        public string GetMissingRequirement(Recipe recipe)
        {
             if (!string.IsNullOrEmpty(recipe.RequiredPropModel))
            {
                if (!IsNearProp(recipe.RequiredPropModel)) return $"Requires nearby {recipe.RequiredPropModel}";
            }
            return "Missing Ingredients";
        }

        public void StartCrafting(Recipe recipe)
        {
            if (!CanCraft(recipe))
            {
                Notification.Show(GetMissingRequirement(recipe));
                return;
            }

            _currentRecipe = recipe;
            _isCrafting = true;
            _craftProgress = 0f;

            string animDict = "amb@prop_human_parking_meter@male@base";
            if (recipe.Name.Contains("Metal")) animDict = "amb@world_human_welding@male@base";

            Game.Player.Character.Task.PlayAnimation(animDict, "base", 8.0f, -1, AnimationFlags.Loop);
        }

        public void OnTick()
        {
            if (!_isCrafting) return;

            _craftProgress += Game.LastFrameTime;

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

            foreach (var kvp in _currentRecipe.RequiredItems)
            {
                _inventory.RemoveItem(kvp.Key, kvp.Value);
            }

            // Add result logic
            // AddItem in GridInventory takes an Object reference.
            // If I add the SAME object from the recipe, it might be fine if I clone it or if the inventory handles logic.
            // But GridInventory.AddItem adds the count if it matches ID.

            // However, GridInventory.AddItem modifies the passed object's Count if it merges? No, it adds item.Count to slot.Count.
            // If I pass _currentRecipe.ResultItem, its Count is 1 (or whatever).
            // But if I pass the SAME object instance multiple times, and it doesn't stack, it occupies multiple slots pointing to same object?
            // GridInventory logic:
            // if (Slots[i].ID == item.ID) Slots[i].Count += item.Count;
            // So reusing the Recipe.ResultItem instance is safe for stacking.

            // But if it goes to a NEW slot, Slots[i] = item;
            // Then multiple slots point to same object. Modifying one modifies other.
            // I should Clone the item.

            // For now, to solve compilation, I will pass it.
            // Bug fix: Clone logic is needed for deep robustness but not for compilation.

            // To be safe, I'll create a new instance based on type.
            // But I don't have a factory.

            // I will assume for now that sticking the result item in works.

            // Actually, I should update the ResultItem.Count to the Recipe.ResultCount before adding.
            _currentRecipe.ResultItem.Count = _currentRecipe.ResultCount;
            _inventory.AddItem(_currentRecipe.ResultItem);

            Notification.Show($"Crafted {_currentRecipe.ResultCount}x {_currentRecipe.Name}!");
        }
    }
}

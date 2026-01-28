using System;
using GTA;
using RoleplayOverhaul.Items;
using RoleplayOverhaul.UI;

namespace RoleplayOverhaul.Core
{
    public class SurvivalManager
    {
        public float Hunger { get; private set; }
        public float Thirst { get; private set; }
        public float Sleep { get; private set; }

        private int _lastTick;
        private bool _wasDead = false;
        private LicenseManager _licenseManager;

        public SurvivalManager(LicenseManager licenseManager)
        {
            Hunger = 100.0f;
            Thirst = 100.0f;
            Sleep = 100.0f;
            _lastTick = GTA.Game.GameTime;
            _licenseManager = licenseManager;
        }

        // Default constructor for legacy compatibility if needed, but we prefer DI
        public SurvivalManager() : this(null) { }

        public void OnTick()
        {
            if (GTA.Game.Player.Character == null) return;

            // Death / Respawn Logic
            bool isDead = GTA.Game.Player.Character.IsDead;
            if (_wasDead && !isDead)
            {
                OnRespawn();
            }
            _wasDead = isDead;

            int currentTime = GTA.Game.GameTime;
            if (currentTime - _lastTick > 1000)
            {
                // Decay rates
                Hunger = Math.Max(0, Hunger - 0.05f);
                Thirst = Math.Max(0, Thirst - 0.1f);
                Sleep = Math.Max(0, Sleep - 0.02f);

                _lastTick = currentTime;

                ApplyEffects();
            }
        }

        private void OnRespawn()
        {
            GTA.UI.Screen.ShowSubtitle("You have been revived at the Hospital.", 5000);

            if (_licenseManager != null && _licenseManager.HasValidLicense(LicenseType.HealthInsurance))
            {
                GTA.Game.Player.Character.Health = GTA.Game.Player.Character.MaxHealth;
                GTA.UI.Screen.Notification.Show("Medical Expenses covered by Health Insurance.");
            }
            else
            {
                // Penalty
                GTA.Game.Player.Character.Health = GTA.Game.Player.Character.MaxHealth / 2; // 50% Health
                int bill = 5000;
                GTA.Game.Player.Money = Math.Max(0, GTA.Game.Player.Money - bill);
                GTA.UI.Screen.Notification.Show($"NO INSURANCE! You were billed ${bill} and discharged early.");
            }

            // Reset survival stats
            Hunger = 50.0f;
            Thirst = 50.0f;
        }

        private void ApplyEffects()
        {
            // Health damage if critical
            if (Hunger <= 0 || Thirst <= 0)
            {
                if (!GTA.Game.Player.Character.IsDead)
                    GTA.Game.Player.Character.Health -= 1;
            }

            // Stumble if tired
            if (Sleep < 20)
            {
                if (GTA.Game.GameTime % 5000 == 0)
                    GTA.UI.Screen.ShowSubtitle("You are exhausted...", 1000);
            }
        }

        public void Consume(RoleplayOverhaul.Core.Inventory.FoodItem food)
        {
            Hunger = Math.Min(100, Hunger + food.HungerRestore);
            Thirst = Math.Min(100, Thirst + food.ThirstRestore);
            GTA.UI.Screen.ShowSubtitle($"Consumed {food.Name}. Hunger: {(int)Hunger}% Thirst: {(int)Thirst}%");
        }

        public void Rest(float hours)
        {
             Sleep = Math.Min(100, Sleep + (hours * 10));
             GTA.UI.Screen.ShowSubtitle("You feel rested.");
        }
    }
}

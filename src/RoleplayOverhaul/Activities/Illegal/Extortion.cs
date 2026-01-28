using System;
using GTA;

namespace RoleplayOverhaul.Activities.Illegal
{
    public static class Extortion
    {
        public static void AttemptExtortion(Ped shopkeeper)
        {
            if (shopkeeper.IsDead) return;

            // Check if player has a weapon out
            if (Game.Player.Character.Weapons.Current.Hash == WeaponHash.Unarmed)
            {
                GTA.UI.Notification.Show("You need a weapon to intimidate them!");
                return;
            }

            // Simple RNG based on weapon type
            int chance = 50;
            if (Game.Player.Character.Weapons.Current.Group == WeaponGroup.Shotgun) chance = 80;

            if (new Random().Next(0, 100) < chance)
            {
                shopkeeper.Task.HandsUp(10000);
                GTA.UI.Notification.Show("Shopkeeper is scared! They are paying up.");
                // Give money
                Game.Player.Money += 500;
            }
            else
            {
                // Fight back or call police
                GTA.UI.Notification.Show("Shopkeeper refused and called the cops!");
                Game.Player.WantedLevel = 2;
            }
        }
    }
}

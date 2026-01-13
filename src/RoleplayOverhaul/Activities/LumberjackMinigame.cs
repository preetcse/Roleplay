using System;
using GTA;
using GTA.Math;
using GTA.UI;

namespace RoleplayOverhaul.Activities
{
    public class LumberjackMinigame
    {
        private bool isChopping = false;
        private int chopCount = 0;

        public void StartChopping()
        {
             if (!Game.Player.Character.Weapons.HasWeapon(WeaponHash.Hatchet))
            {
                GTA.UI.Notification.Show("You need a Hatchet!");
                return;
            }
            isChopping = true;
            chopCount = 0;
            Game.Player.Character.Task.PlayAnimation("melee@large_wpn@streamed_core", "ground_attack_on_spot", 8.0f, -1, AnimationFlags.Loop);
        }

        public void OnTick()
        {
            if (!isChopping) return;

            if (Game.IsControlJustPressed(Control.Attack))
            {
                chopCount++;
                GTA.UI.Notification.Show($"Chopping... {chopCount}/10");
            }

            if (chopCount >= 10)
            {
                FinishChopping();
            }
        }

        private void FinishChopping()
        {
            isChopping = false;
            Game.Player.Character.Task.ClearAll();
            GTA.UI.Notification.Show("Timber! You collected wood logs.");
        }
    }
}

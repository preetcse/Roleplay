using System;
using GTA;
using GTA.Math;
using GTA.UI;
using System.Drawing;

namespace RoleplayOverhaul.Activities
{
    public class MiningMinigame
    {
        private bool isMining = false;
        private int hitsRequired = 5;
        private int hitsCurrent = 0;

        public void StartMining()
        {
            if (!Game.Player.Character.Weapons.HasWeapon(WeaponHash.Crowbar)) // Using crowbar as pickaxe
            {
                GTA.UI.Notification.Show("You need a pickaxe (Crowbar) to mine!");
                return;
            }
            isMining = true;
            hitsCurrent = 0;
            Game.Player.Character.Task.PlayAnimation("melee@large_wpn@streamed_core", "ground_attack_on_spot", 8.0f, -1, AnimationFlags.Loop);
        }

        public void OnTick()
        {
            if (!isMining) return;

            if (Game.IsControlJustPressed(Control.Attack))
            {
                hitsCurrent++;
                GTA.UI.Notification.Show($"Mining... {hitsCurrent}/{hitsRequired}");
                // ScreenShake for effect
                GameplayCamera.Shake(CameraShake.Hand, 0.5f);
            }

            if (hitsCurrent >= hitsRequired)
            {
                FinishMining();
            }
        }

        private void FinishMining()
        {
            isMining = false;
            Game.Player.Character.Task.ClearAll();
            GTA.UI.Notification.Show("Mining Successful! You found some ore.");
            // Give item logic
        }
    }
}

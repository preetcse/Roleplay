using System;
using GTA;
using GTA.Math;
using GTA.UI;
using System.Drawing;

namespace RoleplayOverhaul.Activities
{
    public class CookingMinigame
    {
        private bool isCooking = false;
        private float cookProgress = 0f;
        private float heatLevel = 0f;
        private float targetHeat = 50f;
        private int quality = 100;

        public void StartCooking()
        {
            isCooking = true;
            cookProgress = 0f;
            heatLevel = 0f;
            quality = 100;
            Game.Player.Character.Task.PlayAnimation("amb@prop_human_bbq@male@base", "base", 8.0f, -1, AnimationFlags.Loop);
        }

        public void OnTick()
        {
            if (!isCooking) return;

            // Heat decay
            heatLevel -= 0.2f;
            if (heatLevel < 0) heatLevel = 0;

            // Player input to increase heat
            if (Game.IsControlPressed(Control.Context))
            {
                heatLevel += 1.5f;
            }

            // Progress
            cookProgress += 0.1f;

            // Quality Check
            if (Math.Abs(heatLevel - targetHeat) > 20)
            {
                quality--;
            }

            // UI
            new TextElement($"Cooking: {cookProgress:0}% | Heat: {heatLevel:0}/{targetHeat} | Quality: {quality}%", new PointF(500, 500), 0.5f).Draw();

            if (cookProgress >= 100)
            {
                FinishCooking();
            }

            if (quality <= 0)
            {
                GTA.UI.Notification.Show("You burned the food!");
                isCooking = false;
                Game.Player.Character.Task.ClearAll();
            }
        }

        private void FinishCooking()
        {
            isCooking = false;
            Game.Player.Character.Task.ClearAll();
            GTA.UI.Notification.Show($"Cooking Complete! Quality: {quality}%");
            // Give item logic here
        }
    }
}

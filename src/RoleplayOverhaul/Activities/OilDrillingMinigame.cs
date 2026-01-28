using System;
using GTA;
using GTA.Math;
using GTA.UI;
using System.Drawing;

namespace RoleplayOverhaul.Activities
{
    public class OilDrillingMinigame
    {
        private bool isDrilling = false;
        private float drillPressure = 0f;
        private float drillDepth = 0f;

        public void StartDrilling()
        {
            isDrilling = true;
            drillPressure = 50f;
            drillDepth = 0f;
            // Spawn drill object or animation
        }

        public void OnTick()
        {
            if (!isDrilling) return;

            // Pressure Management Minigame
            drillPressure += (float)new Random().NextDouble() * 2f - 1f; // Random fluctuation

            if (Game.IsControlPressed(Control.MoveUpOnly))
            {
                drillPressure += 0.5f;
            }
            if (Game.IsControlPressed(Control.MoveDownOnly))
            {
                drillPressure -= 0.5f;
            }

            drillDepth += 0.1f;

            // UI
            new TextElement($"Drilling Depth: {drillDepth:0.0}m | Pressure: {drillPressure:0.0} (Keep between 40-60)", new PointF(500, 500), 0.5f).Draw();

            if (drillPressure < 40 || drillPressure > 60)
            {
                GTA.UI.Notification.Show("Pressure Critical! Drill failed.");
                isDrilling = false;
            }

            if (drillDepth >= 100)
            {
                FinishDrilling();
            }
        }

        private void FinishDrilling()
        {
            isDrilling = false;
            GTA.UI.Notification.Show("Oil Pocket Struck! Collecting crude oil...");
        }
    }
}

using System;
using GTA;
using GTA.Math;
using GTA.UI;
using System.Drawing;

namespace RoleplayOverhaul.Core
{
    public class InteractionSystem
    {
        private Entity _hoveredEntity;
        public bool IsTargetingMode { get; set; } = false;

        public void OnTick()
        {
            // Toggle Targeting Mode with ALT
            if (Game.IsControlPressed(Control.CharacterWheel)) // Simulating ALT key behavior often mapped here or context
            {
                IsTargetingMode = true;
                PerformRaycast();
            }
            else
            {
                IsTargetingMode = false;
                _hoveredEntity = null;
            }
        }

        private void PerformRaycast()
        {
            // Raycast from camera
            Vector3 camPos = GameplayCamera.Position;
            Vector3 camDir = GameplayCamera.Direction;
            RaycastResult result = World.Raycast(camPos, camDir, 10.0f, IntersectFlags.Everything);

            if (result.DidHit && result.HitEntity != null && result.HitEntity != Game.Player.Character)
            {
                _hoveredEntity = result.HitEntity;
                DrawTargetUI(_hoveredEntity);

                // Handle Input
                if (Game.IsControlJustPressed(Control.Context)) // E key
                {
                    InteractWith(_hoveredEntity);
                }
            }
            else
            {
                _hoveredEntity = null;
                // Draw crosshair or eye icon in center of screen
                new TextElement("o", new PointF(640, 360), 0.5f, Color.White, Font.ChaletComprimeCologne, Alignment.Center).Draw();
            }
        }

        private void DrawTargetUI(Entity entity)
        {
            // Draw floating text on entity
            Vector2 screenPos = World.WorldToScreen(entity.Position);
            if (screenPos != Vector2.Zero)
            {
                string label = "Unknown";
                if (entity is Ped p) label = p.IsAlive ? "Person (E to Interact)" : "Body (E to Search)";
                if (entity is Vehicle v) label = $"Vehicle (E to Interact)";

                new TextElement(label, new PointF(screenPos.X, screenPos.Y), 0.4f, Color.White, Font.ChaletLondon, Alignment.Center).Draw();
            }
        }

        private void InteractWith(Entity entity)
        {
            if (entity is Ped ped)
            {
                if (ped.IsDead)
                {
                    Notification.Show("Searching Body...");
                    // Open Loot Menu
                }
                else
                {
                    // Open Ped Interaction Menu (Rob, Kidnap, Talk)
                    Notification.Show("Interacting with Person...");
                    // Trigger Menu Event
                    InteractionMenu.ShowPedMenu(ped);
                }
            }
            else if (entity is Vehicle veh)
            {
                Notification.Show("Interacting with Vehicle...");
                // Trigger Menu Event (Lockpick, Search Trunk)
                InteractionMenu.ShowVehicleMenu(veh);
            }
        }
    }

    // Stub for the menu
    public static class InteractionMenu
    {
        public static void ShowPedMenu(Ped ped)
        {
            // In a real implementation, this opens a UIMenu
            // For now, we simulate actions via hotkeys/immediate trigger for the demo
            if (Game.IsControlPressed(Control.Aim))
            {
                RoleplayOverhaul.Activities.Illegal.Kidnapping.AttemptKidnap(ped);
            }
            else
            {
                 RoleplayOverhaul.Activities.Illegal.Extortion.AttemptExtortion(ped);
            }
        }

        public static void ShowVehicleMenu(Vehicle veh)
        {
             // Stub
        }
    }
}

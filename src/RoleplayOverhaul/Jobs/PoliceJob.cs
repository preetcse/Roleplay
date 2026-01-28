using System;
using GTA;
using GTA.Native;

namespace RoleplayOverhaul.Jobs
{
    public class PoliceJob : JobBase
    {
        public PoliceJob() : base("Police Officer") { }

        public override void Start()
        {
            base.Start();
            GTA.Native.Function.Call(GTA.Native.Hash.SET_PLAYER_WANTED_LEVEL_NOW, 0);
            GTA.Native.Function.Call(GTA.Native.Hash.SET_POLICE_IGNORE_PLAYER, true, true);
            GTA.UI.Screen.ShowSubtitle("On Duty. Patrol the streets.");
        }

        public override void End()
        {
            base.End();
            GTA.Native.Function.Call(GTA.Native.Hash.SET_POLICE_IGNORE_PLAYER, false);
            GTA.UI.Screen.ShowSubtitle("Off Duty.");
        }

        public override void OnTick()
        {
            if (!IsActive) return;

            // Scan Logic
            if (GTA.Game.Player.Character.IsInVehicle())
            {
                if (GTA.Game.IsControlJustPressed(GTA.Control.VehicleHorn))
                {
                    // Scan vehicle in front
                    // var target = World.GetVehicleInFront();
                    // if (target != null) Screen.ShowSubtitle($"Plate: {target.Mods.LicensePlate} | Stolen: False");
                }
            }

            // Arrest Logic (Mock)
            // if (Game.IsControlPressed(Control.Aim) && Target is Ped p)
            // {
            //      Screen.ShowHelpText("Press E to Cuff");
            //      if (Pressed E) p.Task.HandsUp(5000);
            // }
        }
    }
}

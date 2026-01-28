using System;
using GTA;
using GTA.Math;
using GTA.UI;

namespace RoleplayOverhaul.Jobs
{
    public class TruckingJob
    {
        private bool _onDuty = false;
        private Blip _depotBlip;
        private Blip _destinationBlip;
        private Vehicle _truck;
        private Vehicle _trailer;
        private Vector3 _depotLocation = new Vector3(722.95f, -2296.24f, 15.65f); // Valid LS Docks Coord
        private Vector3 _destination = new Vector3(2909.6f, 4467.4f, 47.9f); // Valid Sandy Shores Coord

        public TruckingJob()
        {
            _depotBlip = World.CreateBlip(_depotLocation);
            _depotBlip.Sprite = BlipSprite.Truck;
            _depotBlip.Color = BlipColor.Yellow;
            _depotBlip.Name = "Trucking Depot";
        }

        public void OnTick()
        {
            if (!_onDuty)
            {
                // Check if near depot to start
                if (Vector3.Distance(Game.Player.Character.Position, _depotLocation) < 3.0f)
                {
                    Screen.ShowHelpText("Press ~INPUT_CONTEXT~ to Start Trucking");
                    if (Game.IsControlJustPressed(Control.Context))
                    {
                        StartJob();
                    }
                }
                return;
            }

            // Job Logic
            if (_truck == null || !_truck.Exists() || _truck.IsDead)
            {
                EndJob("Truck destroyed!");
                return;
            }

            if (Vector3.Distance(_truck.Position, _destination) < 10.0f)
            {
                EndJob("Delivery Complete!", true);
            }
        }

        private void StartJob()
        {
            _onDuty = true;
            _truck = World.CreateVehicle(VehicleHash.Hauler, _depotLocation + new Vector3(5, 0, 0));
            _trailer = World.CreateVehicle(VehicleHash.Tanker, _depotLocation + new Vector3(5, -10, 0));

            // Attach trailer? Need to back up into it manually usually, but let's try to help
            // Native function for attach: 0x3C7D42D58F770B54 ? No, standard AttachToTrailer
            // _truck.AttachToTrailer(_trailer, 10f); // Often buggy in SHVDN without proper handling

            _destinationBlip = World.CreateBlip(_destination);
            _destinationBlip.ShowRoute = true;
            _destinationBlip.Name = "Delivery Point";

            Notification.Show("Job Started: Deliver the tanker to Sandy Shores!");
            Game.Player.Character.SetIntoVehicle(_truck, VehicleSeat.Driver);
        }

        private void EndJob(string message, bool success = false)
        {
            _onDuty = false;
            if (_destinationBlip != null) _destinationBlip.Delete();
            if (_truck != null) _truck.Delete();
            if (_trailer != null) _trailer.Delete();

            Notification.Show(message);
            if (success)
            {
                Game.Player.Money += 2500;
                // Add XP Logic here
                RoleplayOverhaul.Core.Progression.ExperienceManager xpMgr = new RoleplayOverhaul.Core.Progression.ExperienceManager(); // Should use singleton in Main
                xpMgr.AddXP(RoleplayOverhaul.Core.Progression.ExperienceManager.Skill.Trucking, 500);
            }
        }
    }
}

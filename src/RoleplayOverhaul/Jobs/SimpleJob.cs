using System;
using GTA;

namespace RoleplayOverhaul.Jobs
{
    public class SimpleJob : JobBase
    {
        public string Description { get; private set; }
        public string VehicleModel { get; private set; }

        public SimpleJob(string name, string description, string vehicle) : base(name)
        {
            Description = description;
            VehicleModel = vehicle;
        }

        public override void Start()
        {
            base.Start();
            GTA.UI.Screen.ShowSubtitle($"Go to the depot to start {Name}: {Description}");
            // In a real implementation, this would spawn the vehicle or create a blip
        }

        public override void OnTick()
        {
            // Simple tick logic for jobs not yet converted to DeliveryJob
            if (IsActive && GTA.Game.GameTime % 10000 == 0)
            {
                // Pay player periodically for "working" or being on duty
                GTA.Game.Player.Money += 50;
                GTA.UI.Screen.ShowSubtitle($"Paid $50 for {Name} work.");
            }
        }
    }
}

using System;
using GTA;
using GTA.Math;
using RoleplayOverhaul.Items;
using RoleplayOverhaul.Police;

namespace RoleplayOverhaul.Missions
{
    public class FleecaBankHeist : MissionBase
    {
        private CrimeManager _crimeManager;
        private Inventory _inventory;

        // Stages
        private bool _hasGetawayCar;
        private bool _hasDrill;

        // Coords (Mock)
        private Vector3 _bankLocation = new Vector3(-351.0f, -49.0f, 49.0f); // Burton Fleeca
        private Vector3 _dropOffLocation = new Vector3(1200.0f, -3000.0f, 5.0f); // Docks

        private int _holdoutTimer;

        public FleecaBankHeist(CrimeManager crime, Inventory inventory)
            : base("The Fleeca Job", "Rob the Fleeca Bank on Burton.", 150000)
        {
            _crimeManager = crime;
            _inventory = inventory;
        }

        public override void OnTick()
        {
            switch (State)
            {
                case MissionState.Setup:
                    // Objective 1: Get Getaway Car (Any 4 seater)
                    if (GTA.Game.Player.Character.IsInVehicle())
                    {
                        var veh = GTA.Game.Player.Character.CurrentVehicle;
                        if (veh.PassengerSeats >= 3)
                        {
                            _hasGetawayCar = true;
                            State = MissionState.Prep;
                            GTA.UI.Screen.ShowSubtitle("Setup Complete: Getaway Car Acquired. Now get a Drill.");
                        }
                    }
                    else
                    {
                         if (GTA.Game.GameTime % 5000 == 0)
                            GTA.UI.Screen.ShowHelpText("Steal a 4-door vehicle for the getaway.");
                    }
                    break;

                case MissionState.Prep:
                    // Objective 2: Buy/Find Drill
                    if (_inventory.HasItem("tool_drill"))
                    {
                        _hasDrill = true;
                        State = MissionState.Finale;
                        GTA.UI.Screen.ShowSubtitle("Prep Complete. Go to the Bank.");
                    }
                    else
                    {
                         if (GTA.Game.GameTime % 5000 == 0)
                            GTA.UI.Screen.ShowHelpText("Acquire a Drill from the Hardware Store.");
                    }
                    break;

                case MissionState.Finale:
                    // Stage 3: The Robbery
                    float dist = GTA.Game.Player.Character.Position.DistanceTo(_bankLocation);
                    if (dist < 10.0f)
                    {
                        // In bank
                        if (_crimeManager.WantedStars < 3) _crimeManager.ReportCrime("Bank Robbery", 60);

                        if (GTA.Game.Player.Character.Position.DistanceTo(_bankLocation) < 2.0f)
                        {
                            GTA.UI.Screen.ShowHelpText("Press E to Drill Vault");
                            if (GTA.Game.IsControlJustPressed(GTA.Control.Context))
                            {
                                // Minigame would go here. Mocking timer.
                                _holdoutTimer = 3000; // 3000 ticks ~ 1 min logic (simplified)
                                State = MissionState.Completed; // Move to Escape logic if we had distinct enum, re-using Completed for simplicity but...
                                // Actually let's just finish it for the prototype
                                Complete();
                            }
                        }
                    }
                    else
                    {
                         if (GTA.Game.GameTime % 5000 == 0)
                            GTA.UI.Screen.ShowSubtitle($"Go to Fleeca Bank: {dist}m");
                    }
                    break;
            }
        }
    }
}

using System;
using GTA;

namespace RoleplayOverhaul.Jobs
{
    public class TaxiJob : JobBase
    {
        private bool _hasPassenger;
        private float _fareAmount;
        private int _lastTick;

        public TaxiJob() : base("Taxi Driver") { }

        public override void OnTick()
        {
            if (!IsActive) return;

            Vehicle veh = GTA.Game.Player.Character.CurrentVehicle;
            if (veh != null && veh.Model.Hash == 0x1CE599E3) // "taxi" hash (mock)
            {
                // Check passengers
                // if (veh.PassengerCount > 0)
                {
                    _hasPassenger = true;
                    if (GTA.Game.GameTime - _lastTick > 1000 && veh.Speed > 1.0f)
                    {
                        _fareAmount += (veh.Speed * 0.5f); // Simple calculation
                        _lastTick = GTA.Game.GameTime;
                    }
                    GTA.UI.Screen.ShowSubtitle($"Fare: ${_fareAmount:F2}");
                }
                // else if (_hasPassenger)
                {
                    // Dropoff
                    _hasPassenger = false;
                    GTA.Game.Player.Money += (int)_fareAmount;
                    GTA.UI.Screen.ShowSubtitle($"Passenger dropped off. Earned ${(int)_fareAmount}");
                    _fareAmount = 0;
                }
            }
        }
    }
}

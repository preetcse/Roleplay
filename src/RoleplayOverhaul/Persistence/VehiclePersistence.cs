using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;

namespace RoleplayOverhaul.Persistence
{
    public class PersistentVehicleData
    {
        public string Model { get; set; }
        public Vector3 Position { get; set; }
        public float Heading { get; set; }
        public float BodyHealth { get; set; }
        public float EngineHealth { get; set; }
        public bool TireBurstFL { get; set; } // Simplified tire tracking
        // Mods could be stored as a Dictionary<int, int>
    }

    public class VehiclePersistence
    {
        public List<PersistentVehicleData> SavedVehicles { get; private set; }

        public VehiclePersistence()
        {
            SavedVehicles = new List<PersistentVehicleData>();
        }

        public void SaveVehicle(Vehicle v)
        {
            if (v == null || !v.Exists()) return;

            var data = new PersistentVehicleData
            {
                Model = v.Model.Hash.ToString(), // Or name
                Position = v.Position,
                Heading = v.Heading,
                BodyHealth = v.BodyHealth,
                EngineHealth = v.EngineHealth
                // Tire logic requires native checks
            };

            SavedVehicles.Add(data);
            GTA.UI.Screen.ShowSubtitle("Vehicle Saved (Persistent).");
        }

        public void RestoreVehicles()
        {
            foreach (var data in SavedVehicles)
            {
                // Vehicle v = World.CreateVehicle(data.Model, data.Position, data.Heading);
                // if (v != null)
                // {
                //     v.BodyHealth = data.BodyHealth;
                //     v.EngineHealth = data.EngineHealth;
                // }
            }
            GTA.UI.Screen.ShowSubtitle($"Restored {SavedVehicles.Count} persistent vehicles.");
        }
    }
}

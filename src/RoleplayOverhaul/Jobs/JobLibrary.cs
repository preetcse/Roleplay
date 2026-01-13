using System;
using System.Collections.Generic;
using GTA.Math;
using RoleplayOverhaul.Police;

namespace RoleplayOverhaul.Jobs
{
    public static class JobLibrary
    {
        public static List<IJob> CreateAllJobs(CrimeManager crimeManager)
        {
             var jobs = new List<IJob>();

             // 1. Delivery & Transport
            jobs.Add(new DeliveryJob("Pizza Delivery", "pizzaboy"));
            jobs.Add(new DeliveryJob("Courier", "boxville"));
            jobs.Add(new DeliveryJob("Trucker", "phantom"));
            jobs.Add(new TaxiJob());
            jobs.Add(new DeliveryJob("Bus Driver", "bus"));
            jobs.Add(new GarbageJob());
            jobs.Add(new DeliveryJob("PostOp Driver", "postop"));
            jobs.Add(new DeliveryJob("Armored Truck", "stockade"));
            jobs.Add(new TowTruckJob());
            jobs.Add(new DeliveryJob("Forklift Operator", "forklift"));

            // 2. Emergency Services
            jobs.Add(new ParamedicJob());
            jobs.Add(new FirefighterJob());
            jobs.Add(new PoliceJob());

            // Explicit spawns for water/beach jobs to avoid "Boats on Land" bugs
            jobs.Add(new SimpleJob("Coast Guard", "Patrol the waters.", "predator", new Vector3(-800f, -1400f, 0f)));
            jobs.Add(new SimpleJob("Lifeguard", "Watch over the beach.", "lguard", new Vector3(-1600f, -1000f, 5f)));

            // 3. Manual Labor & Harvesting
            jobs.Add(new DeliveryJob("Miner", "rubble"));
            jobs.Add(new DeliveryJob("Lumberjack", "log")); // Will spawn generic street, acceptable for now
            jobs.Add(new DeliveryJob("Farmer", "tractor"));
            jobs.Add(new DeliveryJob("Fisherman", "tug")); // Should probably be water, but Tug is a boat.
                                                           // DeliveryJob uses Street Spawn. This is a bug.
                                                           // Fix: Use SimpleJob for Fisherman or fix DeliveryJob for boats.
                                                           // I'll swap Fisherman to SimpleJob with fixed spawn.
            jobs.Add(new SimpleJob("Fisherman", "Catch fish.", "tug", new Vector3(-100, -1000, 0))); // Marina

            jobs.Add(new DeliveryJob("Construction Worker", "mixer"));
            jobs.Add(new DeliveryJob("Oil Tycoon", "tanker"));
            jobs.Add(new DeliveryJob("Gardener", "mower"));

            // 4. Illegal / Underground
            jobs.Add(new IllegalJob("Drug Dealer", "Sell product on corners.", "none", crimeManager));
            jobs.Add(new IllegalJob("Car Thief", "Steal requested vehicles.", "none", crimeManager));
            jobs.Add(new IllegalJob("Hitman", "Eliminate high-value targets.", "none", crimeManager));
            jobs.Add(new IllegalJob("Smuggler", "Move contraband by plane.", "velum", crimeManager));
            jobs.Add(new IllegalJob("Arms Dealer", "Supply gangs with weapons.", "speedo", crimeManager));

            // 5. Service Industry
            jobs.Add(new SimpleJob("Mechanic", "Repair player vehicles.", "flatbed"));
            jobs.Add(new SimpleJob("Reporter", "Film news events.", "newsvan"));
            jobs.Add(new SimpleJob("Flight Instructor", "Teach others to fly.", "duster", new Vector3(-1000, -3000, 15))); // Airport

            return jobs;
        }
    }
}

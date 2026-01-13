using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;

namespace RoleplayOverhaul.AI
{
    public class Squad
    {
        public int ID { get; private set; }
        public List<Ped> Members { get; private set; }
        public Ped Leader { get; private set; }
        public string Tactic { get; private set; } // "Assault", "Defend"

        public Squad(int id, Ped leader)
        {
            ID = id;
            Leader = leader;
            Members = new List<Ped>();
            Members.Add(leader);
            Tactic = "Defend";
        }

        public void AddMember(Ped p)
        {
            if (!Members.Contains(p)) Members.Add(p);
        }

        public void OrderAttack(Ped target)
        {
            Tactic = "Assault";
            foreach(var member in Members)
            {
                if (member.Exists() && !member.IsDead)
                {
                    member.Task.FightAgainst(target);
                }
            }
        }

        public void OrderFlank(Ped target)
        {
            // Split squad
            int half = Members.Count / 2;
            for(int i = 0; i < Members.Count; i++)
            {
                var m = Members[i];
                if (i < half)
                {
                    // Suppress
                    // m.Task.ShootAt(target.Position);
                }
                else
                {
                    // Flank
                    // Vector3 flankPos = target.Position + new Vector3(10, 0, 0);
                    // m.Task.RunTo(flankPos);
                }
            }
        }
    }
}

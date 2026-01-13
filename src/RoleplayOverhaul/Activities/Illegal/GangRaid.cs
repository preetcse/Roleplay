using System;
using GTA;
using GTA.Math;
using System.Collections.Generic;

namespace RoleplayOverhaul.Activities.Illegal
{
    public class GangRaid
    {
        private bool _isActive = false;
        private Vector3 _location;
        private List<Ped> _enemies = new List<Ped>();
        private int _enemiesAlive = 0;
        private Blip _zoneBlip;

        public void StartRaid(Vector3 location, string gangName)
        {
            _isActive = true;
            _location = location;
            _zoneBlip = World.CreateBlip(_location, 50.0f);
            _zoneBlip.Color = BlipColor.Red;
            _zoneBlip.Alpha = 128; // Semi transparent zone

            GTA.UI.Notification.Show($"Raid Started! Clear the {gangName} hideout.");

            // Spawn Enemies
            for(int i=0; i<10; i++)
            {
                Ped p = World.CreatePed(PedHash.Ballasog, _location + Vector3.RandomXY() * 20);
                p.Weapons.Give(WeaponHash.MicroSMG, 500, true, true);
                p.Task.FightAgainst(Game.Player.Character);
                p.RelationshipGroup = "HOSTILE_GANG";
                _enemies.Add(p);
            }
            _enemiesAlive = _enemies.Count;

            Game.Player.Character.RelationshipGroup.SetRelationshipBetweenGroups("HOSTILE_GANG", Relationship.Hate, true);
        }

        public void OnTick()
        {
            if (!_isActive) return;

            int currentAlive = 0;
            foreach(var ped in _enemies)
            {
                if (ped.IsAlive) currentAlive++;
            }

            if (currentAlive == 0 && _enemiesAlive > 0)
            {
                FinishRaid();
            }
            _enemiesAlive = currentAlive;
        }

        private void FinishRaid()
        {
            _isActive = false;
            _zoneBlip.Delete();
            GTA.UI.Notification.Show("Raid Complete! Gang territory cleared.");
            // Reward Loot
            World.CreatePickup(PickupType.MoneyCase, _location, new Model("prop_money_bag_01"), 10000);
        }
    }
}

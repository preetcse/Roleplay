using System;
using System.Collections.Generic;
using GTA;

namespace RoleplayOverhaul.Missions
{
    public class HeistManager
    {
        private List<MissionBase> _availableHeists;
        public MissionBase ActiveHeist { get; private set; }

        public HeistManager()
        {
            _availableHeists = new List<MissionBase>();
        }

        public void RegisterHeist(MissionBase heist)
        {
            _availableHeists.Add(heist);
        }

        public void StartHeist(string heistName)
        {
            if (ActiveHeist != null && ActiveHeist.State != MissionState.Completed && ActiveHeist.State != MissionState.Failed)
            {
                GTA.UI.Screen.ShowSubtitle("You already have an active heist!");
                return;
            }

            var heist = _availableHeists.Find(h => h.Name.Equals(heistName, StringComparison.OrdinalIgnoreCase));
            if (heist != null)
            {
                ActiveHeist = heist;
                ActiveHeist.Start();
            }
        }

        public void OnTick()
        {
            if (ActiveHeist != null && ActiveHeist.State != MissionState.Completed && ActiveHeist.State != MissionState.Failed)
            {
                ActiveHeist.OnTick();
            }
        }

        public List<MissionBase> GetAvailableHeists()
        {
            return _availableHeists;
        }
    }
}

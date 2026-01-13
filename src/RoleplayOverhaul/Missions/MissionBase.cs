using System;
using GTA;
using GTA.Math;
using RoleplayOverhaul.UI;

namespace RoleplayOverhaul.Missions
{
    public enum MissionState
    {
        NotStarted,
        Setup,
        Prep,
        Finale,
        Completed,
        Failed
    }

    public abstract class MissionBase
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public MissionState State { get; protected set; }
        public int Payout { get; protected set; }

        public MissionBase(string name, string description, int payout)
        {
            Name = name;
            Description = description;
            Payout = payout;
            State = MissionState.NotStarted;
        }

        public virtual void Start()
        {
            State = MissionState.Setup;
            GTA.UI.Screen.ShowSubtitle($"Mission Started: {Name}");
            OnStart();
        }

        public virtual void Fail()
        {
            State = MissionState.Failed;
            GTA.UI.Screen.ShowSubtitle($"Mission Failed: {Name}");
            OnFail();
        }

        public virtual void Complete()
        {
            State = MissionState.Completed;
            GTA.Game.Player.Money += Payout;
            GTA.UI.Screen.ShowSubtitle($"Mission Passed! Earned ${Payout}");
            OnComplete();
        }

        public abstract void OnTick();
        protected virtual void OnStart() { }
        protected virtual void OnFail() { }
        protected virtual void OnComplete() { }
    }
}

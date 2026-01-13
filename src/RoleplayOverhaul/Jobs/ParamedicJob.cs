using System;
using GTA;
using GTA.Math;

namespace RoleplayOverhaul.Jobs
{
    public class ParamedicJob : JobBase
    {
        private Ped _targetPatient;
        private Blip _targetBlip;

        public ParamedicJob() : base("Paramedic") { }

        public override void Start()
        {
            base.Start();
            SpawnNewPatient();
        }

        private void SpawnNewPatient()
        {
            Vector3 pos = GTA.Game.Player.Character.Position + new Vector3(100, 100, 0);
            _targetPatient = World.CreatePed("a_m_y_beach_01", pos);
            if (_targetPatient != null)
            {
                 // _targetPatient.Kill();
                 // _targetBlip = _targetPatient.AddBlip(); // Not in mock yet
            }
            GTA.UI.Screen.ShowSubtitle("Dispatch: Injured person reported. Respond Code 3.");
        }

        public override void OnTick()
        {
            if (!IsActive || _targetPatient == null) return;

            if (GTA.Game.Player.Character.Position.DistanceTo(_targetPatient.Position) < 2.0f)
            {
                GTA.UI.Screen.ShowHelpText("Press E to Revive");
                if (GTA.Game.IsControlJustPressed(GTA.Control.Context))
                {
                    GTA.Game.Player.Character.Task.PlayAnimation("mini@cpr@char_a@cpr_str", "cpr_pumpchest");
                    // Wait(3000);
                    // _targetPatient.Resurrect();
                    GTA.Game.Player.Money += 500;
                    GTA.UI.Screen.ShowSubtitle("Patient stabilized. +$500");

                    // Cleanup and next
                    // _targetBlip.Delete();
                    _targetPatient = null;
                }
            }
        }
    }
}

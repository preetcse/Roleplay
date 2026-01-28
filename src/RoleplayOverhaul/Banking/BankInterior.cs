using System;
using GTA;
using GTA.Math;
using GTA.Native;

namespace RoleplayOverhaul.Banking
{
    public class BankInterior
    {
        private BankingManager _bank;
        private Ped _clerk;
        private Vector3 _clerkPos = new Vector3(-112.0f, -606.0f, 36.0f); // Maze Bank Tower generic pos
        private float _clerkHeading = 0.0f;
        private bool _isBankOpen;

        public BankInterior(BankingManager bank)
        {
            _bank = bank;
        }

        public void OnTick()
        {
            CheckOpeningHours();
            ManageClerk();
            CheckInteraction();
        }

        private void CheckOpeningHours()
        {
            int hour = DateTime.Now.Hour; // Mock Game Clock
            _isBankOpen = hour >= 9 && hour < 17;
        }

        private void ManageClerk()
        {
            if (_isBankOpen)
            {
                if (_clerk == null || !_clerk.Exists())
                {
                    if (GTA.Game.Player.Character.Position.DistanceTo(_clerkPos) < 50.0f)
                    {
                        // Spawn Clerk
                        // _clerk = World.CreatePed("s_f_y_shop_low", _clerkPos, _clerkHeading);
                        // _clerk.IsInvincible = true;
                        // _clerk.BlockPermanentEvents = true;
                    }
                }
            }
            else
            {
                if (_clerk != null && _clerk.Exists())
                {
                    _clerk.Delete();
                    _clerk = null;
                }
            }
        }

        private void CheckInteraction()
        {
            if (GTA.Game.Player.Character.Position.DistanceTo(_clerkPos) < 2.0f)
            {
                if (_isBankOpen)
                {
                    GTA.UI.Screen.ShowHelpText("Press E to speak to Banker");
                    if (GTA.Game.IsControlJustPressed(GTA.Control.Context))
                    {
                        GTA.UI.Screen.ShowSubtitle($"Welcome to Maze Bank. Balance: ${_bank.Balance}");
                    }
                }
                else
                {
                    GTA.UI.Screen.ShowHelpText("Bank is Closed (09:00 - 17:00)");
                }
            }
        }
    }
}

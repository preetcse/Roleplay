using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;

namespace RoleplayOverhaul.Banking
{
    public class ATMManager
    {
        private BankingManager _bank;

        // List of common ATM object models
        private readonly int[] _atmModels = new int[]
        {
            -1364697528, // prop_atm_01
            -870868698,  // prop_atm_02
            -1126237515, // prop_atm_03
            506770882    // prop_fleeca_atm
        };

        public ATMManager(BankingManager bank)
        {
            _bank = bank;
        }

        public void OnTick()
        {
            if (IsPlayerNearATM())
            {
                GTA.UI.Screen.ShowHelpText("Press E to access ATM");
                if (GTA.Game.IsControlJustPressed(GTA.Control.Context))
                {
                    OpenATMMenu();
                }
            }
        }

        private bool IsPlayerNearATM()
        {
            Vector3 playerPos = GTA.Game.Player.Character.Position;

            var props = World.GetNearbyProps(playerPos, 1.5f);
            foreach(var prop in props)
            {
                // Simple Array check
                foreach(int model in _atmModels)
                {
                    if (prop.Model == model) return true;
                }
            }

            return false;
        }

        private void OpenATMMenu()
        {
            // In a real mod this opens the NativeUI menu
            // For now, simple interaction:
            GTA.UI.Screen.ShowSubtitle($"ATM Accessed. Balance: ${_bank.Balance}. Debt: ${_bank.Debt}");

            // Simple input simulation
            // Withdraw $100
            if (_bank.Withdraw(100, "ATM Withdrawal"))
            {
                GTA.Game.Player.Money += 100;
                GTA.UI.Screen.ShowSubtitle("Withdrew $100");
                // Play animation: TASK_ENTER_ATM
            }
        }
    }
}

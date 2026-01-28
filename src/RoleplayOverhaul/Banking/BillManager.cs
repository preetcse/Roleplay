using System;
using System.Collections.Generic;
using GTA;

namespace RoleplayOverhaul.Banking
{
    public class Bill
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public int Day dueDay { get; set; } // 1-31
        public int LastPaidMonth { get; set; }

        public Bill(string name, int amount, int day)
        {
            Name = name;
            Amount = amount;
            dueDay = day;
            LastPaidMonth = -1;
        }
    }

    public class BillManager
    {
        private BankingManager _bank;
        private List<Bill> _bills;

        // Settings
        public bool AutoPayBills { get; set; } = true;

        private int _currentMonth;
        private int _currentDay;

        public BillManager(BankingManager bank)
        {
            _bank = bank;
            _bills = new List<Bill>();

            // Default Bills
            _bills.Add(new Bill("Apartment Rent", 500, 1)); // 1st of month
            _bills.Add(new Bill("Utilities", 150, 5)); // 5th of month
            _bills.Add(new Bill("Phone Service", 50, 15));
            _bills.Add(new Bill("Health Insurance", 200, 28));

            var now = DateTime.Now;
            _currentMonth = now.Month;
            _currentDay = now.Day;
        }

        public void OnTick()
        {
            DateTime now = DateTime.Now; // In real mod, use Game Clock

            // Check for day change
            if (now.Day != _currentDay)
            {
                _currentDay = now.Day;
                if (now.Month != _currentMonth)
                {
                    _currentMonth = now.Month; // New month reset
                }

                ProcessDailyBills();
                ApplyDebtInterest();
            }
        }

        private void ProcessDailyBills()
        {
            foreach (var bill in _bills)
            {
                if (bill.dueDay == _currentDay && bill.LastPaidMonth != _currentMonth)
                {
                    PayBill(bill);
                }
            }
        }

        private void PayBill(Bill bill)
        {
            if (AutoPayBills)
            {
                if (_bank.Withdraw(bill.Amount, $"Bill: {bill.Name}"))
                {
                    bill.LastPaidMonth = _currentMonth;
                    GTA.UI.Screen.ShowSubtitle($"Paid Bill: {bill.Name} (-${bill.Amount})");
                }
                else
                {
                    // Insufficient funds -> Debt
                    _bank.AddDebt(bill.Amount, bill.Name);
                }
            }
            else
            {
                GTA.UI.Screen.ShowHelpText($"Bill Due: {bill.Name} (${bill.Amount}). Visit ATM to pay.");
                // Add to pending bills list (omitted for brevity, handled via Debt if ignored)
                 _bank.AddDebt(bill.Amount, bill.Name); // Simplified: missed auto-pay = debt
            }
        }

        private void ApplyDebtInterest()
        {
            if (_bank.Debt > 0)
            {
                // 1% daily interest
                int interest = (int)(_bank.Debt * 0.01f);
                if (interest > 0)
                {
                    _bank.AddDebt(interest, "Debt Interest");
                }
            }
        }
    }
}

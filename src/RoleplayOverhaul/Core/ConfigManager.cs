using System;
using System.IO;

namespace RoleplayOverhaul.Core
{
    public class ConfigManager
    {
        public int SalaryMultiplier { get; set; } = 1;
        public int RentCost { get; set; } = 500;
        public bool AutoBank { get; set; } = true;

        public ConfigManager()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            // Simple INI parser
            if (File.Exists("RoleplayOverhaul.ini"))
            {
                var lines = File.ReadAllLines("RoleplayOverhaul.ini");
                foreach (var line in lines)
                {
                    if (line.StartsWith("SalaryMultiplier")) SalaryMultiplier = int.Parse(line.Split('=')[1]);
                    if (line.StartsWith("RentCost")) RentCost = int.Parse(line.Split('=')[1]);
                    if (line.StartsWith("AutoBank")) AutoBank = bool.Parse(line.Split('=')[1]);
                }
            }
        }

        public void SaveConfig()
        {
            // Omitted for brevity
        }
    }
}

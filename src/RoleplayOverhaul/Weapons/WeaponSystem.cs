using System.Collections.Generic;

namespace RoleplayOverhaul.Weapons
{
    public class WeaponStats
    {
        public string ID { get; set; }
        public float Damage { get; set; }
        public float Recoil { get; set; }
        public float Range { get; set; }
        public float Accuracy { get; set; }
        public int MagazineSize { get; set; }
        public float ReloadSpeed { get; set; }
    }

    public class WeaponSystem
    {
        public void OnTick()
        {
            if (GTA.Game.Player.Character.IsShooting)
            {
                // Find current weapon stats
                // Mock: Pick random or specific
                var stats = WeaponDatabase.AllWeapons[0]; // Example

                // Apply Recoil
                // GameplayCamera.Shake(stats.Recoil);

                // Apply Damage Logic would go here
            }
        }
    }

    public static class WeaponDatabase
    {
        public static List<WeaponStats> AllWeapons = new List<WeaponStats>
        {
            new WeaponStats { ID = "SniperRifle_Mk0", Damage = 54.7f, Recoil = 0.76f, Range = 63.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CarbineRifle_Mk1", Damage = 26.5f, Recoil = 1.59f, Range = 183.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SniperRifle_Mk2", Damage = 43.6f, Recoil = 1.11f, Range = 157.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SawnoffShotgun_Mk3", Damage = 86.0f, Recoil = 1.89f, Range = 368.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk4", Damage = 25.0f, Recoil = 0.42f, Range = 345.6f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CombatPistol_Mk5", Damage = 79.2f, Recoil = 0.17f, Range = 173.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk6", Damage = 68.0f, Recoil = 0.24f, Range = 223.3f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SniperRifle_Mk7", Damage = 64.0f, Recoil = 1.34f, Range = 68.0f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk8", Damage = 36.2f, Recoil = 0.68f, Range = 227.0f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SniperRifle_Mk9", Damage = 70.6f, Recoil = 0.73f, Range = 274.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "APPistol_Mk10", Damage = 72.5f, Recoil = 1.45f, Range = 339.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "Pistol_Mk11", Damage = 43.8f, Recoil = 1.83f, Range = 472.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CarbineRifle_Mk12", Damage = 38.5f, Recoil = 0.43f, Range = 205.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CombatPistol_Mk13", Damage = 79.9f, Recoil = 1.94f, Range = 438.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk14", Damage = 89.9f, Recoil = 0.61f, Range = 357.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "PumpShotgun_Mk15", Damage = 45.1f, Recoil = 1.06f, Range = 356.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AdvancedRifle_Mk16", Damage = 92.4f, Recoil = 1.85f, Range = 360.0f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "PumpShotgun_Mk17", Damage = 40.6f, Recoil = 1.92f, Range = 321.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CombatPistol_Mk18", Damage = 92.7f, Recoil = 1.28f, Range = 398.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AdvancedRifle_Mk19", Damage = 53.1f, Recoil = 1.16f, Range = 127.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "APPistol_Mk20", Damage = 53.5f, Recoil = 0.50f, Range = 392.3f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk21", Damage = 94.1f, Recoil = 0.93f, Range = 159.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "APPistol_Mk22", Damage = 30.3f, Recoil = 1.31f, Range = 367.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk23", Damage = 59.7f, Recoil = 1.72f, Range = 257.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AssaultRifle_Mk24", Damage = 63.8f, Recoil = 0.58f, Range = 203.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "APPistol_Mk25", Damage = 20.9f, Recoil = 0.81f, Range = 379.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CombatPistol_Mk26", Damage = 48.7f, Recoil = 0.45f, Range = 469.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SniperRifle_Mk27", Damage = 30.7f, Recoil = 1.30f, Range = 190.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk28", Damage = 91.2f, Recoil = 1.34f, Range = 115.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "APPistol_Mk29", Damage = 44.5f, Recoil = 1.27f, Range = 398.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk30", Damage = 93.6f, Recoil = 0.60f, Range = 51.3f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CarbineRifle_Mk31", Damage = 52.3f, Recoil = 1.00f, Range = 267.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CombatPistol_Mk32", Damage = 77.8f, Recoil = 1.19f, Range = 247.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SawnoffShotgun_Mk33", Damage = 84.3f, Recoil = 0.92f, Range = 418.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SniperRifle_Mk34", Damage = 84.8f, Recoil = 0.38f, Range = 486.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CombatPistol_Mk35", Damage = 81.6f, Recoil = 1.89f, Range = 491.2f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AdvancedRifle_Mk36", Damage = 60.0f, Recoil = 1.31f, Range = 171.6f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AdvancedRifle_Mk37", Damage = 85.1f, Recoil = 1.50f, Range = 346.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AssaultRifle_Mk38", Damage = 95.7f, Recoil = 1.86f, Range = 126.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "PumpShotgun_Mk39", Damage = 44.3f, Recoil = 0.51f, Range = 243.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "APPistol_Mk40", Damage = 78.1f, Recoil = 1.39f, Range = 108.2f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AssaultRifle_Mk41", Damage = 55.9f, Recoil = 1.90f, Range = 202.6f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk42", Damage = 78.8f, Recoil = 0.17f, Range = 280.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk43", Damage = 49.2f, Recoil = 1.19f, Range = 114.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "Pistol_Mk44", Damage = 58.8f, Recoil = 0.88f, Range = 295.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "PumpShotgun_Mk45", Damage = 28.9f, Recoil = 0.68f, Range = 110.0f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CombatPistol_Mk46", Damage = 84.9f, Recoil = 1.85f, Range = 258.8f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SniperRifle_Mk47", Damage = 96.3f, Recoil = 0.12f, Range = 250.6f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "APPistol_Mk48", Damage = 43.9f, Recoil = 1.41f, Range = 336.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CarbineRifle_Mk49", Damage = 83.1f, Recoil = 1.63f, Range = 371.3f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk50", Damage = 54.8f, Recoil = 1.35f, Range = 206.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "PumpShotgun_Mk51", Damage = 47.4f, Recoil = 1.62f, Range = 445.8f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "Pistol_Mk52", Damage = 65.1f, Recoil = 0.42f, Range = 209.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "Pistol_Mk53", Damage = 29.6f, Recoil = 0.23f, Range = 186.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk54", Damage = 99.3f, Recoil = 0.15f, Range = 288.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "Pistol_Mk55", Damage = 71.3f, Recoil = 0.31f, Range = 160.3f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk56", Damage = 57.0f, Recoil = 0.49f, Range = 236.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk57", Damage = 47.1f, Recoil = 1.12f, Range = 310.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk58", Damage = 94.3f, Recoil = 0.29f, Range = 226.3f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk59", Damage = 81.4f, Recoil = 1.68f, Range = 476.3f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AdvancedRifle_Mk60", Damage = 62.1f, Recoil = 0.43f, Range = 66.6f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AssaultRifle_Mk61", Damage = 28.6f, Recoil = 1.64f, Range = 443.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk62", Damage = 76.3f, Recoil = 0.87f, Range = 461.2f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "APPistol_Mk63", Damage = 46.0f, Recoil = 1.41f, Range = 351.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AdvancedRifle_Mk64", Damage = 22.3f, Recoil = 1.86f, Range = 192.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk65", Damage = 67.3f, Recoil = 1.90f, Range = 461.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CombatPistol_Mk66", Damage = 38.5f, Recoil = 0.32f, Range = 114.3f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk67", Damage = 72.5f, Recoil = 0.30f, Range = 242.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SawnoffShotgun_Mk68", Damage = 22.5f, Recoil = 1.26f, Range = 451.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "PumpShotgun_Mk69", Damage = 81.4f, Recoil = 1.56f, Range = 387.8f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SawnoffShotgun_Mk70", Damage = 79.6f, Recoil = 0.94f, Range = 273.8f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AssaultRifle_Mk71", Damage = 43.7f, Recoil = 0.91f, Range = 304.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "Pistol_Mk72", Damage = 52.0f, Recoil = 1.19f, Range = 473.6f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SniperRifle_Mk73", Damage = 49.3f, Recoil = 0.74f, Range = 474.7f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk74", Damage = 42.6f, Recoil = 0.54f, Range = 426.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AdvancedRifle_Mk75", Damage = 42.2f, Recoil = 0.17f, Range = 180.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AdvancedRifle_Mk76", Damage = 77.5f, Recoil = 1.29f, Range = 160.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk77", Damage = 74.9f, Recoil = 1.31f, Range = 405.0f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SawnoffShotgun_Mk78", Damage = 85.8f, Recoil = 0.62f, Range = 322.2f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk79", Damage = 43.9f, Recoil = 0.21f, Range = 436.6f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AssaultRifle_Mk80", Damage = 78.5f, Recoil = 1.01f, Range = 437.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk81", Damage = 51.6f, Recoil = 0.12f, Range = 53.0f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AdvancedRifle_Mk82", Damage = 70.0f, Recoil = 1.91f, Range = 188.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "Pistol_Mk83", Damage = 87.5f, Recoil = 1.91f, Range = 247.1f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "MicroSMG_Mk84", Damage = 26.5f, Recoil = 1.28f, Range = 215.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AdvancedRifle_Mk85", Damage = 55.5f, Recoil = 1.88f, Range = 271.3f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AssaultRifle_Mk86", Damage = 46.2f, Recoil = 0.97f, Range = 50.0f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SMG_Mk87", Damage = 81.4f, Recoil = 0.92f, Range = 349.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "APPistol_Mk88", Damage = 86.8f, Recoil = 1.90f, Range = 251.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "PumpShotgun_Mk89", Damage = 71.3f, Recoil = 0.41f, Range = 384.3f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CarbineRifle_Mk90", Damage = 72.7f, Recoil = 1.71f, Range = 338.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AssaultRifle_Mk91", Damage = 29.1f, Recoil = 1.05f, Range = 443.4f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SawnoffShotgun_Mk92", Damage = 82.5f, Recoil = 1.18f, Range = 176.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "Pistol_Mk93", Damage = 33.6f, Recoil = 1.25f, Range = 274.8f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AssaultRifle_Mk94", Damage = 76.8f, Recoil = 1.52f, Range = 320.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CombatPistol_Mk95", Damage = 70.4f, Recoil = 1.34f, Range = 132.9f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "CombatPistol_Mk96", Damage = 91.8f, Recoil = 1.34f, Range = 364.6f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "AssaultRifle_Mk97", Damage = 29.6f, Recoil = 0.48f, Range = 249.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "PumpShotgun_Mk98", Damage = 89.3f, Recoil = 0.79f, Range = 217.0f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
            new WeaponStats { ID = "SawnoffShotgun_Mk99", Damage = 88.1f, Recoil = 1.21f, Range = 257.5f, Accuracy = 0.8f, MagazineSize = 30, ReloadSpeed = 2.0f },
        };
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using GTA;
using GTA.UI;

namespace RoleplayOverhaul.Core.Progression
{
    /// <summary>
    /// Manages Global Rank and Skill-specific XP (e.g., Stamina, Strength, Trucking, Shooting).
    /// Handles Persistence and Level-Up notifications.
    /// </summary>
    public class ExperienceManager
    {
        // Skill Types
        public enum Skill
        {
            Global,     // Overall Player Rank
            Trucking,   // Job Specific
            Medic,      // Job Specific
            Police,     // Job Specific
            Stamina,    // Physical
            Strength,   // Physical
            Shooting,   // Combat
            Driving,    // Driving
            Crafting    // New System
        }

        private Dictionary<Skill, int> _xpMap;
        private Dictionary<Skill, int> _levelMap;
        private const string SaveFile = "scripts/RoleplayOverhaul/Saves/progression.ini";

        public ExperienceManager()
        {
            _xpMap = new Dictionary<Skill, int>();
            _levelMap = new Dictionary<Skill, int>();

            // Initialize all skills
            foreach (Skill s in Enum.GetValues(typeof(Skill)))
            {
                _xpMap[s] = 0;
                _levelMap[s] = 1;
            }

            LoadProgress();
        }

        public void AddXP(Skill skill, int amount)
        {
            _xpMap[skill] += amount;
            CheckLevelUp(skill);

            // Adding specific skill XP also adds small Global XP
            if (skill != Skill.Global)
            {
                _xpMap[Skill.Global] += (int)(amount * 0.1f);
                CheckLevelUp(Skill.Global);
            }

            SaveProgress();

            // Visual Feedback
            Screen.ShowSubtitle($"~g~+{amount} XP~w~ ({skill})", 2000);
        }

        public int GetLevel(Skill skill)
        {
            return _levelMap.ContainsKey(skill) ? _levelMap[skill] : 1;
        }

        public int GetXP(Skill skill)
        {
            return _xpMap.ContainsKey(skill) ? _xpMap[skill] : 0;
        }

        private void CheckLevelUp(Skill skill)
        {
            int currentLevel = _levelMap[skill];
            int requiredXP = GetXPForNextLevel(currentLevel);

            if (_xpMap[skill] >= requiredXP)
            {
                _levelMap[skill]++;
                _xpMap[skill] -= requiredXP; // Carry over overflow? Or reset?
                // Standard MMO: Total XP accumulates, Level is derived.
                // Reset Implementation for simplicity of "XP bar":
                // Actually, let's keep Total XP and just calculate Level from it to be safer,
                // but user wants persistence. Let's stick to the "Level Up Event" model.

                Notification.Show($"~y~LEVEL UP!~w~ You are now Level ~b~{_levelMap[skill]}~w~ in ~o~{skill}~w~!");
                GTA.Audio.PlaySoundFrontend("Rank_Up_Milestone", "GTAO_FM_Events_Soundset");

                // Trigger Unlocks
                UnlockManager.CheckUnlocks(skill, _levelMap[skill]);
            }
        }

        // XP Curve: Level 1->2 = 1000, Level 2->3 = 1500, etc.
        private int GetXPForNextLevel(int currentLevel)
        {
            return 1000 + (currentLevel * 500);
        }

        private void SaveProgress()
        {
            try
            {
                ScriptSettings settings = ScriptSettings.Load(SaveFile);
                foreach (var kvp in _xpMap)
                {
                    settings.SetValue("XP", kvp.Key.ToString(), kvp.Value);
                    settings.SetValue("Level", kvp.Key.ToString(), _levelMap[kvp.Key]);
                }
                settings.Save();
            }
            catch (Exception ex)
            {
                GTA.UI.Notification.Show($"Error Saving Progress: {ex.Message}");
            }
        }

        private void LoadProgress()
        {
            if (!File.Exists(SaveFile)) return;

            ScriptSettings settings = ScriptSettings.Load(SaveFile);
            foreach (Skill s in Enum.GetValues(typeof(Skill)))
            {
                _xpMap[s] = settings.GetValue("XP", s.ToString(), 0);
                _levelMap[s] = settings.GetValue("Level", s.ToString(), 1);
            }
        }
    }

    public static class UnlockManager
    {
        public static void CheckUnlocks(ExperienceManager.Skill skill, int level)
        {
            // Logic to notify player of new unlocks
            if (skill == ExperienceManager.Skill.Global)
            {
                if (level == 5) Notification.Show("Unlocked: ~g~Weapon License~w~ purchase availability!");
                if (level == 10) Notification.Show("Unlocked: ~r~Heists~w~!");
            }
            if (skill == ExperienceManager.Skill.Trucking && level == 3)
            {
                Notification.Show("Unlocked: ~b~Hazardous Cargo~w~ missions!");
            }
        }
    }
}

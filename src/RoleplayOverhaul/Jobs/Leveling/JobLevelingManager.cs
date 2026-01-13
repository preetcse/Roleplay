using System;
using System.Collections.Generic;
using GTA;

namespace RoleplayOverhaul.Jobs.Leveling
{
    public class JobLevelingManager
    {
        private static Dictionary<JobType, int> JobXP = new Dictionary<JobType, int>();
        private static Dictionary<JobType, int> JobLevels = new Dictionary<JobType, int>();

        public JobLevelingManager()
        {
            foreach (JobType job in Enum.GetValues(typeof(JobType)))
            {
                JobXP[job] = 0;
                JobLevels[job] = 1;
            }
        }

        public static void AddXP(JobType job, int amount)
        {
            if (!JobXP.ContainsKey(job)) JobXP[job] = 0;
            JobXP[job] += amount;

            int currentLevel = JobLevels.ContainsKey(job) ? JobLevels[job] : 1;
            int nextLevelXP = currentLevel * 1000;

            if (JobXP[job] >= nextLevelXP)
            {
                JobLevels[job]++;
                JobXP[job] -= nextLevelXP;
                GTA.UI.Notification.Show($"Level Up! You are now Level {JobLevels[job]} in {job}.");
            }
        }

        public static int GetLevel(JobType job)
        {
            return JobLevels.ContainsKey(job) ? JobLevels[job] : 1;
        }

        public static int GetXP(JobType job)
        {
            return JobXP.ContainsKey(job) ? JobXP[job] : 0;
        }
    }
}

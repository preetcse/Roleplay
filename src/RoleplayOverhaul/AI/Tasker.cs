using System;
using System.Collections.Generic;
using GTA;
using GTA.Native;
using GTA.Math;

namespace RoleplayOverhaul.AI
{
    public enum TaskType
    {
        Idle,
        Wander,
        Flee,
        Fight,
        Surrender,
        Arrest,
        Investigate,
        Flank,
        Suppress,
        Search
    }

    public class AIState
    {
        public TaskType CurrentTask { get; set; }
        public Ped Target { get; set; }
        public Vector3 TargetPosition { get; set; }
        public float Aggressiveness { get; set; } // 0.0 - 1.0
        public float Fear { get; set; } // 0.0 - 1.0
        public int SquadID { get; set; }
    }

    public class Tasker
    {
        private Dictionary<int, AIState> _pedStates;

        public Tasker()
        {
            _pedStates = new Dictionary<int, AIState>();
        }

        public void RegisterPed(Ped ped)
        {
            if (!_pedStates.ContainsKey(ped.Handle))
            {
                _pedStates[ped.Handle] = new AIState
                {
                    CurrentTask = TaskType.Wander,
                    Aggressiveness = 0.5f,
                    Fear = 0.0f,
                    SquadID = -1
                };
            }
        }

        public void Update()
        {
            // In a real mod this would loop through all tracked peds
            // For prototype, we expose methods to update specific peds
        }

        public void AssignTask(Ped ped, TaskType task, Ped target = null, Vector3? pos = null)
        {
            if (!_pedStates.ContainsKey(ped.Handle)) RegisterPed(ped);

            var state = _pedStates[ped.Handle];
            state.CurrentTask = task;
            state.Target = target;
            if (pos.HasValue) state.TargetPosition = pos.Value;

            ExecuteTask(ped, state);
        }

        private void ExecuteTask(Ped ped, AIState state)
        {
            ped.Task.ClearAll(); // Reset

            switch (state.CurrentTask)
            {
                case TaskType.Wander:
                    ped.Task.WanderAround();
                    break;
                case TaskType.Flee:
                    if (state.Target != null) ped.Task.FleeFrom(state.Target);
                    break;
                case TaskType.Fight:
                    if (state.Target != null)
                    {
                        // Advanced Fighting Logic
                        // Function.Call(Hash.TASK_COMBAT_PED, ped, state.Target, 0, 16);
                        ped.Task.FightAgainst(state.Target);
                    }
                    break;
                case TaskType.Surrender:
                    // Hands Up
                    ped.Task.PlayAnimation("random@arrests", "idle_2_hands_up", 8.0f, -1, true, 0); // Mock anim
                    break;
                case TaskType.Flank:
                    // Go to side
                    // Vector3 flankPos = ...
                    // ped.Task.RunTo(flankPos);
                    break;
            }
        }
    }
}

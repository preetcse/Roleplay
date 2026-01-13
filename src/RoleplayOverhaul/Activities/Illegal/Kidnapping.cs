using System;
using GTA;
using GTA.Native;

namespace RoleplayOverhaul.Activities.Illegal
{
    public static class Kidnapping
    {
        public static void AttemptKidnap(Ped target)
        {
            if (target.IsDead) return;

            // Simple logic: If player has weapon aimed, 50% chance to surrender
            if (Game.Player.Character.IsAiming)
            {
                target.Task.HandsUp(5000);
                GTA.UI.Notification.Show("Target Surrendered. Press G to Grapple.");

                // Logic to attach
                // In a full system, we'd wait for keypress G here.
                // Simulating immediate grapple for prototype
                GrappleTarget(target);
            }
            else
            {
                target.Task.ReactAndFlee(Game.Player.Character);
                GTA.UI.Notification.Show("Target fled! You need to intimidate them first.");
            }
        }

        private static void GrappleTarget(Ped target)
        {
            // Attach target to player
            Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, target, Game.Player.Character, 11816, 0.0f, 0.5f, 0.0f, 0.0f, 0.0f, 0.0f, false, false, false, false, 2, true);
            target.Task.PlayAnimation("random@arrests@busted", "idle_a", 8.0f, -1, AnimationFlags.Loop);
            GTA.UI.Notification.Show("Target Grappled. Go to vehicle trunk to stash.");
        }
    }
}

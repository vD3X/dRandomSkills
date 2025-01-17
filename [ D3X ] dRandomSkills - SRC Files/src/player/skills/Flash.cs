using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using static dRandomSkills.dRandomSkills;

namespace dRandomSkills
{
    public static class Flash
    {
        public static void LoadFlash()
        {
            Utils.RegisterSkill("Flash", "Otrzymujesz losową ilość speed na start rundy", "#A31912");
            
            Instance.RegisterEventHandler<EventRoundFreezeEnd>((@event, info) =>
            {
                Instance.AddTimer(0.1f, () => 
                {
                    foreach (var player in Utilities.GetPlayers())
                    {
                        if (!IsPlayerValid(player)) return;

                        var playerInfo = Instance.skillPlayer.FirstOrDefault(p => p.SteamID == player.SteamID);
                        if (playerInfo?.Skill != "Flash") return;

                        var playerPawn = player.PlayerPawn?.Value;

                        if (playerPawn != null)
                        {
                            float velocityModifier = (float)Instance.Random.NextDouble() * (2.5f - 1.2f) + 1.2f;
                            playerPawn.VelocityModifier = velocityModifier;
                            Utilities.SetStateChanged(player, "CCSPlayerPawn", "m_flVelocityModifier");
                        }
                    }
                });
                
                return HookResult.Continue;
            });
        }

        private static bool IsPlayerValid(CCSPlayerController player)
        {
            return player != null && player.IsValid && player.PlayerPawn?.Value != null;
        }
    }
}
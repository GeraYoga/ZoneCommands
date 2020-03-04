using System;
using System.Linq;
using Harmony;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Permissions;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using static GY.ZoneCommands.ZoneCommands;

namespace GY.ZoneCommands
{
    public class CommandPatch
    {
        public static bool execute_Prefix(SteamPlayer caller, string permission)
        {
            var cfg = Instance.Configuration.Instance;

            var player = UnturnedPlayer.FromSteamPlayer(caller);
            
            if (player == null) {return true;}
            
            if (player.GetPermissions().Any(x => x.Name == cfg.IgnorePermission)){return true;}
            
            if (player.IsAdmin && cfg.AdminOverride){return true;}
            
            var isInSafeZone = player.Player.movement.isSafe;
            var isInDeadZone = player.Player.movement.isRadiated;

            var cmd = permission.TrimStart('/').Split(' ').First();

            if (isInDeadZone && cfg.DeadZoneBannedCommands.Any(x => string.Equals(cmd, x, StringComparison.OrdinalIgnoreCase)))
            {
                UnturnedChat.Say(player, Instance.Translate("dead_zone_block", cmd), Color.red);
                return false;
            }
            if (!isInSafeZone || !cfg.SafeZoneBannedCommands.Any(x => string.Equals(cmd, x, StringComparison.OrdinalIgnoreCase))) return true;
            UnturnedChat.Say(player, Instance.Translate("safe_zone_block", cmd), Color.red);
            return false;
        }
    }
}
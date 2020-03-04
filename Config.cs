using System.Collections.Generic;
using Rocket.API;

namespace GY.ZoneCommands
{
    public class Config : IRocketPluginConfiguration
    {
        public List<string> SafeZoneBannedCommands;
        public List<string> DeadZoneBannedCommands;
        public string IgnorePermission;
        public bool AdminOverride;
        public void LoadDefaults()
        {
            SafeZoneBannedCommands = new List<string>
            {
                "v", "i", "s"
            };
            DeadZoneBannedCommands = new List<string>
            {
                "home", "heal", "god"
            };
            IgnorePermission = "gy.zone.ignore";
            AdminOverride = false;
        }
    }
}
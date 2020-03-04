using System;
using System.Reflection;
using Harmony;
using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Permissions;

namespace GY.ZoneCommands
{
    public class ZoneCommands : RocketPlugin<Config>
    {
        public static ZoneCommands Instance;
        private static HarmonyInstance _harmonyInstance;
        private readonly MethodInfo _myOriginal = typeof(UnturnedPermissions).GetMethod("CheckPermissions", BindingFlags.NonPublic | BindingFlags.Static);
        private readonly MethodInfo _myPrefix = typeof(CommandPatch).GetMethod("execute_Prefix", BindingFlags.Static | BindingFlags.Public);
        
        protected override void Load()
        {
            Logger.Log("Made by https://vk.com/plugins_gy", ConsoleColor.Magenta);
            Logger.Log("Made by https://vk.com/plugins_gy", ConsoleColor.Magenta);
            Logger.Log("Made by https://vk.com/plugins_gy", ConsoleColor.Magenta);
            
            _harmonyInstance = HarmonyInstance.Create("com.gy.plugins");
            Instance = this;
            _harmonyInstance.Patch(_myOriginal, new HarmonyMethod(_myPrefix));
        }

        protected override void Unload()
        {
            _harmonyInstance.Unpatch(_myOriginal, _myPrefix);
            _harmonyInstance = null;
            Instance = null;
        }


        public override TranslationList DefaultTranslations => new TranslationList
        {
            {"safe_zone_block", "Использование команды {0} в Безопасной зоне запрещено!"},
            {"dead_zone_block", "Использование команды {0} в Мёртвой зоне запрещено!"}
        };
    }
}
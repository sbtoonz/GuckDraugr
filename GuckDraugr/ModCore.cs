using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using CreatureManager;
using HarmonyLib;
using ServerSync;

namespace GuckDraugr
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class GuckDraugrMod : BaseUnityPlugin
    {
        private const string ModName = "GuckDraugrMod";
        private const string ModVersion = "1.0";
        private const string ModGUID = "com.zarboz.GuckDraugrMod";
        private static Harmony harmony = null!;
        ConfigSync configSync = new(ModGUID) 
            { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion};
        internal static ConfigEntry<bool> ServerConfigLocked = null!;
        ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }
        ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true) => config(group, name, value, new ConfigDescription(description), synchronizedSetting);
        public void Awake()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            harmony = new(ModGUID);
            harmony.PatchAll(assembly);
            ServerConfigLocked = config("1 - General", "Lock Configuration", true, "If on, the configuration is locked and can be changed by server admins only.");
            configSync.AddLockingConfigEntry(ServerConfigLocked);

            Creature guckDraugr = new("guckdraugr", "GuckDraugr", "assets")
            {
                Biome = Heightmap.Biome.Swamp,
                GroupSize =  new Range(2,4),
                RequiredWeather = Weather.None | Weather.SwampRain,
                Maximum = 5
            };
            guckDraugr.Localize().English("Guck Draugr");
            guckDraugr.Drops["Guck"].Amount = new Range(5, 10);
            guckDraugr.Drops["Guck"].DropChance = 75f;
            

        }
    }
}

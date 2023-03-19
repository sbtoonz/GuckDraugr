using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using CreatureManager;
using HarmonyLib;
using ServerSync;
using UnityEngine;

namespace GuckDraugr
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class GuckDraugrMod : BaseUnityPlugin
    {
        private const string ModName = "GuckDraugrMod";
        public const string ModVersion = "1.0.3";
        private const string ModGUID = "com.zarboz.GuckDraugrMod";
        private static Harmony harmony = null!;

        private AssetBundle? _asset = null;
        public static GameObject? aoe_attack= null;
        public static GameObject? aoe_object= null;
        public static GameObject? vomit_attack= null;
        public static GameObject? vomit_object= null;
        
        public void Awake()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            harmony = new(ModGUID);
            harmony.PatchAll(assembly);
            Game.isModded = true;
            _asset = Utilities.LoadAssetBundle("guckdraugr");
            
            aoe_attack = _asset!.LoadAsset<GameObject>("GuckDraugr_Explode_attack");
            aoe_object = _asset.LoadAsset<GameObject>("GuckDraugr_AOE");
            vomit_attack = _asset.LoadAsset<GameObject>("GuckDraugr_Vomit_Attack");
            vomit_object = _asset.LoadAsset<GameObject>("GuckDraugr_Vomit_AOE");
            _asset.Unload(false);
            
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
            guckDraugr.AttackImmediately = true;
            guckDraugr.CanBeTamed = false;
            
            
        }
        
    }
}

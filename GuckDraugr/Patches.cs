using HarmonyLib;

namespace GuckDraugr
{
    public class Patches
    {
        /*[HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake))]
        public static class ZnetPatch
        {
            public static void Prefix(ZNetScene __instance)
            {
                if(__instance.m_prefabs.Count <=0)return;
                __instance.m_prefabs.Add(GuckDraugrMod.aoe_attack);
                __instance.m_prefabs.Add(GuckDraugrMod.aoe_object);
                __instance.m_prefabs.Add(GuckDraugrMod.vomit_attack);
                __instance.m_prefabs.Add(GuckDraugrMod.vomit_object);
            }
        }*/

        [HarmonyPatch(typeof(ObjectDB), nameof(ObjectDB.Awake))]
        public static class ObjectDbPatch
        {
            public static void Prefix(ObjectDB __instance)
            {
                if(__instance.m_items.Count <= 0 || __instance.GetItemPrefab("Amber")==null)return;
                if(GuckDraugrMod.aoe_attack)__instance.m_items.Add(GuckDraugrMod.aoe_attack);
                if(GuckDraugrMod.vomit_attack)__instance.m_items.Add(GuckDraugrMod.vomit_attack);
                
            }
        }
    }
}
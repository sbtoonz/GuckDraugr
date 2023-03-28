using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace GuckDraugr
{
    public class Patches
    {
        public static ItemDrop? item { get; set; }

        [HarmonyPatch(typeof(ObjectDB), nameof(ObjectDB.Awake))]
        public static class ObjectDbPatch
        {
            public static void Prefix(ObjectDB __instance)
            {
                if(__instance.m_items.Count <= 0 || __instance.GetItemPrefab("Amber")==null)return;
                if(GuckDraugrMod.aoe_attack)__instance.m_items.Add(GuckDraugrMod.aoe_attack);
                if(GuckDraugrMod.vomit_attack)__instance.m_items.Add(GuckDraugrMod.vomit_attack);
                foreach (var instanceMItem in __instance.m_items.Where(instanceMItem => instanceMItem == GuckDraugrMod.aoe_attack))
                {
                    item = instanceMItem.GetComponent<ItemDrop>();
                }
            }

    
            
        }

        
        
        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.OnDamaged))]
        public static class TestPatch
        {
            public static void Postfix(Humanoid __instance, HitData hit)
            {
                if(!__instance.gameObject.name.StartsWith("GuckDraugr"))return;
                if (hit.m_ranged && hit.GetTotalDamage() > 15 && hit.GetAttacker() != __instance)
                {
                    if (item != null)
                    {
                        __instance.m_currentAttack = item.m_itemData.m_shared.m_attack;
                        __instance.m_currentAttack.Start(__instance, __instance.m_body, __instance.m_zanim,
                            __instance.m_animEvent, __instance.m_visEquipment, item.m_itemData,
                            __instance.m_previousAttack,
                            __instance.m_timeSinceLastAttack, __instance.GetAttackDrawPercentage());
                    }
                }
            }
        }
    }
}
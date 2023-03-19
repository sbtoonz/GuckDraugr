using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeWhenHit : MonoBehaviour
{
   [SerializeField]private Collider attack_collider;
   [SerializeField]private Collider m_collider;
   [SerializeField] private ItemDrop m_item;

   private void OnCollisionEnter(Collision collision)
   {
//      Debug.LogWarning($"An item has hit my collider ! {collision.gameObject.name}");
   }
}

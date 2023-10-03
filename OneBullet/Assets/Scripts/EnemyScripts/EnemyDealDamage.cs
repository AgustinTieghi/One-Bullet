using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    private int damage = 25;
    bool canDealDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (canDealDamage)
            {
                PlayerStatsManager.instance.GetHit(damage);
                Debug.Log("HIT!");
            }
        }
    }
    public void StartToDealDamage()
    {
        canDealDamage = true;
        GetComponent<SphereCollider>().enabled = true;
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
        GetComponent<SphereCollider>().enabled = false;
    }
}

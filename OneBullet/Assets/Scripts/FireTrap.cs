using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            other.gameObject.GetComponent<PlayerStatsManager>().playerHP = 0;
        }
    }
}

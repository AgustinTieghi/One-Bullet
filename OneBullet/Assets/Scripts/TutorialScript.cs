using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialScript : MonoBehaviour
{
    public GameObject spawners;
    private void Start()
    {
        SensorScript.OnTutorialZoneExit += ActivateZombie;
    }
    public void ActivateZombie()
    {
        GetComponent<EnemyBrain>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
    }

    private void OnDestroy()
    {
        SensorScript.OnTutorialZoneExit -= ActivateZombie;
        if (spawners != null)
        {
            spawners.SetActive(true);
        }          
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorScript : MonoBehaviour
{
    public delegate void DelTutorial();
    public static event DelTutorial OnTutorialZoneExit;

    private void OnTriggerEnter(Collider other)
    {
        OnTutorialZoneExit.Invoke();
    }
}

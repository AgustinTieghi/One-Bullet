using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [Header("Player configuration")]
    [SerializeField] float raycastMaxRange;
    [SerializeField] LayerMask zombiePartLayer;
    [SerializeField] FirstPersonController firstPersonController;
    [Header("Part as weapon config")]
    [SerializeField] GameObject spawnPoint;
    [SerializeField] List<GameObject> partsPrefabs;
    [Header("Zombie corpse parts references")]
    [SerializeField] GameObject zombieHead;
    [SerializeField] GameObject interactUI;
    [SerializeField] GameObject missGO;
    float timer;

    public bool hasWeapon;
    public PlayerAttack playerAttack;
    Outline partOutline;
    void Update()
    {
        InteractUI();

        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectZombiePart();
        }
    }


    void InteractUI()
    {
        Camera _playerCamera = firstPersonController.playerCamera;
        RaycastHit hit;
        Ray raycast = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(raycast, out hit, raycastMaxRange, zombiePartLayer, QueryTriggerInteraction.Collide))
        {
            if (hasWeapon) return;
            interactUI.gameObject.SetActive(true);
            partOutline = hit.collider.gameObject.GetComponentInParent<Outline>();
            if (partOutline != null)
            {
                partOutline.enabled = true;
            }
        }
        else
        {
            interactUI.gameObject.SetActive(false);
            if (partOutline != null)
            {
                partOutline.enabled = false;
            }
        }
    }

    void SelectZombiePart()
    {
        Camera _playerCamera = firstPersonController.playerCamera;
        RaycastHit hit;
        Ray raycast = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);

        if (Physics.Raycast(raycast, out hit, raycastMaxRange, zombiePartLayer, QueryTriggerInteraction.Collide))
        {
            if (hasWeapon) return;
            
            if (hit.collider.gameObject.tag == "ArmsTag")
            {
                hit.collider.gameObject.GetComponentInParent<SkinnedMeshRenderer>().gameObject?.SetActive(false);
                GetZombiePart(hit.collider);
            }
            else if (hit.collider.gameObject.tag == "HeadTag")
            {
                float random = UnityEngine.Random.Range(0, 10);
                if (random <= 4)
                {
                    GetZombiePart(hit.collider);
                }
                else
                {
                    StartCoroutine(missTextCoroutine());        
                }
                hit.collider.gameObject.GetComponentInParent<SkinnedMeshRenderer>().enabled = false;
            }
            else if (hit.collider.gameObject.tag == "LegsTag")
            {
                GetZombiePart(hit.collider);
                hit.collider.gameObject.GetComponentInParent<SkinnedMeshRenderer>().enabled = false;
            }
        }
       
    }

    void GetZombiePart(Collider zombiePart)
    {

        hasWeapon = true;
        foreach (GameObject part in partsPrefabs)
        {
            if (zombiePart.gameObject.tag == part.tag)
            {
                GameObject spawnedPart = Instantiate(part, spawnPoint.transform);
                playerAttack.currentWeapon = spawnedPart;
                var zombie = zombiePart.gameObject.GetComponentInParent<EnemyBrain>();
                if (zombie != null)
                {
                    zombie.StartDespawn(3);
                }
                else
                {
                    return;
                }
            }
        }
    }

    IEnumerator missTextCoroutine()
    {
        missGO.SetActive(true);
        yield return new WaitForSeconds(1);
        missGO.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(firstPersonController.playerCamera.transform.position, firstPersonController.playerCamera.transform.forward * raycastMaxRange);
    }
}

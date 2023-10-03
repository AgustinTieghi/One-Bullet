using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] float coolDownInterval;
    public GameObject currentWeapon;
    [SerializeField] GameObject grenadeHead;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float forceAmount = 0.5f;
    [SerializeField] FirstPersonController firstPersonController;
    [SerializeField] Animator weaponAnimator;
    public EnemySpawner enemySpawner;
    private void Start()
    {

    }
    void Update()
    {


        if (currentWeapon != null)
        {
            weaponAnimator = currentWeapon.GetComponent<Animator>();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (currentWeapon.tag == "HeadTag")
                {
                    Camera _playerCamera = firstPersonController.playerCamera;
                    Destroy(currentWeapon);
                    GameObject explosiveHead = Instantiate(grenadeHead, spawnPoint.transform);
                    explosiveHead.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * forceAmount, ForceMode.Impulse);
                }
                else
                {
                    weaponAnimator.SetTrigger("attack");
                    StartCoroutine(HitEnemy());
                }
                GetComponentInParent<PlayerLogic>().hasWeapon = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                other.gameObject.GetComponent<EnemyBrain>()?.TakeDamage(10);
                //enemySpawner.zombies.Remove(other.gameObject);
                Destroy(currentWeapon, 0.47f);
            }
        }
    }

    IEnumerator HitEnemy()
    {
        GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SphereCollider>().enabled = false;
    }
}
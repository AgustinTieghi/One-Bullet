using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Configuration")]
    public GameObject currentWeapon;
    [SerializeField] GameObject grenadeHead;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float forceAmount = 5f;
    [SerializeField] FirstPersonController firstPersonController;
    [SerializeField] Animator weaponAnimator;
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
                    ThrowHead();
                }
                else
                {
                    currentWeapon?.GetComponent<AttackAudioManager>().PlayNoHitSound();
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
                Destroy(currentWeapon, 0.47f);
            }
        }
    }
    void ThrowHead()
    {
        Camera _playerCamera = firstPersonController.playerCamera;
        Destroy(currentWeapon);
        GameObject explosiveHead = Instantiate(grenadeHead, spawnPoint.transform);
        explosiveHead.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * forceAmount, ForceMode.Impulse);
    }

    IEnumerator HitEnemy()
    {
        GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SphereCollider>().enabled = false;
    }

}
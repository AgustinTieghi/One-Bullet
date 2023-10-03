using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGrenadeAttack : MonoBehaviour
{
    public int layerMask;
    private void Start()
    {
        layerMask = 1 << 7;
    }
    [SerializeField] float explosionRadius = 5f;
    public ParticleSystem impactEffect;
    [SerializeField] AudioSource audioSource;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, layerMask);
            if (hitColliders.Length > 1)
            {
                //Queriamos que se destruyan la mitad pero anda igual
                for (int i = 0; i <= hitColliders.Length / 2; i++)
                {
                    Destroy(hitColliders[i].gameObject);
                }
            }      

            foreach (Collider hitCollider in hitColliders)
            {
                EnemyBrain collissionScript = hitCollider.gameObject.GetComponent<EnemyBrain>();
                if (collissionScript != null)
                {
                    collissionScript.TakeDamage(20f);
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }
                else
                {
                    return;
                }
            }
            //sound
            audioSource.Play();
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    [Header("FootstepSounds")]
    [SerializeField] AudioSource footstepSource;

    [Header("Attack Sounds")]
    [SerializeField] AudioSource attackSourse;
    [SerializeField] List<AudioClip> enemyAttackClips;

    [Header("Hit Sounds")]
    [SerializeField] AudioSource enemyHitSource;

    [Header("Death sound")]
    [SerializeField] AudioSource enemyDeath;

    void OnBoneFootstep()
    {
        if (footstepSource != null)
        {
            footstepSource.Play();
        }
    }
    void OnBoneAttackPlaySounds()
    {
        if(attackSourse != null)
        {
            int index = Random.Range(0, enemyAttackClips.Count);
            attackSourse.PlayOneShot(enemyAttackClips[index]);
        }
    }
    public void OnBoneGetHit()
    {
        if (enemyHitSource != null)
        {
            enemyHitSource.Play();
        }
    }
    public void OnEnemyDead()
    {
        if(enemyDeath != null)
        {
            enemyDeath.Play();
        }

    }
}

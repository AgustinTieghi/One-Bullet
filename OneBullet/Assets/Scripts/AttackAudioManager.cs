using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource attackAudioSource;
    [SerializeField] AudioSource attackMissAudioSource;
    [SerializeField] AudioClip attackClip;
    [SerializeField] AudioClip attackClip2;
    [SerializeField] AudioClip attackMissClip;
    [SerializeField] AudioClip attackMissClip2;
    public void PlayHitSound()
    {
        int randomIndex = Random.Range(0, 2);
        if (attackAudioSource != null)
        {
            if (randomIndex == 1)
            {
                attackAudioSource.clip = attackClip;
            }
            else
            {
                attackAudioSource.clip = attackClip2;
            }
            attackAudioSource.Play();
        }
    }
    public void PlayNoHitSound()
    {
        int randomIndex = Random.Range(0, 2);
        if (attackMissAudioSource != null)
        {
            if (randomIndex == 1)
            {
                attackMissAudioSource.clip = attackMissClip;
            }
            else
            {
                attackMissAudioSource.clip = attackMissClip2;
            }
            attackMissAudioSource.Play();
        }
    }
}

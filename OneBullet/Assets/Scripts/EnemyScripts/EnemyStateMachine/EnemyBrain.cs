using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] public float enemyHP = 50;
    [SerializeField] Animator enemyAnimator;

    [Header("Sensors")]
    [SerializeField] bool isActive;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float attackRange = 2f;
    [SerializeField] List<Collider> zombiePartsColliders;

    [Header("States")]
    [SerializeField] EnemyBaseState defaultState;
    [SerializeField] EnemyBaseState chaseState;
    [SerializeField] EnemyBaseState attackState;
    [SerializeField] AttackAudioManager attackAudioManager;

    [Header("Audio configuration")]
    [SerializeField] AudioSource zombieAudioSource;
    [SerializeField] AudioClip zombieIdle, zombieAttack, zombieDead;
    [SerializeField] float timer = 0;
    public Transform target;

    EnemyBaseState currentState;
    NavMeshAgent agent;
    public delegate void DelDeath();
    public static event DelDeath OnDeath;

    void Start()
    {

        isActive = true;
        if (target == null)
        {
            target = PlayerStatsManager.instance.transform;
        }

        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        currentState = defaultState;


    }


    void Update()
    {
        if (isActive)
        {
            float interval = Random.Range(2, 15);
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                StartCoroutine(IdleSound(interval));
                timer = 0;
            }


            if (enemyHP <= 0)
            {
                Die();
                OnDeath?.Invoke();
            }

            TransitionToState(chaseState);
            if (Vector3.Distance(target.position, transform.position) <= attackRange)
            {
                TransitionToState(attackState);
                zombieAudioSource.clip = zombieAttack;
                zombieAudioSource.Play();
            }
            currentState?.OnUpdateState();
        }
    }

    public void TransitionToState(EnemyBaseState nextState)
    {
        if (currentState == nextState) return;
        currentState.OnExitState();
        currentState.enabled = false;

        currentState = nextState;

        currentState.enabled = true;
        currentState.OnEnterState();
    }

    public void TakeDamage(float damageAmount)
    {
        enemyHP -= damageAmount;
        
        attackAudioManager = target?.GetComponentInChildren<AttackAudioManager>();
        if (attackAudioManager != null)
        {
            attackAudioManager.PlayHitSound();
        }
    }

    void Die()
    {        
        if (agent != null)
        {
            zombieAudioSource.clip = zombieDead;
            zombieAudioSource.Play();
            enemyAnimator.SetTrigger("death");
            agent.isStopped = true;
        }
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        isActive = false;

        foreach (Collider collider in zombiePartsColliders)
        {
            collider.gameObject.SetActive(true);
        }
        StartDespawn(10);
        GetComponent<Collider>().enabled = false;
    }
    public void StartDespawn(float time)
    {
        StartCoroutine(DespawnOnDeath(time));
    }

    private IEnumerator DespawnOnDeath(float time)
    {
        enemyAnimator.SetTrigger("death");
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    IEnumerator IdleSound(float interval)
    {
        zombieAudioSource.clip = zombieIdle;
        zombieAudioSource.Play();
        yield return new WaitForSeconds(interval);
    }


}

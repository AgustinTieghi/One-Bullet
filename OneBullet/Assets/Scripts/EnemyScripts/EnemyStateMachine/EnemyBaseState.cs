using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Animator animator;
    protected EnemyBrain enemyBrain;

    protected EnemyChaseState enemyChaseState;
    protected EnemyAttackState enemyAttackState;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        animator = GetComponent<Animator>();
        agent= GetComponent<NavMeshAgent>();
    }
    public virtual void OnEnterState()
    {
    }    
    public virtual void OnUpdateState()
    {
        float speedPercentage = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speed", speedPercentage);
    }
    public virtual void OnExitState()
    {
    }
}

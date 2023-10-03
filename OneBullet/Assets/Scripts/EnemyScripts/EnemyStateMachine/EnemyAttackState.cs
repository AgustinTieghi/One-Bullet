using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    float timePassed;
    public float rotationSpeed;
    [SerializeField] float attackCoolDown = 2f;

    public override void OnEnterState()
    {
        base.OnEnterState();
    }
    public override void OnExitState()
    {
        base.OnExitState();
    }
    public override void OnUpdateState()
    {
        base.OnUpdateState();

        if (timePassed >= attackCoolDown)
        {
            FaceTarget();
            animator.SetTrigger("attack");
            timePassed = 0f;
        }
        timePassed += Time.deltaTime;
    }
    private void FaceTarget()
    {
        Vector3 direction = (enemyBrain.target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    //for animation events
    public void StartToDealDamage()
    {
        GetComponentInChildren<EnemyDealDamage>().StartToDealDamage();  
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDealDamage>().EndDealDamage();
    }
}

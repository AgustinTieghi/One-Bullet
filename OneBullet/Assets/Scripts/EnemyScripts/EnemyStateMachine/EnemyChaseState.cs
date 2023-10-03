using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    Vector3 direction;
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

        Vector3 direction = (enemyBrain.target.position - transform.position);

        agent.SetDestination(enemyBrain.target.position);
        FaceTarget();

    }
    private void FaceTarget()
    {
        direction = (enemyBrain.target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
    }
}

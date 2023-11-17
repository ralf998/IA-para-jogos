using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    //private static int _enemyLayerMask = 10;//1 << 6;

    private Transform _transform;
    //private Animator _animator;
    GameObject targetEnemy;

    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
        //_animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(AllyBT.life <= AllyBT.criticalLife)
        {
            state = NodeState.FAILURE;
            Debug.Log(state + " low health");
            return state;
        }
        else
        {
            if (t == null)
            {
                float distance = Mathf.Infinity;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                if(enemies != null)
                {
                    foreach (GameObject enemy in enemies) {
                        float currentDistance = (enemy.transform.position - _transform.position).sqrMagnitude;
                        if (currentDistance < distance) {
                            targetEnemy = enemy;
                            distance = currentDistance;
                        }
                    }
                /*Collider[] colliders = Physics.OverlapSphere(
                    _transform.position, AllyBT.fovRange, _enemyLayerMask);

                if (colliders.Length > 0)
                {*/
                    parent.parent.SetData("target", targetEnemy.transform);
                    //_animator.SetBool("Walking", true);
                    state = NodeState.SUCCESS;
                    Debug.Log(state);
                    return state;
                }

                state = NodeState.FAILURE;
                Debug.Log(state + " no target");
                return state;
            }

            state = NodeState.SUCCESS;
            //Debug.Log(state);
            return state;
        }
        
    }

}
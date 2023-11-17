using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
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
            //Debug.Log(state + " low health");
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
                    parent.parent.SetData("target", targetEnemy.transform);
                    //_animator.SetBool("Walking", true);
                    state = NodeState.SUCCESS;
                    //Debug.Log(state);
                    return state;
                }

                state = NodeState.FAILURE;
                //Debug.Log(state + " no target");
                return state;
            }

            state = NodeState.SUCCESS;
            //Debug.Log(state);
            return state;
        }
    }
}
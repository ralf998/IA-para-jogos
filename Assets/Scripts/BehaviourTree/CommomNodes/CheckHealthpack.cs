using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckHealthpack : Node
{

    private Transform _transform;
    //private Animator _animator;
    GameObject targetHeal;

    public CheckHealthpack(Transform transform)
    {
        _transform = transform;
        //_animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Debug.Log("wtf pfr");
        object t = GetData("healthPack");
        if (t == null)
        {
            
            float distance = Mathf.Infinity;

            GameObject[] Blabla = GameObject.FindGameObjectsWithTag("HealthPack");

            if(Blabla != null)
            {
                foreach (GameObject heal in Blabla) {
                    float currentDistance = (heal.transform.position - _transform.position).sqrMagnitude;
                    if (currentDistance < distance) {
                        targetHeal = heal;
                        distance = currentDistance;
                    }
                }
                parent.parent.SetData("healthPack", targetHeal.transform);

                state = NodeState.SUCCESS;
                return state;
            }
            


            state = NodeState.FAILURE;
            return state;

            
        }

        state = NodeState.SUCCESS;
        return state;
    }

}
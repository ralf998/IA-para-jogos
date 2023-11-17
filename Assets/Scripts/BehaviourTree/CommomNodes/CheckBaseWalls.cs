using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckBaseWalls : Node
{

    private Transform _transform;
    //private Animator _animator;
    GameObject targetWall;

    public CheckBaseWalls(Transform transform)
    {
        _transform = transform;
        //_animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {

        object t = GetData("allyBaseWall");

        if (t == null)
        {
            if(!AllyBT.hasBaseBuildt)
            {
                float distance = Mathf.Infinity;

                GameObject[] Blabla = GameObject.FindGameObjectsWithTag("AllyBaseWall");

                

                if(Blabla != null)
                {
                    foreach (GameObject wall in Blabla) {
                        float currentDistance = (wall.transform.position - _transform.position).sqrMagnitude;
                        if(wall.GetComponent<AWallSM>().life == 0 && currentDistance < distance)
                        {
                            targetWall = wall;
                            distance = currentDistance;
                        }
                    }
                    parent.parent.SetData("allyBaseWall", targetWall.transform);

                    state = NodeState.SUCCESS;
                    return state;
                }

                
            }
            
            state = NodeState.FAILURE;
            return state;

        }

        state = NodeState.SUCCESS;
        return state;
    }

}
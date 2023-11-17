using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckResourcesAndBase : Node
{

    private Transform _transform;
    //private Animator _animator;
    GameObject targetResource;

    public CheckResourcesAndBase(Transform transform)
    {
        _transform = transform;
        //_animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("resourcePack");

        if (t == null)
        {
            if(!AllyBT.hasBaseBuildt)
            {
                float distance = Mathf.Infinity;

                GameObject[] Blabla = GameObject.FindGameObjectsWithTag("ResourcePackage");

                if(Blabla != null)
                {
                    foreach (GameObject resource in Blabla) {
                        float currentDistance = (resource.transform.position - _transform.position).sqrMagnitude;
                        if (currentDistance < distance) {
                            targetResource = resource;
                            distance = currentDistance;
                        }
                    }
                    parent.parent.SetData("resourcePack", targetResource.transform);

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
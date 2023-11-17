using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform _transform;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
        transform.GetComponent<SpriteRenderer>().color = new Color(1f, 0.92f, 0.016f, 1f);
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, AllyBT.speed * Time.deltaTime);
        }

        state = NodeState.RUNNING;
        return state;
    }

}
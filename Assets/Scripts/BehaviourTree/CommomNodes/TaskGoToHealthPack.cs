using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToHealthPack : Node
{
    private Transform _transform;

    public TaskGoToHealthPack(Transform transform)
    {
        _transform = transform;
        transform.GetComponent<SpriteRenderer>().color = new Color(1f, 0.46f, 0.008f, 1f);
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("healthPack");

        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, AllyBT.speed * Time.deltaTime);
        }

        state = NodeState.RUNNING;
        return state;
    }

}
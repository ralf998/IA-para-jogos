using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToHealthPack : Node
{
    private Transform _transform;
    Pathfinding _aStar;

    public TaskGoToHealthPack(Transform transform, Pathfinding aStar)
    {
        _transform = transform;
        _aStar = aStar;
        transform.GetComponent<SpriteRenderer>().color = new Color(1f, 0.46f, 0.008f, 1f);
    }

    public override NodeState Evaluate()
    {
        Debug.Log("PQP1");
        Transform target = (Transform)GetData("healthPack");

        _aStar.seeker = _transform;
        _aStar.target = target;

        List<GridNode> point = _aStar.GetPath();

        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, point[0].worldPosition, AllyBT.speed * Time.deltaTime);
        }

        state = NodeState.RUNNING;
        return state;
    }

}
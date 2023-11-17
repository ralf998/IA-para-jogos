using System.Collections.Generic;
using BehaviorTree;

public class AllyBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public UnityEngine.Rigidbody2D rigidBody;
    public UnityEngine.Transform tf;
    // public float speed = 1f;
    public static float speed = 1f;
    public static float life = 100;
    public static float criticalLife = 25;
    public float damage = 10;
    public int resources = 50;
    public static bool hasBaseBuildt = false;

    //
    public static float fovRange = 6f;
    public static float attackRange = 1f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>//Enemy in range
            {
               new CheckEnemyInFOVRange(transform),
               new TaskGoToTarget(transform),
            }),
            new Sequence(new List<Node>//Heal in range
            {
                new CheckHealthpack(transform),
                new TaskGoToHealthPack(transform),
            }),
            new Sequence(new List<Node>//Resources in range
            {
                new CheckResourcesAndBase(transform),
                new TaskGoToResourcePack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckBaseWalls(transform),
                new TaskGoToWallBase(transform),
            })
            //new TaskPatrol(transform, waypoints),
        });

        return root;
    }

    public float GetLifePoints()
    {
        return life;
    }

    public void IncreaseLifePoints(float value)
    {
        life += value;
    }
}

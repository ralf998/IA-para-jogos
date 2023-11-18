using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AllyBT : BTree
{
    public Rigidbody rigidBody;
    public Transform tf;
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

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "Enemy") {
            IncreaseLifePoints(-collisionInfo.gameObject.GetComponent<NPCEnemySM>().damage);
            rigidBody.velocity = (tf.position -collisionInfo.gameObject.transform.position).normalized;
        }
        if (collisionInfo.gameObject.tag == "AllyBaseWall") {
            rigidBody.GetComponent<Collider>().isTrigger = true;
        }
    }

    void OnCollisionStay2D(Collision2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "AllyBaseWall") {
            rigidBody.GetComponent<Collider>().isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "AllyBaseWall") {
            resources = collisionInfo.gameObject.GetComponent<AWallSM>().Fixes(resources);
        }
    }

    private void OnTriggerStay2D(Collider2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "AllyBaseWall" && collisionInfo.gameObject.GetComponent<AWallSM>().currentState.name == "Broken") {
            rigidBody.GetComponent<Collider>().isTrigger = false;
        }
    }

    void OnTriggerExit2D(Collider2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "AllyBaseWall") {
            rigidBody.GetComponent<Collider>().isTrigger = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemySM : StateMachine {
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Chasing chasingState;
    [HideInInspector]
    public Stun stunState;
    [HideInInspector]
    public Fallen fallenState;

    public List<GameObject> targets;
    //public GameObject curTarget;

    //public Rigidbody2D rigidBody;
    public Rigidbody rigidBody;
    public Transform tf;
    public Pathfinding aStar;
    public float speed = 0.5f;
    public float life = 30;
    public float damage = 5;

    private void Awake() {
        idleState = new Idle(this);
        chasingState = new Chasing(this);
        stunState = new Stun(this);
        fallenState = new Fallen(this);
    }

    protected override BaseState GetInitialState() {
        aStar = GetComponent<Pathfinding>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        tf = GetComponent<Transform>();
        chasingState.FindCurrentTarget();
        return idleState;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "Ally") {
            if(collisionInfo.gameObject.GetComponent<NPCAllySM>() != null) {
                life -= collisionInfo.gameObject.GetComponent<NPCAllySM>().damage;
            } else if(collisionInfo.gameObject.GetComponent<AllyBT>() != null) {
                life -= collisionInfo.gameObject.GetComponent<AllyBT>().damage;
            }
            //rigidBody.velocity = new Vector3(0,0,0);
            rigidBody.velocity = 2*speed*(tf.position -collisionInfo.gameObject.transform.position).normalized;
            ChangeState(stunState);
        }
        if (collisionInfo.gameObject.tag == "AllyBaseWall") {
            ChangeState(stunState);
            rigidBody.velocity = speed*(tf.position -collisionInfo.gameObject.transform.position).normalized/2;
        }
    }

    void OnTriggerEnter2D(Collider2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "Ally" && currentState != fallenState) {
            if(collisionInfo.gameObject.GetComponent<NPCAllySM>() != null) {
                life -= collisionInfo.gameObject.GetComponent<NPCAllySM>().damage;
            } else if(collisionInfo.gameObject.GetComponent<AllyBT>() != null) {
                life -= collisionInfo.gameObject.GetComponent<AllyBT>().damage;
            }
            rigidBody.velocity = 2*speed*(tf.position -collisionInfo.gameObject.transform.position).normalized;
            ChangeState(stunState);
        }
    }

    public void LeaveStun() {
        if (currentState != fallenState) {
            idleState.UpdateLogic();
        }
    }
    /*
    private void Update() {
        targets.clear();
        targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
    }*/

    public void Teste() {
        Debug.Log("Fodace");
    }
}

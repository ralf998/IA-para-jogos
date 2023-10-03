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
    public GameObject curTarget;

    public Rigidbody2D rigidBody;
    public Transform tf;
    public float speed = 0.5f;
    public float life = 20;
    public float damage = 10;

    private void Awake() {
        idleState = new Idle(this);
        chasingState = new Chasing(this);
        stunState = new Stun(this);
        fallenState = new Fallen(this);
    }

    protected override BaseState GetInitialState() {
        targets.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        tf = GetComponent<Transform>();
        chasingState.FindCurrentTarget();
        return idleState;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "Ally") {
            life-=5;
            ChangeState(stunState);
        }
    }

    public void LeaveStun() {
        fallenState.UpdateLogic();
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

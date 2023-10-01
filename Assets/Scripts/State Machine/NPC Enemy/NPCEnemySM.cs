using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemySM : StateMachine {
    [HideInInspector]
    public Chasing chasingState;
    [HideInInspector]
    public Fallen fallenState;

    public List<GameObject> targets;
    public GameObject nearTarget;

    public Rigidbody rigidBody;
    public Transform tf;
    public float speed = 0.5f;
    public float life = 20;
    public float damage = 10;

    private void Awake() {
        chasingState = new Chasing(this);
        fallenState = new Fallen(this);
    }

    protected override BaseState GetInitialState() {
        targets.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        tf = GetComponent<Transform>();
        return chasingState;
    }
    /*
    private void Update() {
        targets.clear();
        targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
    }*/
}

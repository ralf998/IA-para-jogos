using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAllySM : StateMachine {
    [HideInInspector]
    public Farming farmingState;
    [HideInInspector]
    public Dead deadState;

    public List<GameObject> enemies;
    public List<GameObject> allies;
    public GameObject nearEnemy;

    public Rigidbody rigidBody;
    public Transform tf;
    public float speed = 1f;
    public float life = 100;
    public float damage = 10;

    private void Awake() {
        farmingState = new Farming(this);
        deadState = new Dead(this);
    }

    protected override BaseState GetInitialState() {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        allies.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        tf = GetComponent<Transform>();
        return farmingState;
    }
    /*
    private void Update() {
        enemies.clear();
        enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        allies.clear();
        allies = new List<GameObject>();
        allies.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
    }*/
}

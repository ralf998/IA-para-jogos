using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCAllySM : StateMachine {
    [HideInInspector]
    public Farming farmingState;
    [HideInInspector]
    public Heal healState;
    [HideInInspector]
    public Group groupState;
    [HideInInspector]
    public Hit hitState;
    [HideInInspector]
    public Dead deadState;

    public List<GameObject> enemies;
    public GameObject nearEnemy;
    public List<GameObject> heals;
    public GameObject nearHeal;
    public List<GameObject> allies;
    public GameObject nearAlly;
    public float alliesDistance;

    public Rigidbody2D rigidBody;
    public Transform tf;
    public float speed = 1f;
    public float life = 100;
    public float damage = 10;
    public int resources = 200;

    private void Awake() {
        farmingState = new Farming(this);
        healState = new Heal(this);
        groupState = new Group(this);
        hitState = new Hit(this);
        deadState = new Dead(this);
    }

    protected override BaseState GetInitialState() {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        allies.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        heals.AddRange(GameObject.FindGameObjectsWithTag("HealthPack"));
        allies.Remove(this.gameObject);
        tf = GetComponent<Transform>();
        farmingState.FindCurrentEnemy();
        return farmingState;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "Enemy") {
            life -= collisionInfo.gameObject.GetComponent<NPCEnemySM>().damage;
            ChangeState(hitState);
            rigidBody.velocity = speed*(tf.position -collisionInfo.gameObject.transform.position).normalized;
        }
        if (collisionInfo.gameObject.tag == "Ally") {
            ChangeState(hitState);
            rigidBody.velocity = speed*(tf.position -collisionInfo.gameObject.transform.position).normalized/2;
        }
        if (collisionInfo.gameObject.tag == "AllyBaseWall") {
            rigidBody.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "HealthPack") {
            heals.Remove(collisionInfo.gameObject);
            if (!heals.Any()) {
                nearHeal = null;
            }
            foreach (GameObject ally in allies) {
                ally.GetComponent<NPCAllySM>().heals = heals;
                if (!heals.Any()) {
                    ally.GetComponent<NPCAllySM>().nearHeal = null;
                }
            }
        }
        if (collisionInfo.gameObject.tag == "AllyBaseWall") {
            resources = collisionInfo.gameObject.GetComponent<AWallSM>().Fixes(resources);
        }
    }

    void OnTriggerExit2D(Collider2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "AllyBase") {
            rigidBody.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    public void LeaveStun() {
        ChangeState(farmingState);
    }

    public void Die() {
        foreach (GameObject enemy in enemies) {
            enemy.GetComponent<NPCEnemySM>().targets.Remove(this.gameObject);
            if (enemy.GetComponent<NPCEnemySM>().curTarget == this.gameObject) {
                enemy.GetComponent<NPCEnemySM>().curTarget = null;
            }
        }
        foreach (GameObject ally in allies) {
            ally.GetComponent<NPCAllySM>().allies.Remove(this.gameObject);
            if (ally.GetComponent<NPCAllySM>().nearAlly == this.gameObject) {
                ally.GetComponent<NPCAllySM>().nearAlly = null;
            }
        }
        Destroy(this.gameObject);
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

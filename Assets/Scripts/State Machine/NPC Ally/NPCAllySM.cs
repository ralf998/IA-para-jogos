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

    public List<GameObject> packages;
    public GameObject nearPackage;
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
    public int resources = 50;

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
        packages.AddRange(GameObject.FindGameObjectsWithTag("ResourcePackage"));
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

    void OnCollisionStay2D(Collision2D collisionInfo) {
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
                if(collisionInfo.gameObject.GetComponent<NPCAllySM>() != null) {
                    ally.GetComponent<NPCAllySM>().heals = heals;
                    if (!heals.Any()) {
                        ally.GetComponent<NPCAllySM>().nearHeal = null;
                    }
                }
            }
        }
        if (collisionInfo.gameObject.tag == "ResourcePackage") {
            packages.Remove(collisionInfo.gameObject);
            if (!packages.Any()) {
                nearPackage = null;
            }
            foreach (GameObject ally in allies) {
                if(collisionInfo.gameObject.GetComponent<NPCAllySM>() != null) {
                    ally.GetComponent<NPCAllySM>().packages = packages;
                    if (!packages.Any()) {
                        ally.GetComponent<NPCAllySM>().nearPackage = null;
                    }
                }
            }
        }
        if (collisionInfo.gameObject.tag == "Enemy" && collisionInfo.gameObject.GetComponent<NPCEnemySM>().currentState.name != "Fallen") {
            ChangeState(hitState);
            rigidBody.velocity = speed*(tf.position -collisionInfo.gameObject.transform.position).normalized/2;
        }
        if (collisionInfo.gameObject.tag == "AllyBaseWall") {
            resources = collisionInfo.gameObject.GetComponent<AWallSM>().Fixes(resources);
        }
    }

    private void OnTriggerStay2D(Collider2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "AllyBaseWall" && collisionInfo.gameObject.GetComponent<AWallSM>().currentState.name == "Broken") {
            rigidBody.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    void OnTriggerExit2D(Collider2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "AllyBaseWall") {
            rigidBody.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    public void LeaveStun() {
        ChangeState(farmingState);
    }

    public void Die() {
        foreach (GameObject enemy in enemies) {
            enemy.GetComponent<NPCEnemySM>().targets.Remove(this.gameObject);
            /*if (enemy.GetComponent<NPCEnemySM>().curTarget == this.gameObject) {
                enemy.GetComponent<NPCEnemySM>().curTarget = null;
            }*/
        }
        foreach (GameObject ally in allies) {
            if(ally.GetComponent<NPCAllySM>() != null) {
                ally.GetComponent<NPCAllySM>().allies.Remove(this.gameObject);
                if (ally.GetComponent<NPCAllySM>().nearAlly == this.gameObject) {
                    ally.GetComponent<NPCAllySM>().nearAlly = null;
                }
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

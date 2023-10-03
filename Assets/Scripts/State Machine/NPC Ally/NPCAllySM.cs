using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCAllySM : StateMachine {
    [HideInInspector]
    public Farming farmingState;
    [HideInInspector]
    public Dead deadState;
    [HideInInspector]
    public Heal healState;
    [HideInInspector]
    public Hit hitState;

    public List<GameObject> enemies;
    public List<GameObject> allies;
    public List<GameObject> heals;
    public GameObject nearEnemy;
    public GameObject nearHeal;

    public Rigidbody2D rigidBody;
    public Transform tf;
    public float speed = 1f;
    public float life = 100;
    public float damage = 10;

    private void Awake() {
        farmingState = new Farming(this);
        deadState = new Dead(this);
        healState = new Heal(this);
        hitState = new Hit(this);
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
            life-=5;
            ChangeState(hitState);
            rigidBody.velocity = speed*(tf.position -collisionInfo.gameObject.transform.position).normalized;
        }
    }

    void OnTriggerEnter2D(Collider2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "HealthPack") {
            //life+=30;
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
    }

    public void LeaveStun() {
        deadState.UpdateLogic();
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

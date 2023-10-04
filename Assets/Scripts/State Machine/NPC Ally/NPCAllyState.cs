using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCAlly : BaseState {
    public NPCAllySM sm;
    
    public NPCAlly(string name, NPCAllySM stateMachine) : base(name, stateMachine) {}

    public override void Enter() {
        base.Enter();
        sm.rigidBody.velocity = new Vector3(0,0,0);
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (sm.life <= 0) {
            stateMachine.ChangeState(sm.deadState);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        if (sm.nearAlly != null && 5 > (sm.nearAlly.transform.position - sm.tf.position).sqrMagnitude) {
            sm.rigidBody.velocity = sm.speed * (sm.rigidBody.velocity - new Vector2((sm.nearAlly.transform.position - sm.tf.position).x, (sm.nearAlly.transform.position - sm.tf.position).z).normalized/3).normalized;
        }
        /*if (sm.life > 70) {
            stateMachine.ChangeState(sm.farmingState);
        }*/
    }

    public void FindCurrentEnemy() {
        GameObject targetEnemy = null;
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in sm.enemies) {
            if (!enemy.GetComponent<Collider2D>().isTrigger) {
                float currentDistance = (enemy.transform.position - sm.tf.position).sqrMagnitude;
                if (currentDistance < distance) {
                    targetEnemy = enemy;
                    distance = currentDistance;
                }
            }
        }
        sm.nearEnemy = targetEnemy;
    }

    public void FindNearHeal() {
        if (sm.heals.Any()) {
            GameObject targetHeal = null;
            float distance = Mathf.Infinity;
            foreach (GameObject heal in sm.heals) {
                if (!sm.allies.Any() || !(sm.nearAlly.GetComponent<NPCAllySM>().nearHeal == heal && sm.nearAlly.GetComponent<NPCAllySM>().life <= sm.life)) {
                    float currentDistance = (heal.transform.position - sm.tf.position).sqrMagnitude;
                    if (currentDistance < distance) {
                        targetHeal = heal;
                        distance = currentDistance;
                    }
                }
            }
            sm.nearHeal = targetHeal;
        }
    }

    public void AllyDistance() {
        float avrgDistance = 0;
        float closeDistance = 0;
        foreach (GameObject ally in sm.allies) {
            float currentDistance = (ally.transform.position - sm.tf.position).sqrMagnitude;
            if (closeDistance == 0) {
                sm.nearAlly = ally;
                closeDistance = currentDistance;
            }
            avrgDistance += currentDistance;
            if (currentDistance < closeDistance) {
                sm.nearAlly = ally;
                closeDistance = currentDistance;
            }
        }
        if (avrgDistance > 0) {
            avrgDistance = avrgDistance/sm.allies.Count;
            closeDistance += 10;
        }

        if (avrgDistance > 50 || closeDistance < 2*avrgDistance/3) {
            sm.alliesDistance = closeDistance;
        } else {
            sm.alliesDistance = avrgDistance;
        }
    }
}

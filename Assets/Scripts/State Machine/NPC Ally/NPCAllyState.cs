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
        } else if (sm.life <= 30 && sm.heals.Any()) {
            stateMachine.ChangeState(sm.healState);
        } else if (sm.life <= 30 && !sm.heals.Any() && sm.alliesDistance > 30) {
            stateMachine.ChangeState(sm.groupState);
        } else if (sm.life < 90 && sm.allies.Any() && sm.alliesDistance > 50) {
            stateMachine.ChangeState(sm.groupState);
        } else {
            stateMachine.ChangeState(sm.farmingState);
        }
    }

    public void FindCurrentEnemy() {
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in sm.enemies) {
            if (!enemy.GetComponent<Collider2D>().isTrigger) {
                float currentDistance = (enemy.transform.position - sm.tf.position).sqrMagnitude;
                if (currentDistance < distance) {
                    sm.nearEnemy = enemy;
                    distance = currentDistance;
                }
            }
        }
    }

    public void FindNearHeal() {
        if (sm.heals.Any()) {
            float distance = Mathf.Infinity;
            foreach (GameObject heal in sm.heals) {
                float currentDistance = (heal.transform.position - sm.tf.position).sqrMagnitude;
                if (currentDistance < distance) {
                    sm.nearHeal = heal;
                    distance = currentDistance;
                }
            }
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

        if (avrgDistance > 50) {
            sm.alliesDistance = closeDistance;
        } else {
            sm.alliesDistance = avrgDistance;
        }
    }
}

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
        } else if (!sm.heals.Any()) {
            stateMachine.ChangeState(sm.farmingState);
        }
        if (sm.life > 70) {
            stateMachine.ChangeState(sm.farmingState);
        }
    }

    public void FindCurrentEnemy() {
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in sm.enemies) {
            float curDistance = (enemy.transform.position - sm.tf.position).sqrMagnitude;
            if (curDistance < distance) {
                sm.nearEnemy = enemy;
                distance = curDistance;
            }
        }
    }

    public void FindNearHeal() {
        if (sm.heals.Any()) {
            float distance = Mathf.Infinity;
            foreach (GameObject heal in sm.heals) {
                float curDistance = (heal.transform.position - sm.tf.position).sqrMagnitude;
                if (curDistance < distance) {
                    sm.nearHeal = heal;
                    distance = curDistance;
                }
            }
        }
    }
}

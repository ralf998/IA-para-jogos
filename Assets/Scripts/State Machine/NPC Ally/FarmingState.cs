using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Farming : NPCAlly {
    public Farming(NPCAllySM stateMachine) : base("Farming", stateMachine) {
        sm = (NPCAllySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(1f, 0.92f, 0.016f, 1f);
        FindCurrentEnemy();
    }

    public override void UpdateLogic() {
        FindCurrentEnemy();
        AllyDistance();
        FindNearHeal();
        if (sm.life <= 30 && sm.nearHeal != null) {
            stateMachine.ChangeState(sm.healState);
        } else if (sm.life <= 30 && sm.nearHeal == null && sm.alliesDistance > 30) {
            stateMachine.ChangeState(sm.groupState);
        } else if (sm.allies.Any() && sm.alliesDistance > 50) {
            stateMachine.ChangeState(sm.groupState);
        } else {
            //stateMachine.ChangeState(sm.deadState); andar sem rumo
            GameObject liveEnemy = null;
            foreach (GameObject enemy in sm.enemies) {
                if (!enemy.GetComponent<Collider2D>().isTrigger) {
                    liveEnemy = enemy;
                    break;
                }
            }
            if (liveEnemy == null) {
                stateMachine.ChangeState(sm.deadState);
            }
        }
        base.UpdateLogic();
    }

    public override void UpdatePhysics() {
        sm.rigidBody.velocity = sm.speed * (sm.nearEnemy.transform.position - sm.tf.position).normalized;
        base.UpdatePhysics();
    }
}

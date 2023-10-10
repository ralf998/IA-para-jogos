using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static System.Math;

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
            //stateMachine.ChangeState(sm.deadState); search/explore
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
        if (sm.nearHeal != null && (((sm.nearHeal.transform.position - sm.tf.position).sqrMagnitude < 10) && (Mathf.Abs(Mathf.Atan2(sm.rigidBody.velocity.x, sm.rigidBody.velocity.y)*Mathf.Rad2Deg - Mathf.Atan2(sm.nearHeal.transform.position.x, sm.nearHeal.transform.position.z)*Mathf.Rad2Deg) < 45))) {
            sm.rigidBody.velocity = sm.speed * (sm.rigidBody.velocity - new Vector2((sm.nearHeal.transform.position - sm.tf.position).x, (sm.nearHeal.transform.position - sm.tf.position).z).normalized).normalized;
        }
    }
}

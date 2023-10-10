using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Group : NPCAlly {
    public Group(NPCAllySM stateMachine) : base("Group", stateMachine) {
        sm = (NPCAllySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f);
    }

    public override void UpdateLogic() {
        AllyDistance();
        if (!sm.allies.Any()) {
            stateMachine.ChangeState(sm.farmingState);
        } else if (sm.life <= 30 && sm.heals.Any() && sm.alliesDistance < 50) {
            stateMachine.ChangeState(sm.healState);
        } else if (sm.alliesDistance < 20) {
            stateMachine.ChangeState(sm.farmingState);
        }
        base.UpdateLogic();
    }

    public override void UpdatePhysics() {
        sm.rigidBody.velocity = sm.speed * (sm.nearAlly.transform.position - sm.tf.position).normalized;
        base.UpdatePhysics();
        if (sm.nearHeal != null && (((sm.nearHeal.transform.position - sm.tf.position).sqrMagnitude < 10) && (Mathf.Abs(Mathf.Atan2(sm.rigidBody.velocity.x, sm.rigidBody.velocity.y)*Mathf.Rad2Deg - Mathf.Atan2(sm.nearHeal.transform.position.x, sm.nearHeal.transform.position.z)*Mathf.Rad2Deg) < 45))) {
            sm.rigidBody.velocity = sm.speed * (sm.rigidBody.velocity - new Vector2((sm.nearHeal.transform.position - sm.tf.position).x, (sm.nearHeal.transform.position - sm.tf.position).z).normalized).normalized;
        }
    }
}

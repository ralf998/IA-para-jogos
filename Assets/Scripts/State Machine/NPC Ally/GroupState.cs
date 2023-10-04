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
    }
}

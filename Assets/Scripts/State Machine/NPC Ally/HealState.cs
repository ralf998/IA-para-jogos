using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Heal : NPCAlly {
    public Heal(NPCAllySM stateMachine) : base("Heal", stateMachine) {
        sm = (NPCAllySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(1f, 0.46f, 0.008f, 1f);
        FindNearHeal();
    }

    public override void UpdateLogic() {
        FindNearHeal();
        if (sm.life > 30 || sm.nearHeal == null) {
            stateMachine.ChangeState(sm.farmingState);
        }
        base.UpdateLogic();
    }

    public override void UpdatePhysics() {
        sm.rigidBody.velocity = sm.speed * (sm.nearHeal.transform.position - sm.tf.position).normalized;
        base.UpdatePhysics();
    }
}
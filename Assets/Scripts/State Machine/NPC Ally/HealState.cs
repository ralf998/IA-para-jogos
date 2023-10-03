using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        base.UpdateLogic();
        FindNearHeal();
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        sm.rigidBody.velocity = sm.speed * (sm.nearHeal.transform.position - sm.tf.position).normalized;
    }
}
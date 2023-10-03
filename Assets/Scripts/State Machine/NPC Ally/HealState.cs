using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : NPCAlly {
    public Heal(NPCAllySM stateMachine) : base("Heal", stateMachine) {
        sm = (NPCAllySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
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
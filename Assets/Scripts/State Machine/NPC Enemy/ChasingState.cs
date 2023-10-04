using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : NPCEnemy {
    public Chasing(NPCEnemySM stateMachine) : base("Chasing", stateMachine) {
        sm = (NPCEnemySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        base.FindCurrentTarget();
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        
        sm.rigidBody.velocity = sm.speed * (sm.curTarget.transform.position - sm.tf.position).normalized;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : NPCEnemy {
    public Chasing(NPCEnemySM stateMachine) : base("Chasing", stateMachine) {
        sm = (NPCEnemySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
        sm.rigidBody.velocity = new Vector3(0,0,0);
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        sm.rigidBody.velocity = sm.speed * (sm.curTarget.transform.position - sm.tf.position).normalized;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : NPCAlly {
    public Farming(NPCAllySM stateMachine) : base("Farming", stateMachine) {
        sm = (NPCAllySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(1f, 0.92f, 0.016f, 1f);
        base.FindCurrentEnemy();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        base.FindCurrentEnemy();
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        sm.rigidBody.velocity = sm.speed * (sm.nearEnemy.transform.position - sm.tf.position).normalized;
    }
}

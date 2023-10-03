using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallen : NPCEnemy {
    public Fallen(NPCEnemySM stateMachine) : base("Fallen", stateMachine) {
        sm = (NPCEnemySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.2f);
        sm.rigidBody.GetComponent<Collider2D>().isTrigger = true;
    }

    public override void UpdateLogic() {}

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }

    public override void Exit() {
        base.Exit();
        sm.rigidBody.GetComponent<Collider2D>().isTrigger = false;
        sm.life = 30;
        sm.damage = 5;
    }
}

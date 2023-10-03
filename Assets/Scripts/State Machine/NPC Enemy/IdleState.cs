using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : NPCEnemy {
    public Idle(NPCEnemySM stateMachine) : base("Idle", stateMachine) {
        sm = (NPCEnemySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        base.FindCurrentTarget();
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }
}

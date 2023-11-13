using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : NPCEnemy {
    public Stun(NPCEnemySM stateMachine) : base("Stun", stateMachine) {
        sm = (NPCEnemySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        sm.Invoke("LeaveStun", 0.3f);
    }

    public override void UpdateLogic() {}

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }
}

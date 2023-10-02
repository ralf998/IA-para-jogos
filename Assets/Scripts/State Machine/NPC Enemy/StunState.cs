using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : NPCEnemy {
    public Stun(NPCEnemySM stateMachine) : base("Stun", stateMachine) {
        sm = (NPCEnemySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.velocity = new Vector3(0,0,0);
        sm.Invoke("LeaveStun", 5.0f);
    }

    public override void UpdateLogic() {}

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }
}

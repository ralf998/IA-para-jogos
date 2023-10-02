using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : NPCAlly {
    public Hit(NPCAllySM stateMachine) : base("Hit", stateMachine) {
        sm = (NPCAllySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.velocity = -sm.rigidBody.velocity/2;
        sm.Invoke("LeaveStun", 2.0f);
    }

    public override void UpdateLogic() {}

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }
}

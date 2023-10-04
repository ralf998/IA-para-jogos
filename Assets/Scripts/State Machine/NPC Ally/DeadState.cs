using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : NPCAlly {
    public Dead(NPCAllySM stateMachine) : base("Dead", stateMachine) {
        sm = (NPCAllySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.Die();
    }

    public override void UpdateLogic() {}

    public override void UpdatePhysics() {}
}

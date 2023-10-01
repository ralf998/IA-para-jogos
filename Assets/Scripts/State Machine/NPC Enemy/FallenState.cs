using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallen : NPCEnemy {
    public Fallen(NPCEnemySM stateMachine) : base("Fallen", stateMachine) {
        sm = (NPCEnemySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }
}

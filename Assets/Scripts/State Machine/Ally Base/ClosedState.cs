using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLosed : AllyBase {
    public CLosed(AllyBaseSM stateMachine) : base("CLosed", stateMachine) {
        sm = (AllyBaseSM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0.068f, 1f, 0f, 0.2745f);
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
    }

    public override void UpdatePhysics() {}
}

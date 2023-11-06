using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opened : AllyBase {
    public Opened(AllyBaseSM stateMachine) : base("Opened", stateMachine) {
        sm = (AllyBaseSM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.1f);
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
    }

    public override void UpdatePhysics() {}
}

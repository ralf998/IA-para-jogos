using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : AWall {
    public Broken(AWallSM stateMachine) : base("Broken", stateMachine) {
        sm = (AWallSM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0.49f, 0.49f, 0.49f, 0.1f);
        sm.rigidBody.GetComponent<Collider2D>().isTrigger = true;
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (sm.building >= 100) {
            sm.ChangeState(sm.builtState);
        }
    }

    public override void UpdatePhysics() {}

    public override void Exit() {
        sm.life = 300;
        sm.building = 0;
    }

    public override int build(int resources) {
        sm.building += resources;
        if (sm.building > 100) {
            resources = sm.building - 100;
            sm.building = 100;
        } else {
            resources = 0;
        }
        return resources;
    }
}

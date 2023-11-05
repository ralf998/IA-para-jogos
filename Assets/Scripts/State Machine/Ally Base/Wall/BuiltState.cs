using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Built : AWall {
    public Built(AWallSM stateMachine) : base("Built", stateMachine) {
        sm = (AWallSM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<Collider2D>().isTrigger = false;
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (sm.life <= 0) {
            sm.ChangeState(sm.brokenState);
        }
    }

    public override void UpdatePhysics() {}

    public override int build(int resources) {
        sm.life += 3*resources;
        if (sm.life > 300) {
            resources = (int) (sm.life-300)/3;
            sm.life = 300;
        } else {
            resources = 0;
        }
        return resources;
    }
}

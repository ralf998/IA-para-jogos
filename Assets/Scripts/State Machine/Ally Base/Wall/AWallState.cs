using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWall : BaseState {
    public AWallSM sm;
    
    public AWall(string name, AWallSM stateMachine) : base(name, stateMachine) {}

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

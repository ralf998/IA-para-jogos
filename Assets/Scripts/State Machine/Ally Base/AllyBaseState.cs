using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBase : BaseState {
    public AllyBaseSM sm;
    
    public AllyBase(string name, AllyBaseSM stateMachine) : base(name, stateMachine) {}

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

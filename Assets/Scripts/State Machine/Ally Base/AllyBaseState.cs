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
        if (sm.CheckWalls()) {
            sm.ChangeState(sm.closedState);
        } else {
            sm.ChangeState(sm.openedState);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }
}

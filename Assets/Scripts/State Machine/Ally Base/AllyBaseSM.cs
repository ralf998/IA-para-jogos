using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBaseSM : StateMachine {
    [HideInInspector]
    public AllyBase allyBaseState;

    private void Awake() {
        allyBaseState = new AllyBase("", this);
    }

    protected override BaseState GetInitialState() {
        return allyBaseState;
    }
}

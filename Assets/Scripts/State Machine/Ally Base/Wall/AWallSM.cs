using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWallSM : StateMachine {
    [HideInInspector]
    public AWall aWallState;
    
    public float life = 300;
    public int resources = 0;

    private void Awake() {
        aWallState = new AWall("", this);
    }

    protected override BaseState GetInitialState() {
        return aWallState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWallSM : StateMachine {
    [HideInInspector]
    public Built builtState;
    [HideInInspector]
    public Broken brokenState;
    
    public float life = 50;
    public int building = 0;

    public Rigidbody2D rigidBody;

    private void Awake() {
        builtState = new Built(this);
        brokenState = new Broken(this);
    }

    protected override BaseState GetInitialState() {
        return builtState;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (collisionInfo.gameObject.tag == "Enemy") {
            life -= collisionInfo.gameObject.GetComponent<NPCEnemySM>().damage;
        }
    }

    public int Fixes(int resources) {
        if (currentState is AWall) {
            AWall cState = (AWall) currentState;
            return cState.build(resources);
        } else {
            return resources;
        }
    }
}

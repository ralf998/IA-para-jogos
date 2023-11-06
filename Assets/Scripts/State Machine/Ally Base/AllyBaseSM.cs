using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBaseSM : StateMachine {
    [HideInInspector]
    public CLosed closedState;
    [HideInInspector]
    public Opened openedState;

    public List<GameObject> walls;

    public Rigidbody2D rigidBody;

    private void Awake() {
        closedState = new CLosed(this);
        openedState = new Opened(this);
    }

    protected override BaseState GetInitialState() {
        return closedState;
    }

    public bool CheckWalls() {
        bool constructed = true;
        foreach (GameObject wall in walls) {
            if (wall.GetComponent<AWallSM>().currentState.name == "Broken") {
                constructed = false;
                break;
            }
        }

        return constructed;
    }
}

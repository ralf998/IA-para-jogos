using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : NPCAlly {
    public Hit(NPCAllySM stateMachine) : base("Hit", stateMachine) {
        sm = (NPCAllySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        sm.Invoke("LeaveStun", 0.2f);
    }

    public override void UpdateLogic() {}

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }
}

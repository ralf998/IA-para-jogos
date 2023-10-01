using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAlly : BaseState {
    public NPCAllySM sm;
    
    public NPCAlly(string name, NPCAllySM stateMachine) : base(name, stateMachine) {}

    public override void Enter() {
        base.Enter();
        sm.rigidBody.velocity = new Vector3(0,0,0);
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (sm.life <= 0) {
            stateMachine.ChangeState(sm.deadState);
        }
    }

    public void FindClosestEnemy() {
        float distance = Mathf.Infinity;
        foreach (GameObject go in sm.enemies) {
            Vector3 diff = go.transform.position - sm.tf.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                sm.nearEnemy = go;
                distance = curDistance;
            }
        }
    }
}

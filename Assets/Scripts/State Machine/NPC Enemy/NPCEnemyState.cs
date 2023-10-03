using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemy : BaseState {
    public NPCEnemySM sm;
    
    public NPCEnemy(string name, NPCEnemySM stateMachine) : base(name, stateMachine) {}

    public override void Enter() {
        base.Enter();
        sm.rigidBody.velocity = new Vector3(0,0,0);
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (sm.curTarget == null) {
            stateMachine.ChangeState(sm.fallenState);//idle
        }
        if (sm.life <= 0) {
            stateMachine.ChangeState(sm.fallenState);
        }
    }

    public void FindCurrentTarget() {
        float distance = Mathf.Infinity;
        float tarLife = 100;
        foreach (GameObject target in sm.targets) {
            float tarDistance = (target.transform.position - sm.tf.position).sqrMagnitude;
            if (target.GetComponent<NPCAllySM>().life < 20) {
                if (tarDistance < distance) {
                    sm.curTarget = target;
                    distance = tarDistance;
                    tarLife = target.GetComponent<NPCAllySM>().life;
                }
            } else if (tarLife > 20) {
                sm.curTarget = target;
                distance = tarDistance;
                tarLife = target.GetComponent<NPCAllySM>().life;
            }
        }
    }
}

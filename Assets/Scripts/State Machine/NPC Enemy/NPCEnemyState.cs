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
        FindCurrentTarget();
        if (sm.life <= 0) {
            stateMachine.ChangeState(sm.fallenState);
        } else {
            if (sm.curTarget == null) {
                stateMachine.ChangeState(sm.idleState);
            } else {
                stateMachine.ChangeState(sm.chasingState);
            }
        }
    }

    public void FindCurrentTarget() {
        GameObject inRangeTarget = null;
        float distance = 20;
        float tarLife = 100;
        foreach (GameObject target in sm.targets) {
            float tarDistance = (target.transform.position - sm.tf.position).sqrMagnitude;
            if (tarDistance < distance) {
                if (target.GetComponent<NPCAllySM>().life < 20) {
                    inRangeTarget = target;
                    distance = tarDistance;
                    tarLife = target.GetComponent<NPCAllySM>().life;
                } else if (tarLife > 20) {
                    inRangeTarget = target;
                    distance = tarDistance;
                    tarLife = target.GetComponent<NPCAllySM>().life;
                }
            }
        }
        sm.curTarget = inRangeTarget;
    }
}

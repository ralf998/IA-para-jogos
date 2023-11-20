using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : NPCEnemy {
    public Chasing(NPCEnemySM stateMachine) : base("Chasing", stateMachine) {
        sm = (NPCEnemySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
        sm.rigidBody.velocity = new Vector3(0,0,0);
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        //sm.rigidBody.velocity = sm.speed * (sm.curTarget.transform.position - sm.tf.position).normalized;

        List<GridNode> point = sm.aStar.GetPath();
        if (Vector3.Distance(sm.tf.position, sm.aStar.target.position) > 0.01f)
        {
            sm.tf.position = Vector3.MoveTowards(
                sm.tf.position, point[0].worldPosition, sm.speed * Time.deltaTime);
        }
    }
}

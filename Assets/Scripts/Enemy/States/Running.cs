using UnityEngine;

public class Running : State
{
    EnemyAI AI;
    public Running(StateMachine fsm)
    {
        AI = fsm as EnemyAI;
    }

    public override void OnEnter()
    {
        AI.aiPath.canMove = true;

        AI.rb.velocity *= 0;
        AI.rb.angularVelocity *= 0;
    }

    public override void OnExit()
    {
        AI.aiPath.canMove = false;
    }
}

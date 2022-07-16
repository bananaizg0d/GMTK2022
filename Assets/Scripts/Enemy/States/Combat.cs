using System.Collections;
using UnityEngine;

public class Combat : State
{
    EnemyAI AI;
    Coroutine attacking;

    public Combat(StateMachine fsm)
    {
        AI = fsm as EnemyAI;
    }

    public override void OnEnter()
    {
        attacking = AI.StartCoroutine(StartAttacking());
    }

    public override void OnUpdate()
    {
        var dir = (AI.target.position - AI.transform.position).normalized;
        AI.transform.up = dir;
    }

    public override void OnExit()
    {
        if (attacking != null)
            AI.StopCoroutine(attacking);
    }

    IEnumerator StartAttacking()
    {
        while (true)
        {
            AI.Attack();
            yield return new WaitForSeconds(AI.delayBetweenAttacks);
        }
    }
}

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
        AI.transform.LookAt(AI.target);
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

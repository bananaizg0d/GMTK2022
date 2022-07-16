using UnityEngine;
using System;

public class AIManager : MonoBehaviour
{
    public Health target;
    public Action onEnemiesDie;
    bool areEnemiesDead;

    void Update()
    {
        if (transform.childCount == 0 && !areEnemiesDead)
        {
            areEnemiesDead = true;
            onEnemiesDie?.Invoke();
        }
    }

    public Transform FindPlayer()
    {
        var player = GameObject.FindWithTag(TopDownMovement.PLAYERTAG);

        if (player != null)
            if (player.TryGetComponent(out target))
                return target.transform;
        
        return null;
    }
}

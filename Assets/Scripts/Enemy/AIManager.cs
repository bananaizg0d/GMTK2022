using UnityEngine;
using System;

public class AIManager : MonoBehaviour
{
    public Health target;
    public Action onEnemiesDie;
    bool areEnemiesDead;

    public bool canFollowPlayer = false;

    SafeZone sz;

    void Start()
    {
        sz = GameObject.FindWithTag(SafeZone.TAG).GetComponent<SafeZone>();

        sz.onSafeZoneOut += EnablePlayerFollow;
    }

    void EnablePlayerFollow()
    {
        canFollowPlayer = true;
        sz.onSafeZoneOut -= EnablePlayerFollow;
    }

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

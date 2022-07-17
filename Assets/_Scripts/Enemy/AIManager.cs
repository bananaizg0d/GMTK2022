using UnityEngine;
using System;

public class AIManager : MonoBehaviour
{
    public const string TAG = "AIManager";
    public Health target;
    public Action onEnemiesDie;

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

    public Transform FindPlayer()
    {
        var player = GameObject.FindWithTag(TopDownMovement.PLAYERTAG);

        if (player != null)
            if (player.TryGetComponent(out target))
                return target.transform;
        
        return null;
    }

    public void KillClosestEnemy(Vector2 pos)
    {
        Transform closest = null;
        float closestDist = Mathf.Infinity;
        foreach(Transform t in transform)
        {
            var dist = Vector2.Distance(pos, t.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = t;
            }
        }

        if (closest != null)
            closest.GetComponent<Health>().Kill();
    }

    public void MakeInvulnirable(int count, float time)
    {
        int ind = 0;
        foreach(Transform t in transform)
        {
            if (ind > count)
                break;

            t.GetComponent<Health>().MakeInvulnirable(time);
            ind++;
        }
    }
}

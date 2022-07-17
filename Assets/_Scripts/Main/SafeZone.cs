using System;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public const string TAG = "SafeZone";
    public Action onSafeZoneOut;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TopDownMovement.PLAYERTAG)
        {
            onSafeZoneOut?.Invoke();
            Destroy(gameObject);
        }
    }
}

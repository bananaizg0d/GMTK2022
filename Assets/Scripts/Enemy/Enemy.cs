using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] AIPath aiPath;
    public void OnDamage()
    {
        aiPath.canMove = false;
        Invoke(nameof(CanMove), 0.5f);
    }

    void CanMove()
    {
        aiPath.canMove = true;
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
}

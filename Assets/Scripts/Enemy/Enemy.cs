using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] AIPath aiPath;
    [SerializeField] float stunOnDamageTime;
    public void OnDamage()
    {
        aiPath.canMove = false;
        Invoke(nameof(CanMove), stunOnDamageTime);
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

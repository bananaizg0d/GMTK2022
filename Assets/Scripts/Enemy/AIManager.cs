using UnityEngine;

public class AIManager : MonoBehaviour
{
    public Health target;

    public Transform FindPlayer()
    {
        var player = GameObject.FindWithTag(TopDownMovement.PLAYERTAG);

        if (player != null)
            if (player.TryGetComponent(out target))
                return target.transform;
        
        return null;
    }
}

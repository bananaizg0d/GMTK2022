using UnityEngine;

public class Hitbox : MonoBehaviour, IBuffable
{
    [SerializeField] CapsuleCollider2D mainCollider;

    Vector2 normalSize;

    public void Buff(float value, float time)
    {
        normalSize = mainCollider.size;
        mainCollider.size *= value;
        Invoke(nameof(SetNormalModifier), time);
    }

    public void SetNormalModifier()
    {
        mainCollider.size = normalSize;
    }
}

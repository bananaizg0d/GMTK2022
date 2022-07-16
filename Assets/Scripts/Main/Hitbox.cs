using UnityEngine;

public class Hitbox : MonoBehaviour, IBuffable
{
    [SerializeField] CapsuleCollider2D mainCollider;
    [SerializeField] Transform GFX;

    Vector2 ccNormalSize;
    Vector2 gfxNormalSize;

    public void Buff(float value, float time)
    {
        ccNormalSize = mainCollider.size;
        gfxNormalSize = GFX.localScale;
        mainCollider.size *= value;
        GFX.localScale *= value;

        Invoke(nameof(SetNormalModifier), time);
    }

    public void SetNormalModifier()
    {
        mainCollider.size = ccNormalSize;
        GFX.localScale = gfxNormalSize;
    }
}

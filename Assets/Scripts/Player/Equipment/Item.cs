using UnityEngine;
using UnityEngine.Animations;

public class Item : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] float tossAngularVelocity;
    [SerializeField] float holdingOffsetX;

    protected Collider2D col;
    protected EquipmentSystem character;
    internal GameObject hintObject;

    protected virtual void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    public void WasEquippedBy(EquipmentSystem character)
    {
        this.character = character;
        col.enabled = false;
        transform.SetParent(character.itemHolder.transform);
        transform.localPosition = Vector2.zero.WhereX(holdingOffsetX);
        transform.localEulerAngles = Vector2.zero;
        OnEquip();
    }
    public void WasTossedAway()
    {
        OnToss();
        col.enabled = true;
        transform.SetParent(null);
        transform.position = character.transform.position.AddTo(y: -1);
        transform.rotation = Quaternion.identity;
        character = null;
    }
    protected virtual void OnEquip()
    {
        character.animator.SetBool("isHoldingBox", true);
    }
    protected virtual void OnToss()
    {
        character.animator.SetBool("isHoldingBox", false);
    }
    public virtual void Use() { }
    public virtual void StopUsing() { }

    public virtual void Aim(Vector2 aimDirection) { }
}

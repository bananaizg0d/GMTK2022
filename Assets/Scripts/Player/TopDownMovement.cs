using UnityEngine;

public class TopDownMovement : MonoBehaviour, IBuffable
{
    public const string PLAYERTAG = "Player";

    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody2D rb;

    float Modifier = 1;

    Vector2 movementInput;

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;
    }

    public void Buff(float newModifier, float time)
    {
        Modifier = newModifier;
        Invoke(nameof(SetNormalModifier), time);
    }

    public void SetNormalModifier()
    {
        Modifier = 1;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * movementSpeed * Modifier * Time.deltaTime);
    }
}

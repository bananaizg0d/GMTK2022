using UnityEngine;

public class TopDownMovement : MonoBehaviour, IBuffable
{
    public const string PLAYERTAG = "Player";

    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody2D rb;

    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;
    bool canDash = true;
    bool isDashing;

    float Modifier = 1;

    Vector2 movementInput;

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;

        if (canDash && Input.GetKeyDown(KeyCode.Space))
            Dash();
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
        if (isDashing)
            return;

        rb.MovePosition(rb.position + movementInput * movementSpeed * Modifier * Time.deltaTime);
    }

    void Dash()
    {
        canDash = false;
        isDashing = true;
        var dashDir = movementInput;
        if (movementInput.magnitude < 0.001f)
            dashDir = Vector2.up;
        rb.velocity = dashDir * dashSpeed;
        Invoke(nameof(StopDash), dashTime);
    }

    void StopDash()
    {
        rb.velocity *= 0;
        isDashing = false;

        Invoke(nameof(EnableDash), dashCooldown);
    }

    void EnableDash()
    {
        canDash = true;
    }
}

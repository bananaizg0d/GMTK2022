using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody2D rb;


    Vector2 movementInput;

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * movementSpeed * Time.deltaTime);
    }
}

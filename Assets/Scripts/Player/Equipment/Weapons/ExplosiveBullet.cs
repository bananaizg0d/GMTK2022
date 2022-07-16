using UnityEngine;
public class ExplosiveBullet : Bullet
{
    [SerializeField] float explosionRadius, explosionForce;
    [SerializeField] LayerMask explosiveLayers;
    
    protected override void OnHitObstacle(Collider2D _)
    {
        var collisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosiveLayers);

        Rigidbody2D rbToPush;
        Vector2 dir;
        float distanceModifier;

        foreach (var coll in collisions)
        {
            if (coll.attachedRigidbody == null || coll.gameObject == gameObject)
                continue;

            if (coll.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }

            rbToPush = coll.attachedRigidbody;
            dir = (rbToPush.position - rb.position).normalized;
            distanceModifier = Mathf.Clamp(1 / Vector2.Distance(rb.position, rbToPush.position), 0.3f, 1);

            rbToPush.AddForce(dir * explosionForce * distanceModifier, ForceMode2D.Impulse);
        }
    }
}

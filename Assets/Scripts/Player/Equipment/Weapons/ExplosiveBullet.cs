using UnityEngine;
public class ExplosiveBullet : Bullet
{
    [SerializeField] float explosionRadius, explosionForce;
    [SerializeField] LayerMask explosiveLayers;
    
    protected override bool RegisterHit(Collider2D _)
    {
        var collisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosiveLayers);

        Rigidbody2D rbToPush;

        foreach (var coll in collisions)
        {
            if (coll.gameObject == gameObject)
                continue;

            if (coll.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }

            Push(coll.attachedRigidbody);
        }

        return true;
    }

    public void Push(Rigidbody2D rbToPush)
    {
        if (rbToPush == null)
            return;

        Vector2 dir = (rbToPush.position - rb.position).normalized;
        float distanceModifier = Mathf.Clamp(1 / Vector2.Distance(rb.position, rbToPush.position), 0.3f, 1);

        rbToPush.AddForce(dir * explosionForce * distanceModifier, ForceMode2D.Impulse);
    }
}

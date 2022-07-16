using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem onDestroyEffect, onDamageEffect;
    [SerializeField] AudioClip onDestroySound, onDamageSound;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] AudioSource audioSrc;

    protected EquipmentSystem holder;

    protected int damage = 10;
    protected float speed = 30f;

    bool collided;

    void Start()
    {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        StartCoroutine(DestroyWhenTooFar());
    }

    public void Init(EquipmentSystem holder, int damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
        this.holder = holder;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger || collided)
            return;

        collision.TryGetComponent(out EquipmentSystem es);

        if (es == holder)
            return;

        collided = true;

        if (spriteRend != null)
            spriteRend.enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        transform.SetParent(collision.transform);

        ParticleSystem particleSystem;
        AudioClip soundEffect;

        OnHitObstacle(collision);

        //StartCoroutine(PlayEffects(particleSystem, soundEffect));

        Destroy(gameObject);
    }

    protected virtual void OnHitObstacle(Collider2D collision)
    {
        if (collision.TryGetComponent(out Rigidbody2D otherRB))
        {
            otherRB.AddForce(transform.right * (speed / 5), ForceMode2D.Impulse);
        }

        if (collision.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }
    }

    IEnumerator PlayEffects(ParticleSystem particleSystem, AudioClip soundEffect)
    {
        particleSystem.Play();
        audioSrc.clip = soundEffect;
        audioSrc.Play();

        yield return null;
        yield return new WaitWhile(() => particleSystem.isPlaying);

        Destroy(gameObject);
    }

    IEnumerator DestroyWhenTooFar()
    {
        yield return new WaitUntil(() => {
            var vp = Camera.main.WorldToViewportPoint(transform.position);
            if (vp.x < -3 || vp.y < -3 || vp.y > 3 || vp.x > 3)
                return true;
            return false;
        });

        Destroy(gameObject);
    }
}

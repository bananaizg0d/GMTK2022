using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    public EquipmentSystem holder;
    
    [SerializeField] ParticleSystem onDestroyEffect, onDamageEffect;
    [SerializeField] AudioClip onDestroySound, onDamageSound;

    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] AudioSource audioSrc;

    [HideInInspector] public int damage = 10;
    [HideInInspector] public float speed = 30f;
    [HideInInspector] public float gravityScale = 0f;

    bool collided;

    void Start()
    {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        rb.gravityScale = gravityScale;
        StartCoroutine(DestroyWhenNotSeen());
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

        if (collision.TryGetComponent(out Rigidbody2D otherRB))
        {
            otherRB.AddForce(transform.right * speed / otherRB.gravityScale, ForceMode2D.Impulse);
        }

        if (collision.TryGetComponent(out Health health) && health.isStatic == false)
        {
            health.TakeDamage(damage);
            particleSystem = onDamageEffect;
            soundEffect = onDamageSound;
        }
        else
        {
            if (health != null && health.isStatic)
            {
                health.TakeDamage(damage);
            }
            particleSystem = onDestroyEffect;
            soundEffect = onDestroySound;
        }
        //StartCoroutine(PlayEffects(particleSystem, soundEffect));
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

    IEnumerator DestroyWhenNotSeen()
    {
        yield return new WaitUntil(() => {
            var vp = Camera.main.WorldToViewportPoint(transform.position);
            if (vp.x < 0 || vp.y < 0 || vp.y > 1 || vp.x > 1)
                return true;
            return false;
        });

        Destroy(gameObject);
    }
}

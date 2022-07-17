using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip onStepSound, activationSound;

    List<Health> healthToDamage;

    void Awake()
    {
        healthToDamage = new List<Health>();
    }

    void Update()
    {
        animator.SetBool("IsStandingOnSpikes", healthToDamage.Count > 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            if (healthToDamage.Count == 0)
            {
                animator.SetBool("IsStandingOnSpikes", true);
                src.PlayOneShot(onStepSound);
            }
            healthToDamage.Add(health);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var health in healthToDamage)
        {
            if (collision.transform == health.transform)
            {
                healthToDamage.Remove(health);
            }
        }

        if (healthToDamage.Count == 0)
        {
            animator.SetBool("IsStandingOnSpikes", false);
        }
    }

    public void AELDealDamage()
    {
        src.PlayOneShot(activationSound);
        foreach(var health in healthToDamage)
        {
            health.TakeDamage(damage);
        }
    }

    public void AELSpikesAudio()
    {
        src.Play();
    }
}

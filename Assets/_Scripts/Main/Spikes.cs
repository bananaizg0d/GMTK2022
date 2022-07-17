using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip onStepSound, activationSound;

    int currentNum;

    void Update()
    {
        animator.SetBool("IsStandingOnSpikes", currentNum > 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
            src.Play();
            currentNum++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            currentNum--;
            if (currentNum <= 0)
            {
                animator.SetBool("IsStandingOnSpikes", false);
            }
        }
    }
}

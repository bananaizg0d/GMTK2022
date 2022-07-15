using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : Item
{
    [Header("Stats")]
    [SerializeField] protected int bullets;
    [SerializeField] float maxAimAngle;
    [SerializeField] int damage;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletGravityScale;


    protected int currentBullets;

    [Header("References")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] ParticleSystem shootEffect;

    [Header("Audio")]
    [SerializeField] protected AudioClip shootSFX;
    [SerializeField] protected AudioClip emptyMagSFX;
    [SerializeField] float pitchRandomness;


    protected AudioSource src;
    SpriteRenderer GFX;
    Coroutine recoilRoutine;

    public int CurrentBullets => currentBullets;

    protected override void Awake()
    {
        base.Awake();
        GFX = GetComponentInChildren<SpriteRenderer>();
        src = GetComponent<AudioSource>();
        currentBullets = bullets;
    }

    protected void SingleShot()
    {
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation, null);
        var bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.speed = bulletSpeed;
        bulletComponent.gravityScale = bulletGravityScale;
        bulletComponent.damage = damage;
        bulletComponent.holder = character;
        if (recoilRoutine != null) StopCoroutine(recoilRoutine);
        currentBullets -= 1;
        PlayShootEffects();
    }

    protected virtual void PlayShootEffects()
    {
        shootEffect.Play();
        var initialPitch = src.pitch;
        src.pitch = src.pitch + Random.Range(-pitchRandomness, pitchRandomness);
        src.PlayOneShot(shootSFX);
        src.pitch = initialPitch;
    }

    public override void Aim(Vector2 aimPoint)
    {
        var dir = aimPoint - (Vector2)transform.position;
        transform.right = dir;
    }
    protected override void OnToss()
    {
        StopAllCoroutines();
    }
}

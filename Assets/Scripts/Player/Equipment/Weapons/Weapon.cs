using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : Item
{
    [Header("Weapon")]
    [SerializeField] protected WeaponStatsSO stats;
    [SerializeField] Transform shootingPoint;
    [SerializeField] ParticleSystem shootEffect;

    AudioSource src;
    SpriteRenderer GFX;
    protected Coroutine shooting;

    protected override void Awake()
    {
        base.Awake();
        GFX = GetComponentInChildren<SpriteRenderer>();
        src = GetComponent<AudioSource>();
    }

    protected void SingleShot()
    {
        var bullet = Instantiate(stats.bulletPrefab, shootingPoint.position, shootingPoint.rotation, null);
        var bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.Init(character.gameObject, stats.damage, stats.bulletSpeed, character.Modifier);

        PlayShootEffects();
    }

    protected virtual void PlayShootEffects()
    {
        shootEffect?.Play();
        var initialPitch = src.pitch;
        src.pitch = src.pitch + Random.Range(-stats.pitchRandomness, stats.pitchRandomness);
        //remove comment after added audioclip
        //src.PlayOneShot(stats.shootSFX);
        src.pitch = initialPitch;
    }

    public override void Aim(Vector2 aimPoint)
    {
        var dir = aimPoint - (Vector2)transform.position;
        transform.right = dir;
    }

    public override void WasTossedAway()
    {
        base.WasTossedAway();
        StopAllCoroutines();
    }

    public override void Use()
    {
        if (!stats.isAutomatic)
        {
            SingleShot();
            return;
        }

        if (shooting != null) return;

        shooting = StartCoroutine(AutoShoot(stats.delayBetweenShots));
    }

    public override void StopUsing()
    {
        if (shooting != null)
            StopCoroutine(shooting);
        shooting = null;
    }

    IEnumerator AutoShoot(float delay)
    {
        while (true)
        {
            SingleShot();
            src.PlayOneShot(stats.shootSFX);
            yield return new WaitForSeconds(delay);
        }
    }
}

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

    bool canShoot = true;

    protected override void Awake()
    {
        base.Awake();
        GFX = GetComponentInChildren<SpriteRenderer>();
        src = GetComponent<AudioSource>();
    }

    protected void SingleShot()
    {
        if (canShoot == false && !stats.isAutomatic)
            return;

        canShoot = false;
        Invoke(nameof(EnableShooting), stats.delayBetweenShotsNonAuto);

        var bullet = Instantiate(stats.bulletPrefab, shootingPoint.position, shootingPoint.rotation, null);
        var bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.Init(character.gameObject, stats.damage, stats.bulletSpeed, character.Modifier);

        PlayShootEffects();
    }

    void EnableShooting()
    {
        canShoot = true;
    }

    void ShotgunShot()
    {
        if (canShoot == false && !stats.isAutomatic)
            return;

        canShoot = false;
        Invoke(nameof(EnableShooting), stats.delayBetweenShotsNonAuto);

        GameObject bullet;
        Bullet bulletComp;
        for (int i = 0; i < stats.shotgunMaxBulletsPershot; i++)
        {
            bullet = Instantiate(stats.bulletPrefab, shootingPoint.position, shootingPoint.rotation, null);
            bullet.transform.Rotate(Vector3.forward, (i - Mathf.FloorToInt(stats.shotgunMaxBulletsPershot * 0.5f)) * stats.shotgunSpreadAngle);
            bulletComp = bullet.GetComponent<Bullet>();
            bulletComp.Init(character.gameObject, stats.damage, stats.bulletSpeed, character.Modifier);

            PlayShootEffects();
        }
    }

    protected virtual void PlayShootEffects()
    {
        shootEffect?.Play();
        var initialPitch = src.pitch;
        src.pitch = src.pitch + Random.Range(-stats.pitchRandomness, stats.pitchRandomness);
        //remove comment after added audioclip
        src.PlayOneShot(stats.shootSFX);
        src.pitch = initialPitch;
    }

    public override void Aim(Vector2 aimPoint)
    {

    }

    public override void WasTossedAway()
    {
        base.WasTossedAway();
        StopAllCoroutines();
    }

    public override void Use()
    {
        if (stats.isShotgun)
        {
            ShotgunShot();
            return;
        }

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

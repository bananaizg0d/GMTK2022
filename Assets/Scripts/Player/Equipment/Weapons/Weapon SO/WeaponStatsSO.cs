using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "SO/Weapon/WeaponStats")]
public class WeaponStatsSO : ScriptableObject
{
    public GameObject bulletPrefab;

    [Header("Stats")]
    public ParticleSystem sus;
    public bool isAutomatic;
    public float delayBetweenShots;
    public int damage;
    public float bulletSpeed;
    public float bulletGravityScale;

    [Header("Audio")]
    public AudioClip shootSFX;
    public AudioClip emptyMagSFX;
    public float pitchRandomness;
}

using UnityEngine;
using System.Collections.Generic;

public class RandomWeapon : Weapon
{
    public List<WeaponStatsSO> gunTypes;
    [SerializeField] float delay;

    public override void WasEquippedBy(EquipmentSystem character)
    {
        base.WasEquippedBy(character);

        InvokeRepeating(nameof(Randomize), delay, delay);
    }

    void Randomize()
    {
        var randomStat = gunTypes[Random.Range(0, gunTypes.Count)];

        ChangeStats(randomStat);
    }

    void ChangeStats(WeaponStatsSO newStats)
    {
        stats = newStats;

        if (shooting != null)
            StopCoroutine(shooting);

        Use();
    }
}

using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float buffTime;
    [Space]
    [Header("Speed")]
    [SerializeField] float speedBuffValue;
    [SerializeField] float speedDebuffValue;
    [Header("Damage")]
    [SerializeField] float damageBuffValue;
    [SerializeField] float damageDebuffValue;
    [Header("HitBox")]
    [SerializeField] float hitboxBuffValue;
    [SerializeField] float hitboxDebuffValue;
    [Header("Bullets Around")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int bullets;
    [SerializeField] float spreadAngle;
    [SerializeField] int damage;
    [SerializeField] float bulletSpeed;
    [SerializeField] float shootingDelay;
    [SerializeField] float bulletBuffTime;
    [Header("Kill enemies")]



    //player components
    [Header("Player Components")]
    [SerializeField] EquipmentSystem es;
    [SerializeField] TopDownMovement movement;
    [SerializeField] Hitbox hitbox;
    [SerializeField] Health playerHealth;


    AIManager aiManager;
    /*
     0 = speed
     1 = damage
     2 = hitbox
     3... later
     */

    void Start()
    {
        aiManager = GameObject.FindWithTag(AIManager.TAG).GetComponent<AIManager>();
    }

    public void ChoosePowerUp(int buffType, bool isBuff)
    {
        IBuffable componentToBuff;
        float buffValue;

        if (es == null || movement == null || hitbox == null) 
        {
            Debug.LogWarning("Scripts are not attached");
            return;
        }

        switch (buffType)
        {
            case 0:
                componentToBuff = movement;
                buffValue = speedBuffValue;
                if (!isBuff)
                    buffValue = speedDebuffValue;
                componentToBuff.Buff(buffValue, buffTime);
                break;
            case 1:
                componentToBuff = es;
                buffValue = damageBuffValue;
                if (!isBuff)
                    buffValue = damageDebuffValue;
                componentToBuff.Buff(buffValue, buffTime);
                break;
            case 2:
                componentToBuff = hitbox;

                buffValue = hitboxBuffValue;
                if (!isBuff)
                    buffValue = hitboxDebuffValue;
                componentToBuff.Buff(buffValue, buffTime);
                break;
            case 3:
                if (isBuff)
                    StartCoroutine(KillEnemies());
                else
                    aiManager.MakeInvulnirable(3, 5);
                break;
            case 4:
                playerHealth.MakeInvulnirable(buffTime);
                break;
            case 5:
                StartCoroutine(ShootInEveryDirection());
                break;
            default:
                return;
        }

        /*
         speed up - 5 s
         damage up - 5s
         shoot in every dir - 5 s
         unlimited dash/ defenesive - 5s
         hitbox decreased
         */
    }

    IEnumerator KillEnemies()
    {
        float curtime = 0;

        while (curtime <= buffTime)
        {
            aiManager.KillClosestEnemy(transform.position);
            yield return new WaitForSeconds(1);
            curtime += 1;
        }
    }

    IEnumerator ShootInEveryDirection()
    {
        float curTime = 0;

        while(curTime < bulletBuffTime)
        {
            ShotgunShot();
            yield return new WaitForSeconds(shootingDelay);
            curTime += shootingDelay;
        }
    }

    void ShotgunShot()
    {
        GameObject bullet;
        Bullet bulletComp;
        for (int i = 0; i < bullets; i++)
        {
            bullet = Instantiate(bulletPrefab, transform.position, transform.rotation, null);
            bullet.transform.Rotate(Vector3.forward, (i - Mathf.FloorToInt(bullets * 0.5f)) * spreadAngle);
            bulletComp = bullet.GetComponent<Bullet>();
            bulletComp.Init(transform.parent.gameObject, damage, bulletSpeed, 1);
        }
    }
}
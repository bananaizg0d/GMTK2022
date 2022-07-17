using UnityEngine;

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

    //player components
    [Header("Player Components")]
    [SerializeField] EquipmentSystem es;
    [SerializeField] TopDownMovement movement;
    [SerializeField] Hitbox hitbox;

    /*
     0 = speed
     1 = damage
     2 = hitbox
     3... later
     */

    public void ChoosePowerUp(int buff, bool isBuff)
    {
        IBuffable componentToBuff;
        float buffValue;

        string debugMessage = "";

        if (es == null || movement == null || hitbox == null) 
        {
            Debug.LogWarning("Scripts are not attached");
            return;
        }

        switch (buff)
        {
            case 0:
                debugMessage += "Speed";
                componentToBuff = movement;
                buffValue = speedBuffValue;
                if (!isBuff)
                    buffValue = speedDebuffValue;
                break;

            case 1:
                debugMessage += "Damage";
                componentToBuff = es;
                buffValue = damageBuffValue;
                if (!isBuff)
                    buffValue = damageDebuffValue;
                break;
            case 2:
                debugMessage += "HitBox";
                componentToBuff = hitbox;

                buffValue = hitboxBuffValue;
                if (!isBuff)
                    buffValue = hitboxDebuffValue;
                break;
            default:
                return;
        }

        if (isBuff)
            debugMessage += " Buff";
        else
            debugMessage += " Debuff";

        print(debugMessage);

        componentToBuff.Buff(buffValue, buffTime);

        /*
         speed up - 5 s
         damage up - 5s
         shoot in every dir - 5 s
         unlimited dash/ defenesive - 5s
         hitbox decreased
         */
    }
}
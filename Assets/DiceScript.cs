using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    public bool dicable = true;
    public bool diceHitable;

    public bool diceSucceded;

    public float diceCD = 5;
    public float diceStartZone = 1.5f;
    public float diceHitZone = 0.5f;
    public float diceCycle;

    public float powerRandomizer;

    private void Start()
    {
        diceCycle = diceCD + diceHitZone + diceStartZone;

        InvokeRepeating(nameof(DiceStart), diceCD, diceCycle);
        InvokeRepeating(nameof(CheckSuccess), diceCycle, diceCycle);
    }

    void Update()
    {
        if (diceHitable && Input.GetKeyDown(KeyCode.H))
        {
            diceSucceded = true;
        }
        //else if ()
        //{

        //}
    }

    public void LevelStart() => dicable = true;

    public void DiceStart()
    {
        Invoke(nameof(DiceHitZone), diceStartZone);
    }

    public void DiceHitZone()
    {
        Invoke(nameof(DiceEnd), diceHitZone);
        diceHitable = true;
    }

    public void DiceEnd()
    {
        diceHitable = false;
    }

    public void CheckSuccess()
    {
        if (diceSucceded)
        {
            Debug.LogError("success");
            Buff();
        }
        else if (!diceSucceded)
        {
            Debug.LogError("failed");
            Debuff();
        }
        diceSucceded = false;
    }

    private void Buff()
    {
        powerRandomizer = Random.Range(1, 6);

        if (powerRandomizer == 1)
        {
            //player speed up for 5 secs
        }
        if (powerRandomizer == 2)
        {
            //player damage/fire rate up for 5 secs
        }
        if (powerRandomizer == 3)
        {
            //shoot in every direction
        }
        if (powerRandomizer == 4)
        {
            //unlimited dash/defensive ability for 5 secs
        }
        if (powerRandomizer == 5)
        {
            //player hitbox decreased for 5 secs
        }
        if (powerRandomizer == 6)
        {
            //something cool
        }
    }

    private void Debuff()
    {
        powerRandomizer = Random.Range(1, 6);

        if (powerRandomizer == 1)
        {
            //player speed down for 5 secs
        }
        if (powerRandomizer == 2)
        {
            //player damage/fire rate down for 5 secs
        }
        if (powerRandomizer == 3)
        {
            //enemy bullets increase??
        }
        if (powerRandomizer == 4)
        {
            //no dash/defensive ability for 5 secs
        }
        if (powerRandomizer == 5)
        {
            //player hitbox increased for 5 secs
        }
        if (powerRandomizer == 6)
        {
            //something uncool
        }
    }
}

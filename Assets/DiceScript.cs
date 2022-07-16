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

    public float debugTimer;

    private void Start()
    {
        diceCycle = diceCD + diceHitZone + diceStartZone;

        StartCoroutine(DiceNumerator());
        StartCoroutine(Check4Success());

        //InvokeRepeating(nameof(DiceStart), diceCD, diceCycle);
        //InvokeRepeating(nameof(CheckSuccess), diceCycle, diceCycle);
    }

    void Update()
    {
        if (diceHitable && Input.GetKeyDown(KeyCode.H))
        {
            diceSucceded = true;
        }
        debugTimer += Time.deltaTime;
    }
    
    IEnumerator DiceNumerator()
    {
        yield return new WaitForSeconds(diceCD);
        while (true)
        {
            DiceStart();
            yield return new WaitForSeconds(diceCycle);
        }
    }
    IEnumerator Check4Success()
    {
        yield return new WaitForSeconds(diceCycle);
        while (true)
        {
            CheckSuccess();
            yield return new WaitForSeconds(diceCycle);
        }
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
        diceCD = Random.Range(4, 8);
        diceStartZone = Random.Range(1, 4);
        diceCycle = diceCD + diceHitZone + diceStartZone;

        debugTimer = 0;
    }

    private void Buff()
    {
        powerRandomizer = Random.Range(1, 6);

        if (powerRandomizer == 1)
        {
            //player speed up for 5 secs
        }
        else if (powerRandomizer == 2)
        {
            //player damage/fire rate up for 5 secs
        }
        else if (powerRandomizer == 3)
        {
            //shoot in every direction
        }
        else if (powerRandomizer == 4)
        {
            //unlimited dash/defensive ability for 5 secs
        }
        else if (powerRandomizer == 5)
        {
            //player hitbox decreased for 5 secs
        }
        else if (powerRandomizer == 6)
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
        else if (powerRandomizer == 2)
        {
            //player damage/fire rate down for 5 secs
        }
        else if (powerRandomizer == 3)
        {
            //damage taken increase
        }
        else if (powerRandomizer == 4)
        {
            //no dash/defensive ability for 5 secs
        }
        else if (powerRandomizer == 5)
        {
            //player hitbox increased for 5 secs
        }
        else if (powerRandomizer == 6)
        {
            //something uncool
        }


    }
}

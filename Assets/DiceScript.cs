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
            Debug.Log("success");
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
        Debug.Log("a");
    }

    public void DiceHitZone()
    {
        Invoke(nameof(DiceEnd), diceHitZone);
        diceHitable = true;
        Debug.Log("b");
    }

    public void DiceEnd()
    {
        diceHitable = false;
        Debug.Log("c");
    }

    public void CheckSuccess()
    {
        if (diceSucceded)
        {
            Debug.Log("success");
        }
        else if (!diceSucceded)
        {
            Debug.Log("failed");
        }
        diceSucceded = false;
    }
}

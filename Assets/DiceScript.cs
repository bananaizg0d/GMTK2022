using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    [SerializeField] PowerUp powerUp;

    public bool dicable = true;
    public bool diceHitable;

    public bool diceSucceded;

    public float diceCD = 5;
    public float diceStartZone = 1.5f;
    public float diceHitZone = 0.5f;
    public float diceCycle;

    public int powerRandomizer;

    public float debugTimer;

    private void Start()
    {
        diceCycle = diceCD + diceHitZone + diceStartZone;

        StartCoroutine(DiceNumerator());
        //StartCoroutine(Check4Success());

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
    //IEnumerator Check4Success()
    //{
    //    yield return new WaitForSeconds(diceCycle);
    //    while (true)
    //    {
    //        CheckSuccess();
    //        yield return new WaitForSeconds(diceCycle);
    //    }
    //}

    public void LevelStart() => dicable = true;

    public void DiceStart()
    {
        Invoke(nameof(DiceHitZone), diceStartZone);
        Debug.LogError("started");
    }

    public void DiceHitZone()
    {
        Invoke(nameof(DiceEnd), diceHitZone);
        diceHitable = true;
        Debug.LogError("hitZone");

    }

    public void DiceEnd()
    {
        diceHitable = false;
        CheckSuccess();
        Debug.LogError("end");
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
        powerRandomizer = Random.Range(0, 3);

        powerUp.ChoosePowerUp(powerRandomizer, true);
    }

    private void Debuff()
    {
        powerRandomizer = Random.Range(0, 3);

        powerUp.ChoosePowerUp(powerRandomizer, false);
    }
}

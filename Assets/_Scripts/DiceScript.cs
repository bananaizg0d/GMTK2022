using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    [SerializeField] PowerUp powerUp;
    [SerializeField] AudioSource src;

    public bool dicable = true;
    public bool diceHitable;

    public bool diceSucceded;

    public float diceCD = 5;
    public float diceStartZone = 1.5f;
    public float diceHitZone = 0.5f;
    public float diceCycle;

    public int powerRandomizer;

    public float debugTimer;

    RollingDiceVisual timeBar;

    private void Start()
    {
        var obj = GameObject.FindWithTag(RollingDiceVisual.TAG);
        timeBar = obj.GetComponent<RollingDiceVisual>();
        diceCycle = diceCD + diceHitZone + diceStartZone;
        StartCoroutine(DiceNumerator());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (diceHitable)
            {
                diceSucceded = true;
            }
            diceHitable = false;
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
        timeBar.OnDiceStart(diceHitZone + diceStartZone);
        Invoke(nameof(DiceHitZone), diceStartZone);

    }

    public void DiceHitZone()
    {
        src.Play();
        timeBar.OnHitZoneEnter(diceHitZone);
        Invoke(nameof(DiceEnd), diceHitZone);
        diceHitable = true;
    }

    public void DiceEnd()
    {
        diceHitable = false;
        CheckSuccess();

    }
    public void CheckSuccess()
    {
        if (diceSucceded)
        {
            Buff();
        }
        else if (!diceSucceded)
        {
            Debuff();
        }

        diceCD = Random.Range(4, 8);
        timeBar.OnCheckSuccess(diceCD, diceSucceded, powerRandomizer);

        diceSucceded = false;

        diceStartZone = Random.Range(1, 4);
        diceCycle = diceCD + diceHitZone + diceStartZone;

        debugTimer = 0;
    }

    private void Buff()
    {
        powerRandomizer = Random.Range(0, 6);

        powerUp.ChoosePowerUp(powerRandomizer, true);
    }

    private void Debuff()
    {
        powerRandomizer = Random.Range(0, 4);

        powerUp.ChoosePowerUp(powerRandomizer, false);
    }
}

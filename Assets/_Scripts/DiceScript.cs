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

    public GameObject DiceSlider;

    public float debugTimer;

    TimeBar timeBar;

    private void Start()
    {
        DiceSlider = GameObject.Find("DiceSlider");
        timeBar = DiceSlider.GetComponent<TimeBar>();

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
        timeBar.FillTimeBar(diceHitZone + diceStartZone);
        Invoke(nameof(DiceHitZone), diceStartZone);

    }

    public void DiceHitZone()
    {
        timeBar.OnHitZoneEnter();
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
        diceSucceded = false;
        diceCD = Random.Range(4, 8);

        timeBar.EmptyTimeBar(diceCD);
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

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

    private void Start()
    {
        //if (GameObject.Find("DiceSlider") != null)
        //{
        //    DiceSlider = GameObject.Find("DiceSlider");
        //}
        DiceSlider = GameObject.Find("DiceSlider");
        DiceSlider.GetComponent<TimeBar>().refillTime = diceStartZone + diceHitZone;
        DiceSlider.GetComponent<TimeBar>().countDownTime = diceCD;

        diceCycle = diceCD + diceHitZone + diceStartZone;
        StartCoroutine(DiceNumerator());
    }

    void Update()
    {
        if (diceHitable && Input.GetKeyDown(KeyCode.Mouse1))
        {
            diceSucceded = true;
        }
        debugTimer += Time.deltaTime;
    }
    
    IEnumerator DiceNumerator()
    {
        DiceSlider.GetComponent<TimeBar>().countDown = true;

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
        //Debug.LogError("started");
        DiceSlider.GetComponent<TimeBar>().countDown = false;

    }

    public void DiceHitZone()
    {
        Invoke(nameof(DiceEnd), diceHitZone);
        diceHitable = true;
        //Debug.LogError("hitZone");

    }

    public void DiceEnd()
    {
        diceHitable = false;
        CheckSuccess();
        //Debug.LogError("end");
        DiceSlider.GetComponent<TimeBar>().countDown = true;

    }

    public void CheckSuccess()
    {
        if (diceSucceded)
        {
            //Debug.LogError("success");
            Buff();
        }
        else if (!diceSucceded)
        {
            //Debug.LogError("failed");
            Debuff();
        }
        diceSucceded = false;
        diceCD = Random.Range(4, 8);
        diceStartZone = Random.Range(1, 4);
        diceCycle = diceCD + diceHitZone + diceStartZone;
        DiceSlider.GetComponent<TimeBar>().refillTime = diceStartZone + diceHitZone;
        DiceSlider.GetComponent<TimeBar>().countDownTime = diceCD;


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

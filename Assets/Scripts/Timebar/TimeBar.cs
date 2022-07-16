using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{

    public static bool allowInputs;

    public Slider countdownBar;
    public bool countDown = true;

    public float countDownTime = 8;
    public float refillTime = 10;




    public object CountDownBar { get; private set; }

    private void Start()
    {

        countdownBar.maxValue = refillTime;
        countdownBar.enabled = false;
        countdownBar.value = countdownBar.maxValue;//banana
    }

    private void Update()
    {

        if (countdownBar.maxValue != refillTime && !countDown)//banana
            countdownBar.maxValue = refillTime;
        else if (countdownBar.maxValue != countDownTime && countDown)//banana
            countdownBar.maxValue = countDownTime;//banana

        if (countDown)
            countdownBar.value -= Time.deltaTime/* / countDownTime * refillTime*/;//banana
        else if(!countDown)
            countdownBar.value += Time.deltaTime;

        //if (countdownBar.value <= 0)
        //{
        //    countDown = false;
        //    allowInputs = false;
        //}
        //else if (countdownBar.value >= refillTime)
        //{

        //    countDown = true;
        //    allowInputs = true;
        //}

    }
    public void CountDownBarFiller() //banana
    {
        countdownBar.value = countdownBar.maxValue;
    }
 
}
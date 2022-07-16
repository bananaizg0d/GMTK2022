using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{

    public static bool allowInputs;

    public Slider countdownBar;
    private bool countDown = true;

    public float countDownTime = 8;
    public float refillTime = 10;

    public object CountDownBar { get; private set; }

    private void Start()
    {

        countdownBar.maxValue = refillTime;
        countdownBar.enabled = false;
    }

    private void Update()
    {

        if (countdownBar.maxValue != refillTime)
            countdownBar.maxValue = refillTime;

        if (countDown)
            countdownBar.value -= Time.deltaTime / countDownTime * refillTime;
        else
            countdownBar.value += Time.deltaTime;

        if (countdownBar.value <= 0)
        {
            countDown = false;
            allowInputs = false;
        }
        else if (countdownBar.value >= refillTime)
        {

            countDown = true;
            allowInputs = false;
            }

    }
 
}
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float currentSeconds;

    string finalTimerValue;

    void Start()
    {
        currentSeconds = 0;
        finalTimerValue = (currentSeconds % 60).ToString("mm:ss");
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        currentSeconds += Time.unscaledDeltaTime;

        finalTimerValue = (currentSeconds % 60).ToString("00:00");

        timerText.text = finalTimerValue;
    }
}

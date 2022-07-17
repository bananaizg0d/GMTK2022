using UnityEngine;
using TMPro;
using System.Text;

public class TimerScript : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float currentSeconds;
    int currentMins;
    bool isStarted;

    StringBuilder sb;

    string finalTimerValue;

    void Start()
    {
        currentSeconds = 0;
        currentMins = 0;
        sb = new StringBuilder();
    }
    void Update()
    {
        if (!isStarted)
            return;
        if (Time.timeScale == 0)
            return;

        sb.Clear();

        currentSeconds += Time.unscaledDeltaTime;

        finalTimerValue = (currentSeconds % 60).ToString("00");
    }
}

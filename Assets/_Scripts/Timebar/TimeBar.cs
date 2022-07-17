using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TimeBar : MonoBehaviour
{
    [SerializeField] Image imageToChange;
    [SerializeField] Slider countdownBar;
    [SerializeField] Color hitZoneColor;


    Color originalColor;

    void Start()
    {
        originalColor = imageToChange.color;
    }

    public void FillTimeBar(float time)
    {
        countdownBar.DOValue(countdownBar.maxValue, time);
    }

    public void EmptyTimeBar(float time)
    {
        imageToChange.color = originalColor;
        countdownBar.DOValue(countdownBar.minValue, time);
    }
 
    public void OnHitZoneEnter()
    {
        imageToChange.color = hitZoneColor;
    }
}
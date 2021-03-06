using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GFXBehaivior : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Color damagedColor;
    [SerializeField] float timeToBlink;
    [SerializeField] float delayBetweenBlinks;

    Color normalColor;
    void Start()
    {
        normalColor = sr.color;
    }
    public IEnumerator Blink()
    {
        var currentTime = 0f;
        var currentColor = 0;
        while(currentTime < timeToBlink)
        {
            if (currentColor == 0)
            {
                sr.color = damagedColor;
                currentColor = 1;
            }
            else
            {
                sr.color = normalColor;
                currentColor = 0;
            }

            yield return new WaitForSeconds(delayBetweenBlinks);
            currentTime += delayBetweenBlinks;
        }

        sr.color = normalColor;
    }
}

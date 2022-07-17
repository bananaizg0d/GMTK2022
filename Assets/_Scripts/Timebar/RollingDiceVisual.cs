using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class RollingDiceVisual : MonoBehaviour
{
    public const string TAG = "RollingDice";
    [SerializeField] Image img;
    [SerializeField] Animator anim;
    [SerializeField] Color hitZoneColor;
    [SerializeField] List<Buff> buffSprites;



    public void OnDiceStart(float time)
    {
        img.color = Color.white;
    }

    public void OnCheckSuccess(float time, bool isBuff, int type)
    {
        foreach (var buff in buffSprites)
        {
            if (buff.isBuff == isBuff && buff.type == type)
            {
                anim.enabled = false;
                img.sprite = buff.sprite;
                img.color = Color.white;
                Invoke(nameof(DisableVisuals), buff.buffTime);
            }
        }

    }

    void DisableVisuals()
    {
        img.color = new Color(0, 0, 0, 0);
    }

    public void OnHitZoneEnter(float time)
    {
        img.color = hitZoneColor;
    }

    void ResetColor()
    {
        img.color = Color.white;
    }

    [System.Serializable]
    class Buff
    {
        public Sprite sprite;
        public int type;
        public bool isBuff;
        public float buffTime;
    }
}
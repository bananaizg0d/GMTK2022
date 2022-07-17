using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class RollingDiceVisual : MonoBehaviour
{
    public const string TAG = "RollingDice";
    [SerializeField] Image mainImg, buffImg;
    [SerializeField] Animator anim;
    [SerializeField] Color hitZoneColor;
    [SerializeField] List<Buff> buffSprites;
    [SerializeField] AudioSource src;



    public void OnDiceStart(float time)
    {
        mainImg.color = Color.white;
    }

    public void OnCheckSuccess(float time, bool isBuff, int type)
    {
        foreach (var buff in buffSprites)
        {
            if (buff.isBuff == isBuff && buff.type == type)
            {
                buffImg.gameObject.SetActive(true);
                if (buff.clip != null)
                    src.PlayOneShot(buff.clip);
                buffImg.sprite = buff.sprite;
                mainImg.color = Color.white;
                Invoke(nameof(OnDisableBuff), buff.buffTime);
            }
        }
    }

    void OnDisableBuff()
    {
        buffImg.gameObject.SetActive(false);
    }

    public void OnHitZoneEnter(float time)
    {
        mainImg.color = hitZoneColor;
    }


    [System.Serializable]
    class Buff
    {
        public Sprite sprite;
        public int type;
        public bool isBuff;
        public float buffTime;
        public AudioClip clip;
    }
}
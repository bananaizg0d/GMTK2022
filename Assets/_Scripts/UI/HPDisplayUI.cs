using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPDisplayUI : MonoBehaviour
{
    [SerializeField] Image hpFill;
    Health health;

    IEnumerator Start()
    {
        GameObject player = null;

        while (player == null)
        {
            player = GameObject.FindWithTag(TopDownMovement.PLAYERTAG);
            yield return new WaitForSeconds(0.5f);
        }

        Init(player);
    }

    void OnDestroy()
    {
        health?._OnDamage.RemoveListener(DisplayHealth);
        health?._OnRestore.RemoveListener(DisplayHealth);
        health?._OnDie.RemoveListener(DisplayZeroHealth);
    }

    void Init(GameObject player)
    {
        health = player.GetComponent<Health>();

        hpFill.fillAmount = health.currentHealth / health.maxHealth;

        health._OnDamage.AddListener(DisplayHealth);
        health._OnRestore.AddListener(DisplayHealth);
        health._OnDie.AddListener(DisplayZeroHealth);
    }

    void DisplayZeroHealth()
    {
        hpFill.fillAmount = 0;
    }

    void DisplayHealth(int newHealth)
    {
        var newFillValue = (float)newHealth / health.maxHealth;
        hpFill.fillAmount = newFillValue;
    }
}

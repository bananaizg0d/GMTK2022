using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ExitScript : MonoBehaviour
{
    [SerializeField] BoxCollider2D coll;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] GameObject arrow;
    [SerializeField] TextMeshProUGUI textObject;
    AIManager aiManager;

    bool enemiesDead;

    void Awake()
    {
        aiManager = FindObjectOfType<AIManager>();
    }

    void Update()
    {
        if (aiManager == null || enemiesDead)
            return;

        if (!enemiesDead && aiManager.transform.childCount == 0)
        {
            enemiesDead = true;
            OpenExit();
        }
        else
        {
            DisplayCurrentEnemies();
        }
    }
    
    void DisplayCurrentEnemies()
    {
        textObject.text = "ENEMIES: \n" + aiManager.transform.childCount;
    }

    void OpenExit()
    {
        textObject.gameObject.SetActive(false);
        coll.isTrigger = true;
        sr.color = Color.green;
        arrow.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != TopDownMovement.PLAYERTAG)
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

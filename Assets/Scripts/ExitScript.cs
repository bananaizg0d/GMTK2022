using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    [SerializeField] BoxCollider2D coll;
    [SerializeField] SpriteRenderer sr;
    AIManager aiManager;

    void Awake()
    {
        aiManager = FindObjectOfType<AIManager>();
    }

    void OnEnable()
    {
        aiManager.onEnemiesDie += OpenExit;
    }

    void OnDisable()
    {
        aiManager.onEnemiesDie -= OpenExit;
    }

    void OpenExit()
    {
        coll.isTrigger = true;
        sr.color = Color.green;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != TopDownMovement.PLAYERTAG)
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

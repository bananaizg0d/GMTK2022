using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    public GameObject[] enemies;
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {

        if (enemies.Length <= 0)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            gameObject.GetComponent<BoxCollider2D>().enabled = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}

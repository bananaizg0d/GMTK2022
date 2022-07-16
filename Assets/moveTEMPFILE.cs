using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTEMPFILE : MonoBehaviour
{
    private Rigidbody2D player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player.velocity = new Vector2(1f, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTimeBar : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        //ref
        TimeBar timebar = gameObject.GetComponent<TimeBar>();

        //enable timebar
        timebar.enabled = true;

        //disable timebar
        //timebar.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

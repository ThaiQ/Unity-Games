//Recreated by ThaiQ
//Created by Brackeys (Youtube Channel)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public float speed = 10f;

    private Transform target;
    private int wptindex = 0;

    // Use this for initialization
    void Start()
    {
        target = wpt.pt[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * speed , Space.World);
        
        if (Vector3.Distance(target.position, transform.position)<=0.2f)
        {
            nxtwpt();
        }

    }

    void nxtwpt() {

        if (wptindex <=wpt.pt.Length)
        {
            wptindex++;
            target = wpt.pt[wptindex];
        }
        else {
            wptindex = -1;
        }

    }

}


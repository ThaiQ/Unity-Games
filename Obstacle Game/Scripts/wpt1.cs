//Recreated by ThaiQ
//Created by Brackeys (Youtube Channel)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wpt1 : MonoBehaviour
{
    public static Transform[] pt;
    private int i;
    // Use this for initialization
    /*void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/
    private void Awake()
    {
        pt = new Transform[transform.childCount];

        for (int i = 0; i < pt.Length; i++)
        {
            pt[i] = transform.GetChild(i);
        }

    }

}

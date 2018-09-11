//Created by ThaiQ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Petals : MonoBehaviour {

    public static Transform[] PositionPetals;
    private int i;
    private bool[] all;
    public NewPacman pac;
    private int NofPetals;
    private int scores = 0;

    // Use this for initialization
    void Start()
    {
        PositionPetals = new Transform[transform.childCount];
        all = new bool[PositionPetals.Length];
        for (i = 0; i < PositionPetals.Length; i++)
        {PositionPetals[i] = transform.GetChild(i);}
    }

    // Update is called once per frame
    void Update()
    {
        Consumption();
    }

    void Consumption()
    {
        for (i=0; i < PositionPetals.Length; i++)
        {
            var x = Mathf.Abs(PositionPetals[i].transform.position.x - pac.Pacposition().x);
            var y = Mathf.Abs(PositionPetals[i].transform.position.y - pac.Pacposition().y);
            if (PositionPetals[i].GetComponent<SpriteRenderer>().enabled == true)
            {
                if (x < 0.1 && y < 0.1)
                {
                    PositionPetals[i].GetComponent<SpriteRenderer>().enabled = false;
                    scores++;
                }
            }
            else { }
        }
    }

    public int currentscore()
    {
        return scores;
    }
}

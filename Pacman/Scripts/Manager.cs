//By ThaiQ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Text scores;
    public Text times;
    public Text win;
    public Petals petals;
    private float sec;
    private float min;

    // Use this for initialization
    void Start () {
        win.enabled = false;
        scores.enabled = true;
        times.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameover())
        {
            win.enabled = true;
        }
        else { timer(); }
        currentscores();
	}

    bool gameover()
    {
        if (petals.currentscore() >= 80)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    void timer()
    {
        min = (int)(Time.time / 60);
        sec = (Time.time % 60);

        times.text = min.ToString() + ":" + sec.ToString();
    }

    void currentscores()
    {
        var point = petals.currentscore();
        scores.text = "Scores:" + point;
    }

}

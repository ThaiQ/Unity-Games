//Recreated by ThaiQ
//Created by Brackeys (Youtube Channel)
//Created by GameGrind (Youtube Channel)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private float speed = 7f;
    private Vector3 control;
    private Vector3 spawn;

    // Use this for initialization
    void Start()
    {
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        control = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Rigidbody.AddForce(control);
        //GetComponent<Rigidbody>().AddForce(control*10f*Time.deltaTime);
        transform.Translate(control*Time.deltaTime*speed);

        if (transform.position.y<-2) { Die();}

    }

    void OnCollisionEnter(Collision dead)
    {
        if (dead.transform.tag == "Enemy")
        {Die();}
        else if (dead.transform.tag == "goal")
        { master.NewLevel();}
    }

    void Die() {
        transform.position = spawn;
    }

}
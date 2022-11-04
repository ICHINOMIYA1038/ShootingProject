using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class InputManager : MonoBehaviour
{
   
    bool canLaunch;
    float span;
   public PlayerBulletController controller;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
   
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetAxis("Fire1")!=0)
        {
            controller.launchLeft();
        }
        if (Input.GetAxis("Fire2") != 0)
        {
            controller.launchRight();
        }


    }


   
}

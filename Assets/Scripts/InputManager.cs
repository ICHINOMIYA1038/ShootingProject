using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class InputManager : MonoBehaviour
{
   public PlayerBulletController controller;
    [SerializeField]
   gameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
   
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.getCurrentScene() == gameManager.MAIN_SCENE)
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                controller.launchLeft();
            }
            if (Input.GetAxis("Fire2") != 0)
            {
                controller.launchRight();
            }
        }
       


    }


   
}

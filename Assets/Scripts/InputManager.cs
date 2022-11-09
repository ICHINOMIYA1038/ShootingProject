using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class InputManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerBulletController controller;
    [SerializeField] private PlayerHeliController playerHeliController;
    [SerializeField] private HeliCameraController cameraController;
    [SerializeField] private GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
   
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.getCurrentScene() == GameManager.MAIN_SCENE&&manager.isConfig==false&&manager.isMovieNow==false)
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                controller.launchLeft();
            }
            if (Input.GetAxis("Fire2") != 0)
            {
                controller.launchRight();
            }
            //カメラ操作のチェック
            CameraUpdateCheck();
            Turbocheck();
        }


    }


    private void CameraUpdateCheck()
    {
        //フォーカス中のカメラ
        //フォーカス中のカメラは、ターボに関わらず、一定のカメラ感度を持つ。
        if (Input.GetAxis("Focus") != 0)
        {
            cameraController.SetCameraFocus();
        }


        //フォーカスではない状態で、ターボ状態に入ったとき。
        if (Input.GetAxis("Focus") == 0 && Input.GetAxis("Turbo") != 0)
        {
            cameraController.SetCameraTurboWithoutFocus();
        }

        //フォーカス中でもターボでもない時のカメラ操作
        if (Input.GetAxis("Focus") == 0 && Input.GetAxis("Turbo") == 0)
        {
            cameraController.SetCameraDefault();
        }

        //ターボに入った時
        if (Input.GetAxis("Turbo") != 0)
        {
            cameraController.setTurbo();
        }

        //ターボ終了
        if (Input.GetAxis("Turbo") == 0)
        {
            cameraController.finishTurbo();
        }
    }

    private void Turbocheck()
    {
        if(Input.GetAxis("Turbo") != 0)
        {
            playerHeliController.isTurbo = true;
        }
        if (Input.GetAxis("Turbo") == 0)
        {
            playerHeliController.isTurbo = false;
        }

    }


   
}
